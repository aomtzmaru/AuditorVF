using System;

namespace api.Dtos
{
    public class FileForDownload
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FileStream { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string FileId { get; set; }
        public string EncryptFileName { get; set; }
        public string CreatedUser { get; set; }
    }
}