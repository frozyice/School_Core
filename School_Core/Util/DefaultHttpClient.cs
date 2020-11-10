using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using School_Core.API.DTOs;

namespace School_Core.Util
{
    public interface IMedicalHttpClient
    {
        public HttpClient Client { get; }
        public Task<HttpResponseMessage> GetAsync(string requestUri);
        public Task<HttpResponseMessage> PostAsync(string requestUri, MedicalWriteDto writeDto);
        public Task<HttpResponseMessage> PutAsync(string requestUri, MedicalWriteDto writeDto);
        public Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }

    public class MedicalHttpClient : IMedicalHttpClient
    {
        public HttpClient Client { get; }

        public MedicalHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:3001/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            var response = new HttpResponseMessage();
            try
            {
                response = await Client.GetAsync(requestUri);
            }
            catch (HttpRequestException)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
            }

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, MedicalWriteDto writeDto)
        {
            var serializedObject = JsonConvert.SerializeObject(writeDto);
            var response = new HttpResponseMessage();
            try
            {
                response = await Client.PostAsync(requestUri, new StringContent(serializedObject, Encoding.Default, "application/json"));
            }
            catch (HttpRequestException)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
            }

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, MedicalWriteDto writeDto)
        {
            var serializedObject = JsonConvert.SerializeObject(writeDto);
            var response = new HttpResponseMessage();
            try
            {
                response = await Client.PutAsync(requestUri, new StringContent(serializedObject, Encoding.Default, "application/json"));
            }
            catch (HttpRequestException)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            var response = new HttpResponseMessage();
            try
            {
                response = await Client.DeleteAsync(requestUri);
            }
            catch (HttpRequestException)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
            }

            return response;
        }
    }
}