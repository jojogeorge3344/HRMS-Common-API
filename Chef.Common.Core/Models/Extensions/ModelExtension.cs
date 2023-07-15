namespace Chef.Common.Core;

//TODO: This class should be removed.
public static class ModelExtension
{
    public static string TableName(this Model model)
    {
        System.Type type = model.GetType();
        string schemaName = type.Namespace.Split('.')[1].ToLower();
        return schemaName + "." + type.Name.ToLower();
    }

    public static string TableNameWOSchema(this Model model)
    {
        return model.GetType().Name.ToLower();
    }

    public static string SchemaName(this Model model)
    {
        return model.GetType().Namespace.Split('.')[1].ToLower();
    }
}
