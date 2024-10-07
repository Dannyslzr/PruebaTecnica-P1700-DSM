using Models.Dtos.Results;
using Newtonsoft.Json;
using System.Text;
using Web.Utilidades;

namespace GST.Web.Utilidades
{
    public class UtilidadesWeb : IUtilidades
    {
        private readonly HttpClient _client = new HttpClient();
        //private readonly ILocalStorageService _localStorage;

        public async Task<Result<TModel>> GetAsync<TModel>(string url, string token)
        {
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<Result<TModel>>(response);
            return result;
        }

        /// <summary>
        /// Realiza una POST al API y envia un DTO como modelo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TModel">DTO que devuelve el API</typeparam>
        /// <param name="url">URL del API</param>
        /// <param name="modelToApi">DTO enviado al API</param>
        /// <returns>Retonar un DTO "TModel"</returns>
        public async Task<Result<TModel>> PostItemGetItem<T, TModel>(string url, T modelToApi, string token)
        {
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var serializedItem = JsonConvert.SerializeObject(modelToApi);
            var response = await _client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            var resString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<TModel>>(resString);

            return result;
        }

        /// <summary>
        /// Realiza una POST al API y envia un DTO como modelo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TModel">DTO que devuelve el API</typeparam>
        /// <param name="url">URL del API</param>
        /// <param name="modelToApi">DTO enviado al API</param>
        /// <returns>Retonar una Lista DTO "TModel"</returns>
        public async Task<Result<IEnumerable<TModel>>> PostItemGetArray<T, TModel>(string url, T modelToApi, string token)
        {
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var serializedItem = JsonConvert.SerializeObject(modelToApi);
            var response = await _client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            var resString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<IEnumerable<TModel>>>(resString);

            return result;
        }
    }
}
