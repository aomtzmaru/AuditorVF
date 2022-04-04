using System;

namespace api.Dtos
{
    public class FileForRequest
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string FileStream { get; set; }
        public string FileId { get; set; }
        public string EncryptFileName { get; set; }
        public int Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIp { get; set; }
        public string CreatedUser { get; set; }

        public FileForRequest()
        {
            CreatedDate = DateTime.Now;
            Deleted = 0;
        }
    }
}