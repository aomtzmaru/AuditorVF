using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace api.Utils
{
    public class Aes256
    {
        private readonly IConfiguration _config;
        private readonly byte[] _key;
        private readonly byte[] _authKey;

        public Aes256(string masterKey)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            _config = builder.Build();

            if (string.IsNullOrEmpty(masterKey))
                throw new ArgumentException("masterKey can not be null or empty.");

            byte[] salt = Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Salt").Value);
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(masterKey, salt, 50000))
            {
                this._key = rfc2898DeriveBytes.GetBytes(32);
                this._authKey = rfc2898DeriveBytes.GetBytes(64);
            }
        }

        public string Encrypt(string input) => Convert.ToBase64String(this.Encrypt(Encoding.UTF8.GetBytes(input)));

        public byte[] Encrypt(byte[] input)
        {
            if (input == null)
                throw new ArgumentNullException("input can not be null.");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Position = 32L;
                using (AesCryptoServiceProvider cryptoServiceProvider = new AesCryptoServiceProvider())
                {
                    cryptoServiceProvider.KeySize = 256;
                    cryptoServiceProvider.BlockSize = 128;
                    cryptoServiceProvider.Mode = CipherMode.CBC;
                    cryptoServiceProvider.Padding = PaddingMode.PKCS7;
                    cryptoServiceProvider.Key = this._key;
                    cryptoServiceProvider.GenerateIV();
                    // cryptoServiceProvider.IV = Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:KeyIV").Value);
                    // cryptoServiceProvider.IV = Encoding.UTF8.GetBytes("5555555555555555");
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        memoryStream.Write(cryptoServiceProvider.IV, 0, cryptoServiceProvider.IV.Length);
                        cryptoStream.Write(input, 0, input.Length);
                        cryptoStream.FlushFinalBlock();
                        using (HMACSHA256 hmacshA256 = new HMACSHA256(this._authKey))
                        {
                            byte[] hash = hmacshA256.ComputeHash(memoryStream.ToArray(), 32, memoryStream.ToArray().Length - 32);
                            memoryStream.Position = 0L;
                            memoryStream.Write(hash, 0, hash.Length);
                        }
                    }
                }
                return memoryStream.ToArray();
            }
        }

        public string Decrypt(string input) => Encoding.UTF8.GetString(this.Decrypt(Convert.FromBase64String(input)));

        public byte[] Decrypt(byte[] input)
        {
            if (input == null)
                throw new ArgumentNullException("input can not be null.");
            using (MemoryStream memoryStream = new MemoryStream(input))
            {
                using (AesCryptoServiceProvider cryptoServiceProvider = new AesCryptoServiceProvider())
                {
                    cryptoServiceProvider.KeySize = 256;
                    cryptoServiceProvider.BlockSize = 128;
                    cryptoServiceProvider.Mode = CipherMode.CBC;
                    cryptoServiceProvider.Padding = PaddingMode.PKCS7;
                    cryptoServiceProvider.Key = this._key;
                    using (HMACSHA256 hmacshA256 = new HMACSHA256(this._authKey))
                    {
                        byte[] hash = hmacshA256.ComputeHash(memoryStream.ToArray(), 32, memoryStream.ToArray().Length - 32);
                        byte[] numArray = new byte[32];
                        memoryStream.Read(numArray, 0, numArray.Length);
                        if (!this.AreEqual(hash, numArray))
                            throw new CryptographicException("Invalid message authentication code (MAC).");
                    }
                    byte[] buffer1 = new byte[16];
                    memoryStream.Read(buffer1, 0, 16);
                    cryptoServiceProvider.IV = buffer1;
                    // cryptoServiceProvider.IV = Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:KeyIV").Value);
                    // cryptoServiceProvider.IV = Encoding.UTF8.GetBytes("5555555555555555");
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] buffer2 = new byte[memoryStream.Length - 16L + 1L];
                        byte[] numArray = new byte[cryptoStream.Read(buffer2, 0, buffer2.Length)];
                        Buffer.BlockCopy((Array)buffer2, 0, (Array)numArray, 0, numArray.Length);
                        return numArray;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private bool AreEqual(byte[] a1, byte[] a2)
        {
            bool flag = true;
            for (int index = 0; index < a1.Length; ++index)
            {
                if ((int)a1[index] != (int)a2[index])
                    flag = false;
            }
            return flag;
        }
    }
}