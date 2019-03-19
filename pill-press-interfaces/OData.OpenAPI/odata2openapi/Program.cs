﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.OData.OpenAPI;
using NJsonSchema;
using NSwag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

/****
 * 
 * OData2OpenAPI - conversion tool used to convert from MS Dynamics to OpenAPI.
 * 
 *****/

namespace odata2openapi
{
    class Program
    {
        
        static string GetDynamicsMetadata (IConfiguration Configuration )
        {
            string dynamicsOdataUri = Configuration["DYNAMICS_ODATA_URI"];
            string aadTenantId = Configuration["DYNAMICS_AAD_TENANT_ID"];
            string serverAppIdUri = Configuration["DYNAMICS_SERVER_APP_ID_URI"];
            string clientKey = Configuration["DYNAMICS_CLIENT_KEY"];
            string clientId = Configuration["DYNAMICS_CLIENT_ID"];
            string ssgUsername = Configuration["SSG_USERNAME"];
            string ssgPassword = Configuration["SSG_PASSWORD"];
            var webRequest = WebRequest.Create(dynamicsOdataUri + "$metadata");
            HttpWebRequest request = (HttpWebRequest)webRequest;
           
            if (string.IsNullOrEmpty(ssgUsername) || string.IsNullOrEmpty(ssgPassword))
            {
                var authenticationContext = new AuthenticationContext(
                "https://login.windows.net/" + aadTenantId);
                ClientCredential clientCredential = new ClientCredential(clientId, clientKey);
                var task = authenticationContext.AcquireTokenAsync(serverAppIdUri, clientCredential);
                task.Wait();
                AuthenticationResult authenticationResult = task.Result;
                request.Headers.Add("Authorization", authenticationResult.CreateAuthorizationHeader());
            }
            else
            {
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(ssgUsername + ":" + ssgPassword));
                request.Headers.Add("Authorization", "Basic " + encoded); 
            }
            
            string result = null;
            
            request.Method = "GET";
            request.PreAuthenticate = true;
            //request.Accept = "application/json;odata=verbose";
            //request.ContentType =  "application/json";

            // we need to add authentication to a HTTP Client to fetch the file.
            using (
                MemoryStream ms = new MemoryStream())
            {               
                request.GetResponse().GetResponseStream().CopyTo(ms);
                var buffer = ms.ToArray();
                result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }

            return result;

        }

        static void FixProperty (JsonSchema4 item , string name)
        {
            if (item.Properties.Keys.Contains(name))
            {
                item.Properties.Remove(name);

                JsonProperty jsonProperty = new JsonProperty();
                jsonProperty.Type = JsonObjectType.String;
                jsonProperty.IsNullableRaw = true;
                KeyValuePair<string, JsonProperty> keyValuePair = new KeyValuePair<string, JsonProperty>(name, jsonProperty);

                item.Properties.Add(keyValuePair);

            }
        }

        static void Main(string[] args)
        {
            bool getMetadata = true;

            // start by getting secrets.
            var builder = new ConfigurationBuilder()                
                .AddEnvironmentVariables();

            builder.AddUserSecrets<Program>();           
            var Configuration = builder.Build();
            string csdl;
            // get the metadata.
            if (getMetadata)
            {
                csdl = GetDynamicsMetadata(Configuration);
                File.WriteAllText("dynamics-metadata.xml", csdl);
            }
            else
            {
                csdl = File.ReadAllText("dynamics-metadata.xml");
            }               

            // fix the csdl.

            csdl = csdl.Replace("ConcurrencyMode=\"Fixed\"", "");

            Microsoft.OData.Edm.IEdmModel model = Microsoft.OData.Edm.Csdl.CsdlReader.Parse(XElement.Parse(csdl).CreateReader());

            // fix dates.
            OpenApiTarget target = OpenApiTarget.Json;
            OpenApiWriterSettings settings = new OpenApiWriterSettings
            {
                BaseUri = new Uri(Configuration["DYNAMICS_ODATA_URI"])
            };
           
            string swagger = null;

            using (MemoryStream ms = new MemoryStream())
            {
                model.WriteOpenApi(ms, target, settings);
                var buffer = ms.ToArray();
                string temp = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                // The Microsoft OpenAPI.Net library doesn't seem to work with MS Dynamics metadata, so we use NSwag here.

                var runner = SwaggerDocument.FromJsonAsync(temp);
                runner.Wait();
                var swaggerDocument = runner.Result;

                List<string> allops = new List<string>();
                Dictionary<string, SwaggerPathItem> itemsToRemove = new Dictionary<string, SwaggerPathItem>();
                // fix the operationIds.
                foreach (var operation in swaggerDocument.Operations)
                {
                    string suffix = "";
                    switch (operation.Method)
                    {
                        case SwaggerOperationMethod.Post:
                            suffix = "Create";
                            // for creates we also want to add a header parameter to ensure we get the new object back.
                            SwaggerParameter swaggerParameter = new SwaggerParameter()
                            {
                                Type = JsonObjectType.String,
                                Name = "Prefer",
                                Default = "return=representation",
                                Description = "Required in order for the service to return a JSON representation of the object.",
                                Kind = SwaggerParameterKind.Header
                            };
                            operation.Operation.Parameters.Add(swaggerParameter);
                            break;

                        case SwaggerOperationMethod.Patch:
                            suffix = "Update";
                            break;

                        case SwaggerOperationMethod.Put:
                            suffix = "Put";
                            break;

                        case SwaggerOperationMethod.Delete:
                            suffix = "Delete";
                            break;

                        case SwaggerOperationMethod.Get:
                            if (operation.Path.Contains("{"))
                            {
                                suffix = "GetByKey";
                            }
                            else
                            {
                                suffix = "Get";
                            }                            
                            break;
                    }

                    string prefix = "Unknown";
                    string firstTag = operation.Operation.Tags.FirstOrDefault();

                    if (firstTag == null)
                    {
                        firstTag = operation.Path.Substring(1);
                    }

                    
                    if (firstTag != null)
                    {
                        bool ok2Delete = true;
                        string firstTagLower = firstTag.ToLower();

                        // || firstTagLower.Equals("equipments")

                        if (firstTagLower.Equals("incidents") || firstTagLower.Equals("sharepointdocumentlocations") || firstTagLower.Equals("sharepointsites") || firstTagLower.Equals("contacts") || firstTagLower.Equals("accounts") || firstTagLower.Equals("invoices") || firstTagLower.Equals("entitydefinitions") || firstTagLower.Equals("globaloptionsetdefinitions"))
                        {
                            ok2Delete = false;
                        }
                        if (firstTagLower.IndexOf ("bcgov") != -1)
                        {
                            ok2Delete = false;
                            firstTagLower = firstTagLower.Replace("bcgov_", "");
                            firstTagLower = firstTagLower.Replace("bcgov", "");
                            operation.Operation.Tags.Clear();
                            operation.Operation.Tags.Add(firstTagLower);
                        }                        

                            if (ok2Delete)
                        {
                            if (! itemsToRemove.Keys.Contains(operation.Path))
                            {
                                itemsToRemove.Add(operation.Path, operation.Operation.Parent);                                
                            }                            
                        }

                        if (!allops.Contains(firstTag))
                        {
                            allops.Add(firstTag);
                        }
                        prefix = firstTagLower;
                        // Capitalize the first character.


                        if (prefix.Length > 0)
                        {
                            prefix.Replace("bcgov", "");
                            prefix = ("" + prefix[0]).ToUpper() + prefix.Substring(1);
                        }
                        // remove any underscores.
                        prefix = prefix.Replace("_", "");
                    }

                    operation.Operation.OperationId = prefix + "_" + suffix;

                    // adjustments to operation parameters

                    foreach (var parameter in operation.Operation.Parameters)
                    {
                        string name = parameter.Name;
                        if (name == null)
                        {
                            name = parameter.ActualParameter.Name;
                        }
                        if (name != null)
                        {                            
                            if (name == "$top")
                            {
                                parameter.Kind = SwaggerParameterKind.Query;
                                parameter.Reference = null;
                                parameter.Schema = null;
                                parameter.Type = JsonObjectType.Integer;
                            }
                            if (name == "$skip")
                            {
                                parameter.Kind = SwaggerParameterKind.Query;
                                parameter.Reference = null;
                                parameter.Schema = null;
                                parameter.Type = JsonObjectType.Integer;
                            }
                            if (name == "$search")
                            {
                                parameter.Kind = SwaggerParameterKind.Query;
                                parameter.Reference = null;
                                parameter.Schema = null;
                                parameter.Type = JsonObjectType.String;
                            }
                            if (name == "$filter")
                            {
                                parameter.Kind = SwaggerParameterKind.Query;
                                parameter.Reference = null;
                                parameter.Schema = null;
                                parameter.Type = JsonObjectType.String;
                            }
                            if (name == "$count")
                            {
                                parameter.Kind = SwaggerParameterKind.Query;
                                parameter.Reference = null;
                                parameter.Schema = null;
                                parameter.Type = JsonObjectType.Boolean;
                            }
                            if (name == "If-Match")
                            {
                                parameter.Reference = null;
                                parameter.Schema = null;
                            }
                            if (string.IsNullOrEmpty(parameter.Name))
                            {
                                parameter.Name = name;
                            }
                        }
                        //var parameter = loopParameter.ActualParameter;
                        
                        // get rid of style if it exists.
                        if (parameter.Style != SwaggerParameterStyle.Undefined)
                        {
                            parameter.Style = SwaggerParameterStyle.Undefined;
                        }

                        // clear unique items
                        if (parameter.UniqueItems)
                        {
                            parameter.UniqueItems = false;
                        }

                        // we also need to align the schema if it exists.
                        if (parameter.Schema != null && parameter.Schema.Item != null)
                        {

                            var schema = parameter.Schema;
                            if (schema.Type == JsonObjectType.Array)
                            {
                                // move schema up a level.
                                parameter.Item = schema.Item;
                                parameter.Schema = null;
                                parameter.Reference = null;
                                parameter.Type = JsonObjectType.Array;
                            }
                            if (schema.Type == JsonObjectType.String)
                            {
                                parameter.Schema = null;
                                parameter.Reference = null;
                                parameter.Type = JsonObjectType.String;
                            }
                        }
                        else
                        {
                            // many string parameters don't have the type defined.
                            if (!(parameter.Kind == SwaggerParameterKind.Body) && !parameter.HasReference && (parameter.Type == JsonObjectType.Null || parameter.Type == JsonObjectType.None))
                            {
                                parameter.Schema = null;
                                parameter.Type = JsonObjectType.String;
                            }
                        }
                    }

                    // adjustments to response
                    // TODO changes to fix the “GetOkResponseModelModelModel…” generated models
                    // Compare to Cannabis solution https://github.com/bcgov/jag-lcrb-carla-public/blob/master/cllc-interfaces/OData.OpenAPI/odata2openapi/Program.cs line 342

                    foreach (var response in operation.Operation.Responses)
                    {
                        var val = response.Value;
                        if (val != null && 
                            val.Reference != null && 
                            val.Reference.Description != null &&
                            val.Reference.Description.Equals("ok"))
                        {
                            var schema = val.Schema;
                           
                        }
                        
                    }
                }
                
                foreach (var opDelete in itemsToRemove)
                {
                    Debug.WriteLine($"Removing {opDelete.Key}");
                    swaggerDocument.Paths.Remove(opDelete);
                    
                }

                /*
                 * Cleanup definitions.                 
                 */

                foreach (var definition in swaggerDocument.Definitions)
                {                    


                    foreach (var property in definition.Value.Properties)
                    {
                        if (property.Key.Equals("totalamount"))
                        {
                            property.Value.Type = JsonObjectType.Number;
                            property.Value.Format = "decimal";
                        }


                            if (property.Key.Equals("versionnumber"))
                        {                            
                            // clear oneof.
                            property.Value.OneOf.Clear();
                            // force to string.
                            property.Value.Type = JsonObjectType.String;
                        }
                    }
                    
                }

                // cleanup parameters.
                swaggerDocument.Parameters.Clear();

                swagger = swaggerDocument.ToJson(SchemaType.Swagger2);

                // fix up the swagger file.

                swagger = swagger.Replace("('{", "({");
                swagger = swagger.Replace("}')", "})");

                swagger = swagger.Replace("\"$ref\": \"#/responses/error\"", "\"schema\": { \"$ref\": \"#/definitions/odata.error\" }");

                // fix for problem with the base entity.

                swagger = swagger.Replace("        {\r\n          \"$ref\": \"#/definitions/Microsoft.Dynamics.CRM.crmbaseentity\"\r\n        },\r\n", "");

                // NSwag is almost able to generate the client as well.  It does it much faster than AutoRest but unfortunately can't do multiple files yet.                

                /*
                var generatorSettings = new SwaggerToCSharpClientGeneratorSettings
                {
                    ClassName = "DynamicsClient",
                    CSharpGeneratorSettings =
                    {
                        Namespace = "<namespace>"
                    }
                };                

                var generator = new SwaggerToCSharpClientGenerator(swaggerDocument, generatorSettings);
                var code = generator.GenerateFile();

                File.WriteAllText("<filename>", code);
                */

            }

            // output the file.

            File.WriteAllText("dynamics-swagger.json", swagger);

        }
    }
}
