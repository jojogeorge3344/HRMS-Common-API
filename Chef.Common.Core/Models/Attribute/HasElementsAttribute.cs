using System.Collections;

namespace Chef.Common.Core
{
    public class HasElementsAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection;

            if (collection != null)
                return collection.Count > 0;

            if (value is IEnumerable enumerable)
                return enumerable.GetEnumerator().MoveNext();

            return false;
        }
    }
}
