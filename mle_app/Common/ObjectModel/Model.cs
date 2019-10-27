using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using mle_app.Common.ObjectModel.ValidationRule;

namespace mle_app.Common.ObjectModel
{
    public abstract class Model : NotifyBase, IDataErrorInfo
    {
        public virtual string Error => null;

        public virtual string this[string columnName]
        {
            get
            {
                var errors = GetValidationRules(columnName)
                                .Where(v => !v.IsValid(this, columnName))
                                .Select(v => v.ErrorMessage);
                return errors.Count() > 0 ? string.Join(Environment.NewLine, errors) : null;
            }
        }

        protected Attribute[] GetAttributes(string propertyName)
        {
            var prop = this.GetType().GetProperty(propertyName);
            return Attribute.GetCustomAttributes(prop);
        }

        protected IEnumerable<ValidationRuleAttribute> GetValidationRules(string propertyName)
        {

            var rules = new List<ValidationRuleAttribute>();
            rules.AddRange(GetAttributes(propertyName)
                .Where(a => typeof(ValidationRuleAttribute)
                .IsAssignableFrom(a.GetType()))
                .Select(a => a as ValidationRuleAttribute));

            var qvrs = this.QueryValidationRules(propertyName);
            if (qvrs != null) rules.AddRange(qvrs);


            return rules;
        }
    }
}
