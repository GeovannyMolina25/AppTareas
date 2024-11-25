using AppTareas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace AppTareas.Service
{
    public class LoginServicio : Controller
    {
        public string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la contraseña en bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir el array de bytes en una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (byte byteValue in bytes)
                {
                    builder.Append(byteValue.ToString("x2"));
                }

                // Retornar el hash de la contraseña como un string hexadecimal
                return builder.ToString().Substring(0, 24);  // Devolver solo los primeros 24 caracteres
            }
        }

        // Método para verificar la contraseña
        public bool VerifyPassword(string hashedPassword, string password)
        {
            // Generar el hash de la contraseña proporcionada
            string hashToVerify = EncryptPassword(password);

            // Comparar los hashes
            return hashToVerify == hashedPassword;
        }


    }
}
