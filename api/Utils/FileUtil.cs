using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using api.Dtos;

namespace api.Utils
{
    public class FileUtil
    {
        static FileUtil()
        {
            
        }
        public static string SaveFile(FileForRequest file, string username)
        {
            var folderName = Path.Combine("Resources", "Files", username);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var filePath = Path.Combine(pathToSave, file.FileName);

            DirectoryInfo info = new DirectoryInfo(pathToSave);
            if (!info.Exists) {
                info.Create();
            }

            FileInfo fileExist = new FileInfo(filePath);
            if (!fileExist.Exists) {
                byte[] byteArray = Convert.FromBase64String(file.FileStream);
                Stream stream = new MemoryStream(byteArray);
                using(FileStream outputFileStream = new FileStream(filePath, FileMode.Create)) {
                    stream.CopyTo(outputFileStream);
                }
            }

            return folderName;
        }

        public static string EncryptFile(string password, string destFileName, byte[] fileContent, string username)
        {
            string folderName = Path.Combine("PrivateResources", "Files", username);
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            DirectoryInfo info = new DirectoryInfo(fullPath);
            if (!info.Exists)
            {
                info.Create();
            }

            string pathToDestFile = Path.Combine(fullPath, destFileName);
            FileInfo fileExist = new FileInfo(pathToDestFile);
            if (!fileExist.Exists) {
                Aes256 aes256 = new Aes256(password);
                byte[] result = aes256.Encrypt(fileContent);
                File.WriteAllBytes(pathToDestFile, result);
            }
            return destFileName;
        }

        public static string DecryptFile(string password, string decryptedFileName, string encryptedFileName)
        {
            string folderName = Path.Combine("PrivateResources", "Files");
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            string randomFolder = HashFile(DateTime.Now.ToString("yyyyMMddHHmmss") + decryptedFileName + encryptedFileName);
            string destFolderName = Path.Combine("Files", "Temp", randomFolder);
            string destFullPath = Path.Combine(Directory.GetCurrentDirectory(), destFolderName);
            string pathToDestFile = Path.Combine(destFullPath, decryptedFileName);
            //create destination folder
            DirectoryInfo info = new DirectoryInfo(destFullPath);
            if (!info.Exists) {
                info.Create();
            }

            string pathToSourceFile = Path.Combine(fullPath, encryptedFileName);
            FileInfo fileExist = new FileInfo(pathToSourceFile);
            if (fileExist.Exists)
            {
                // Decrypt
                byte[] fileContent = File.ReadAllBytes(pathToSourceFile);
                Aes256 aes256 = new Aes256(password);
                byte[] result = aes256.Decrypt(fileContent);
                File.WriteAllBytes(pathToDestFile, result);
            }
            return destFolderName + "/" + decryptedFileName;
        }

        public static string HashFile(string data)
        {
            MD5 md5 = MD5.Create();
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(data);
            byte[] hash = md5.ComputeHash(tmpSource);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static string HashFileContent(Stream fileStream)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(fileStream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        
    }
}