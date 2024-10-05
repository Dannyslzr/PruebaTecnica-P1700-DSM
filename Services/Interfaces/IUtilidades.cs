namespace Services.Interfaces
{
    public interface IUtilidades
    {
        Task<DateTime> ObtenerFecha();
        Task<string> EncriptaString(string psw);
        Task<string> DesencriptaString(string psw);
        Task<bool> EsCorreoValido(string email);
    }
}
