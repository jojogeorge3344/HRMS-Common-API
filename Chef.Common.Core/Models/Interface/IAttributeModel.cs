namespace Chef.Common.Core
{
    public interface IAttributeModel : IModel
    {
        public string AttributeName { set; get; }
        public string AttributeValue { set; get; }
    }
}
