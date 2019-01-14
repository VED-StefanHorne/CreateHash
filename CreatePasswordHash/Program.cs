using System;
using System.Security.Cryptography;
using System.Text;

namespace CreatePasswordHash
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0].ToString().ToLower() == "h")
            {
                DisplayHelp();
                return;
            }

            if (args[0].ToString().ToLower() == "md5")
            {
                Console.WriteLine(GetMd5Hash(args[1]));
                return;
            }

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

        public static void DisplayHelp()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Usage:\n");
            Console.WriteLine("Use flag 'md5' for MD5 hash. e.g. CreatePasswordHash md5 Testing123");
            Console.WriteLine("No flag for default (MD5 -> PBKDF2). e.g. CreatePasswordHash Testing123");
        }
    }
}
