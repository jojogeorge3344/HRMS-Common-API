using System;

namespace Chef.Common.Models
{
    [Serializable]
    public class DocumentFile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public byte[] Content { get; set; }

        public bool IsPrintPreview { get; set; } = false;
    }
}
