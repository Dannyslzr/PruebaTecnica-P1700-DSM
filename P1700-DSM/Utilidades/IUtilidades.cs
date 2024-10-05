using Models.Dtos.Results;

namespace Web.Utilidades
{
    public interface IUtilidades
    {
        Task<Result<TModel>> GetAsync<TModel>(string url, string token);
        Task<Result<TModel>> PostItemGetItem<T, TModel>(string url, T modelToApi, string token);
        Task<Result<IEnumerable<TModel>>> PostItemGetArray<T, TModel>(string url, T modelToApi, string token);
    }
}
