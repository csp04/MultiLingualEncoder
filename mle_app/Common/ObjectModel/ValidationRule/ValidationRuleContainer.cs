using System;
using System.Collections.Generic;


namespace mle_app.Common.ObjectModel.ValidationRule
{
    public static class ValidationRuleContainer
    {
        /// <summary>
        /// To hold the validation rules with tag
        /// </summary>
        private class PropTagVr : Dictionary<string, List<ValidationRuleAttribute>>
        {
        }

        private class VrContainer : Dictionary<Type, PropTagVr>
        { }


        private static VrContainer m_container = new VrContainer();

        public static void AddCustomRule<T>(string propertyName, params ValidationRuleAttribute[] rule) where T : Model
        {

            Type _type = typeof(T).GetType();

            if (!m_container.TryGetValue(_type, out PropTagVr _tagvr))
            {
                _tagvr = new PropTagVr();
                m_container.Add(_type, _tagvr);
            }

            List<ValidationRuleAttribute> vrList;

            if (!_tagvr.TryGetValue(propertyName, out vrList))
            {
                vrList = new List<ValidationRuleAttribute>();
                _tagvr.Add(propertyName, vrList);
            }

            _tagvr[propertyName].AddRange(rule);
        }

        public static bool RemoveCustomRule<T>(string propertyName) where T : Model
        {

            Type _type = typeof(T).GetType();

            if (m_container.TryGetValue(_type, out PropTagVr _tagvr))
            {
                return _tagvr.Remove(propertyName);
            }

            return false;
        }

        internal static List<ValidationRuleAttribute> QueryValidationRules<T>(this T model, string propertyName) where T : Model
        {


            Type _type = typeof(T).GetType();

            if (m_container.TryGetValue(_type, out PropTagVr _tagvr))
            {
                if (_tagvr.TryGetValue(propertyName, out List<ValidationRuleAttribute> vrList))
                    return vrList;
            }

            return null;
        }
    }
}
