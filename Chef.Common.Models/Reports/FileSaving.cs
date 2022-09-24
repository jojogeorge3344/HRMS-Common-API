using System;

namespace Chef.Common.Models
{
    [Serializable]
    public class FileSaving
    {
        /// <summary>
        /// Saving the byte array data from report viewer
        /// </summary>
        public byte[] ByteData { get; set; }

        /// <summary>
        /// Unique name which can be given to the report name while saving pdf
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id from database
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Module name or name of the report. Folders are created according to this name
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// true: when the file is given for approval, while viewing it is showed in preview
        /// false: supporting documents. viewing is showed as link
        /// </summary>
        public bool IsPrintPreview { get; set; } = false;
    }
}
