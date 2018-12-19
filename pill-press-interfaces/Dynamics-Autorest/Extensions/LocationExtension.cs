using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/webapi/associate-disassociate-entities-using-web-api

namespace Gov.Jag.PillPressRegistry.Interfaces
{

    public partial class Locations : IServiceOperations<DynamicsClient>, ILocations
    {

        /// <summary>
        /// Add reference to adoxio_workers
        /// </summary>
        /// <param name='locationId'>
        /// key: adoxio_workerid
        /// </param>
        /// <param name='fieldname'>
        /// key: fieldname
        /// </param>
        /// <param name='odataid'>
        /// reference value
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="OdataerrorException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>

        public async Task<HttpOperationResponse> AddReferenceWithHttpMessagesAsync(string locationId, string fieldname, OdataId odataid = default(OdataId), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (locationId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "locationId");
            }
            if (fieldname == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "fieldname");
            }
            if (odataid == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "odataids");
            }
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("locationid", locationId);
                tracingParameters.Add("fieldname", fieldname);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "AddReference", tracingParameters);
            }
            // Construct URL
            var _baseUrl = Client.BaseUri.AbsoluteUri;
            var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "bcgov_locations({locationId})/{fieldname}/$ref").ToString();
            _url = _url.Replace("{locationId}", System.Uri.EscapeDataString(locationId));
            _url = _url.Replace("{fieldname}", System.Uri.EscapeDataString(fieldname));
            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("POST");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers


            if (customHeaders != null)
            {
                foreach (var _header in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(_header.Key))
                    {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            if (odataid != null)
            {


                _requestContent = Microsoft.Rest.Serialization.SafeJsonConvert.SerializeObject(odataid, Client.SerializationSettings);
                _httpRequest.Content = new StringContent(_requestContent, System.Text.Encoding.UTF8);
                _httpRequest.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
            }
            // Set Credentials
            if (Client.Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Client.Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            }
            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await Client.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int)_statusCode != 204)
            {
                var ex = new OdataerrorException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                try
                {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Odataerror _errorBody = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<Odataerror>(_responseContent, Client.DeserializationSettings);
                    if (_errorBody != null)
                    {
                        ex.Body = _errorBody;
                    }
                }
                catch (JsonException)
                {
                    // Ignore the exception
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

        /// <summary>
        /// Remove reference to location
        /// </summary>
        /// <param name='locationId'>
        /// key: adoxio_workerid
        /// </param>
        /// <param name='fieldname'>
        /// key: fieldname
        /// </param>
        /// <param name='odataid'>
        /// reference value
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="OdataerrorException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        /// 
        public async Task<HttpOperationResponse> RemoveReferenceWithHttpMessagesAsync(string locationId, string fieldname, string removeId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (locationId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "locationId");
            }
            if (fieldname == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "fieldname");
            }

            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("locationid", locationId);
                tracingParameters.Add("fieldname", fieldname);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "AddReference", tracingParameters);
            }
            // Construct URL
            var _baseUrl = Client.BaseUri.AbsoluteUri;
            var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "bcgov_locations({locationId})/{fieldname}({removeId})/$ref").ToString();

            if (removeId == null)
            {
                _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "bcgov_locations({locationId})/{fieldname}/$ref").ToString();
            }
            else
            {
                _url = _url.Replace("{removeId}", System.Uri.EscapeDataString(removeId));
            }


            _url = _url.Replace("{locationId}", System.Uri.EscapeDataString(locationId));
            _url = _url.Replace("{fieldname}", System.Uri.EscapeDataString(fieldname));

            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("DELETE");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers


            if (customHeaders != null)
            {
                foreach (var _header in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(_header.Key))
                    {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;

            // Set Credentials
            if (Client.Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Client.Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            }
            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await Client.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int)_statusCode != 204)
            {
                var ex = new OdataerrorException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                try
                {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Odataerror _errorBody = Microsoft.Rest.Serialization.SafeJsonConvert.DeserializeObject<Odataerror>(_responseContent, Client.DeserializationSettings);
                    if (_errorBody != null)
                    {
                        ex.Body = _errorBody;
                    }
                }
                catch (JsonException)
                {
                    // Ignore the exception
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

    }

    public partial interface ILocations
    {
        /// <summary>
        /// Add reference to location
        /// </summary>
        /// <param name='locationId'>
        /// key: adoxio_workerid
        /// </param>
        /// <param name='fieldname'>
        /// key: fieldname
        /// </param>
        /// <param name='odataid'>
        /// reference value
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="OdataerrorException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<HttpOperationResponse> AddReferenceWithHttpMessagesAsync(string locationId, string fieldname, OdataId odataid = default(OdataId), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove reference to location
        /// </summary>
        /// <param name='locationId'>
        /// key: adoxio_workerid
        /// </param>
        /// <param name='fieldname'>
        /// key: fieldname
        /// </param>
        /// <param name='odataid'>
        /// reference value
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="OdataerrorException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<HttpOperationResponse> RemoveReferenceWithHttpMessagesAsync(string locationId, string fieldname, String removeId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }

    public static partial class LocationsExtensions
    {
        /// <summary>
        /// Add reference to adoxio_workers
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='locationId'>
        /// key: adoxio_workerid
        /// </param>
        /// <param name='fieldname'>
        /// key: fieldname
        /// </param>
        /// <param name='odataid'>
        /// reference value
        /// </param>
        public static void AddReference(this ILocations operations, string locationId, string fieldname, OdataId odataid = default(OdataId))
        {
            operations.AddReferenceAsync(locationId, fieldname, odataid).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Remove reference to adoxio_workers
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='locationId'>
        /// key: adoxio_workerid
        /// </param>
        /// <param name='fieldname'>
        /// key: fieldname
        /// </param>
        /// <param name='odataid'>
        /// reference value
        /// </param>
        public static void RemoveReference(this ILocations operations, string locationId, string fieldname, string removeId)
        {
            operations.RemoveReferenceAsync(locationId, fieldname, removeId).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Add reference to adoxio_workers
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='locationId'>
        /// key: locationId
        /// </param>
        /// <param name='fieldname'>
        /// key: fieldname
        /// </param>
        /// <param name='odataid'>
        /// reference value
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task AddReferenceAsync(this ILocations operations, string locationId, string fieldname, OdataId odataid = default(OdataId), CancellationToken cancellationToken = default(CancellationToken))
        {
            (await operations.AddReferenceWithHttpMessagesAsync(locationId, fieldname, odataid, null, cancellationToken).ConfigureAwait(false)).Dispose();
        }

        /// <summary>
        /// Remove reference to adoxio_workers
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='locationId'>
        /// key: locationId
        /// </param>
        /// <param name='fieldname'>
        /// key: fieldname
        /// </param>
        /// <param name='odataid'>
        /// reference value
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task RemoveReferenceAsync(this ILocations operations, string locationId, string fieldname, string removeId, CancellationToken cancellationToken = default(CancellationToken))
        {
            (await operations.RemoveReferenceWithHttpMessagesAsync(locationId, fieldname, removeId, null, cancellationToken).ConfigureAwait(false)).Dispose();
        }

    }
}
