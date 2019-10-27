using System;


namespace mle_app.Common.ObjectModel.ValidationRule
{
    [AttributeUsage(validOn: AttributeTargets.Property, AllowMultiple = true)]
    public class ValidationRuleAttribute : Attribute
    {
        public string ErrorMessage { get; set; }

        public ValidationRuleAttribute(string ErrorMessage)
        {
            this.ErrorMessage = ErrorMessage;

        }

        public virtual bool IsValid(object obj, string propertyName)
        {
            return true;
        }


    }
}
