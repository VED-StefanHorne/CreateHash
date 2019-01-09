using System;
using System.Security.Cryptography;
using System.Text;

namespace CreatePasswordHash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CreatePasswordHash(args[0]));
        }

        public static string CreatePasswordHash(string password)
        {
            string prehashedPassword = GetMd5Hash(password).ToUpper(); // required for compatibility with existing password scheme (uppercase MD5)

            return PasswordHash.PasswordHash.CreateHash(prehashedPassword);
        }

        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
