using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using School_Core.API.DTOs;

namespace School_Core.Util
{
    public interface IDefaultHttpClient
    {
        public HttpClient Client { get; }
        public Task<HttpResponseMessage> GetAsync(string requestUri);
        public Task<HttpResponseMessage> PostAsync(string requestUri, MedicalWriteDto writeDto);
        public Task<HttpResponseMessage> PutAsync(string requestUri, MedicalWriteDto writeDto);
        public Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }

    public class DefaultHttpClient : IDefaultHttpClient
    {
        public HttpClient Client { get; }

        public DefaultHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:3001/api/");
            Client = client;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await Client.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, MedicalWriteDto writeDto)
        {
            var serializedObject = JsonConvert.SerializeObject(writeDto);
            var response = await Client.PostAsync(requestUri, new StringContent(serializedObject, Encoding.Default, "application/json"));
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, MedicalWriteDto writeDto)
        {
            var serializedObject = JsonConvert.SerializeObject(writeDto);
            var response = await Client.PutAsync(requestUri, new StringContent(serializedObject, Encoding.Default, "application/json"));
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            var response = await Client.DeleteAsync(requestUri);
            return response;
        }
    }
}