using Services.Interfaces;
using Services.UnitOfWork;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.Servicios
{
    public class UtilidadesService : IUtilidades
    {

        private static readonly string key = "A@_54?¿{&v1434PQRSTU=3+Ybcxwc.>;4ªwxyz";
        private readonly IUnitOfWork _unitOfWork;

        public UtilidadesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> DesencriptaString(string psw)
        {
            byte[] keyArray;

            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar = Convert.FromBase64String(psw);

            //se llama a las clases que tienen los algoritmos de encriptación se le aplica hashing algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray; tdes.Mode = CipherMode.ECB; tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length); tdes.Clear();

            tdes.Clear();

            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public async Task<string> EncriptaString(string psw)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;

            //arreglo de bytes donde guardaremos el texto que vamos a encriptar
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(psw);

            //se utilizan las clases de encriptación //provistas por el Framework
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            //se guarda la llave para que se le realice //hashing
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            //Algoritmo 3DAS 
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray; tdes.Mode = CipherMode.ECB; tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la cadena cifrada
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();

            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }

        public async Task<bool> EsCorreoValido(string email)
        {
            // Expresión regular para validar correos electrónicos
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Verificar si el email coincide con el patrón
            bool isValid = Regex.IsMatch(email, pattern);

            return isValid;
        }

        public async Task<DateTime> ObtenerFecha()
        {
            return DateTime.Now;
        }
    }
}
