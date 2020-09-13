﻿namespace Chef.Common.Repositories
{
    public struct Column
    {
        public string ForeignKey;
        public bool IsKey;
        public bool IsRequired;
        public bool IsWrite;
        public bool IsSkip;
        public bool IsUnique;
        public string Name;
        public string Type;
    }
}