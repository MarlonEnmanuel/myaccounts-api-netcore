namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NullableAttribute : ValidationAttribute
    {
        public NullableAttribute()
        {
        }

        public override bool IsValid(object? value)
        {
            return true;
        }


    }
}