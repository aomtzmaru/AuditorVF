using System;

namespace api.Dtos
{
    public class FileForReturn
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}