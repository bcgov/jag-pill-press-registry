using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.OData.OpenAPI;
using NJsonSchema;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using NSwag;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace odata2openapi
{

    /// <summary>
    /// LocalizedLabel
    /// </summary>
    public partial class MicrosoftDynamicsCRMLocalizedLabel
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMLocalizedLabel class.
        /// </summary>
        public MicrosoftDynamicsCRMLocalizedLabel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMLocalizedLabel class.
        /// </summary>
        public MicrosoftDynamicsCRMLocalizedLabel(string label = default(string), int? languageCode = default(int?), bool? isManaged = default(bool?), string metadataId = default(string), bool? hasChanged = default(bool?))
        {
            Label = label;
            LanguageCode = languageCode;
            IsManaged = isManaged;
            MetadataId = metadataId;
            HasChanged = hasChanged;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Label")]
        public string Label { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "LanguageCode")]
        public int? LanguageCode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsManaged")]
        public bool? IsManaged { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "MetadataId")]
        public string MetadataId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "HasChanged")]
        public bool? HasChanged { get; set; }

    }

    /// <summary>
    /// BooleanManagedProperty
    /// </summary>
    public partial class MicrosoftDynamicsCRMBooleanManagedProperty
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMBooleanManagedProperty class.
        /// </summary>
        public MicrosoftDynamicsCRMBooleanManagedProperty()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMBooleanManagedProperty class.
        /// </summary>
        public MicrosoftDynamicsCRMBooleanManagedProperty(bool? value = default(bool?), bool? canBeChanged = default(bool?), string managedPropertyLogicalName = default(string))
        {
            Value = value;
            CanBeChanged = canBeChanged;
            ManagedPropertyLogicalName = managedPropertyLogicalName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Value")]
        public bool? Value { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CanBeChanged")]
        public bool? CanBeChanged { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ManagedPropertyLogicalName")]
        public string ManagedPropertyLogicalName { get; set; }

    }


    /// <summary>
    /// Label
    /// </summary>
    public partial class MicrosoftDynamicsCRMLabel
    {
        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMLabel class.
        /// </summary>
        public MicrosoftDynamicsCRMLabel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMLabel class.
        /// </summary>
        public MicrosoftDynamicsCRMLabel(IList<MicrosoftDynamicsCRMLocalizedLabel> localizedLabels = default(IList<MicrosoftDynamicsCRMLocalizedLabel>), MicrosoftDynamicsCRMLocalizedLabel userLocalizedLabel = default(MicrosoftDynamicsCRMLocalizedLabel))
        {
            LocalizedLabels = localizedLabels;
            UserLocalizedLabel = userLocalizedLabel;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "LocalizedLabels")]
        public IList<MicrosoftDynamicsCRMLocalizedLabel> LocalizedLabels { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "UserLocalizedLabel")]
        public MicrosoftDynamicsCRMLocalizedLabel UserLocalizedLabel { get; set; }

    }
    /// <summary>
    /// OptionSetMetadataBase
    /// </summary>
    public partial class MicrosoftDynamicsCRMOptionSetMetadataBase
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMOptionSetMetadataBase class.
        /// </summary>
        public MicrosoftDynamicsCRMOptionSetMetadataBase()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMOptionSetMetadataBase class.
        /// </summary>
        /// <param name="optionSetType">Possible values include: 'Picklist',
        /// 'State', 'Status', 'Boolean'</param>
        public MicrosoftDynamicsCRMOptionSetMetadataBase(MicrosoftDynamicsCRMLabel description = default(MicrosoftDynamicsCRMLabel), MicrosoftDynamicsCRMLabel displayName = default(MicrosoftDynamicsCRMLabel), bool? isCustomOptionSet = default(bool?), bool? isGlobal = default(bool?), bool? isManaged = default(bool?), MicrosoftDynamicsCRMBooleanManagedProperty isCustomizable = default(MicrosoftDynamicsCRMBooleanManagedProperty), string name = default(string), string optionSetType = default(string), string introducedVersion = default(string))
        {
            Description = description;
            DisplayName = displayName;
            IsCustomOptionSet = isCustomOptionSet;
            IsGlobal = isGlobal;
            IsManaged = isManaged;
            IsCustomizable = isCustomizable;
            Name = name;
            OptionSetType = optionSetType;
            IntroducedVersion = introducedVersion;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public MicrosoftDynamicsCRMLabel Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DisplayName")]
        public MicrosoftDynamicsCRMLabel DisplayName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsCustomOptionSet")]
        public bool? IsCustomOptionSet { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsGlobal")]
        public bool? IsGlobal { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsManaged")]
        public bool? IsManaged { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsCustomizable")]
        public MicrosoftDynamicsCRMBooleanManagedProperty IsCustomizable { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Picklist', 'State',
        /// 'Status', 'Boolean'
        /// </summary>
        [JsonProperty(PropertyName = "OptionSetType")]
        public string OptionSetType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IntroducedVersion")]
        public string IntroducedVersion { get; set; }

        [JsonProperty(PropertyName = "Options")]
        public List<Option> Options { get; set; }

    }

    public class Option
    {
        [JsonProperty(PropertyName = "Value")]
        public int Value { get; set; }


        [JsonProperty(PropertyName = "Label")]
        public MicrosoftDynamicsCRMLabel Label { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public MicrosoftDynamicsCRMLabel Description { get; set; }

        [JsonProperty(PropertyName = "Color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "IsManaged")]
        public string IsManaged { get; set; }

        [JsonProperty(PropertyName = "MetadataId")]
        public string MetadataId { get; set; }

        [JsonProperty(PropertyName = "HasChanged")]
        public string HasChanged { get; set; }

    }

    class OptionSetResult
    {
        [JsonProperty(PropertyName = "value")]
        public IList<MicrosoftDynamicsCRMOptionSetMetadataBase> Value { get; set; }

    }


    class Program
    {
        
        static string GetOptionSets (IConfiguration Configuration )
        {
            string dynamicsOdataUri = Configuration["DYNAMICS_ODATA_URI"];
            string aadTenantId = Configuration["DYNAMICS_AAD_TENANT_ID"];
            string serverAppIdUri = Configuration["DYNAMICS_SERVER_APP_ID_URI"];
            string clientKey = Configuration["DYNAMICS_CLIENT_KEY"];
            string clientId = Configuration["DYNAMICS_CLIENT_ID"];

            var authenticationContext = new AuthenticationContext(
                "https://login.windows.net/" + aadTenantId);
                ClientCredential clientCredential = new ClientCredential(clientId, clientKey);
            var task = authenticationContext.AcquireTokenAsync(serverAppIdUri, clientCredential);
            task.Wait();
            var authenticationResult = task.Result;

            string result = null;
            var webRequest = WebRequest.Create(dynamicsOdataUri + "GlobalOptionSetDefinitions");
            HttpWebRequest request = (HttpWebRequest)webRequest;
            request.Method = "GET";
            //request.PreAuthenticate = true;
            request.Headers.Add("Authorization", authenticationResult.CreateAuthorizationHeader());
            //request.Accept = "application/json;odata=verbose";
            request.ContentType =  "application/json";

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

            // login to dynamics
            string jsonString = GetOptionSets(Configuration);

            // now parse the result as a list of meta data

            OptionSetResult optionSetResult = JsonConvert.DeserializeObject<OptionSetResult>(jsonString);

            string result = "";

            foreach (var item in optionSetResult.Value)
            {
                if (item.Options != null)
                {
                    string cleanedName = item.Name.Replace("bcgov_","");
                    string firstChar = "" + cleanedName[0];
                    cleanedName = firstChar.ToUpper() + cleanedName.Substring(1);

                    result += $"\tpublic enum {cleanedName}\r\n";
                    result += "\t{\r\n";

                    for (int i = 0; i < item.Options.Count; i++)
                    {
                        var option = item.Options[i];
                        var label = option.Label.LocalizedLabels.FirstOrDefault();

                        result += $"\t\t[Display(Name = \"{label.Label}\")]\r\n";

                        string sanitized = label.Label.Replace(" ", "");
                        sanitized = sanitized.Replace("-", "");
                        sanitized = sanitized.Replace(",", "");

                        result += $"\t\t{sanitized} = {option.Value}";
                        if (i < item.Options.Count - 1)
                        {
                            result += ",";
                        }
                        result += "\r\n";
                    }
                    result += "\t}\r\n\r\n\r\n";
                }
                

            }

            Console.Out.WriteLine(result);
            Console.In.ReadLine();

        }
    }
}
