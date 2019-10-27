

namespace mle_app.Common.ObjectModel.ValidationRule
{
    public class RequiredAttribute : ValidationRuleAttribute
    {
        public RequiredAttribute(string ErrorMessage)
            : base(ErrorMessage)
        {
        }
        public RequiredAttribute() : base("This is a required field.") { }

        public override bool IsValid(object obj, string propertyName)
        {
            var value = Utils.GetPropertyValue(obj, propertyName);
            return value != null && value.ToString() != "";
        }
    }
}
