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
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte byteValue in bytes)
                {
                    builder.Append(byteValue.ToString("x2"));
                }
                return builder.ToString().Substring(0, 24);  
            }
        }
        public bool VerifyPassword(string hashedPassword, string password)
        {
            string hashToVerify = EncryptPassword(password);
            return hashToVerify == hashedPassword;
        }


    }
}
