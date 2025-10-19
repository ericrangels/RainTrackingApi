using System.ComponentModel.DataAnnotations;

namespace RainTrackingApi.Validation
{
    public class NotEmptyOrWhitespaceAttribute : ValidationAttribute
    {
        public NotEmptyOrWhitespaceAttribute()
        {
            ErrorMessage = "The field must contain a non-empty, non-whitespace value.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            if (value is string s) return !string.IsNullOrWhiteSpace(s);
            return true;
        }
    }
}
