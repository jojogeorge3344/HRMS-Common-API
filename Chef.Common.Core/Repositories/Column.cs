namespace Chef.Common.Repositories
{
    public struct Column
    {
        public string ForeignKey;
        public string ForeignKeyReference;
        public bool IsKey;
        public bool IsRequired;
        public bool IsWrite;
        public bool IsSkip;
        public bool IsUnique;
        public string Name;
        public string Type;
        public bool IsFromBase;
    }
}