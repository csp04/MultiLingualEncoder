using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

/* * * * * * * * * * * * * * * * * * * * * * * * * * *
 * For lower net framework version, uncomment this..
 * * * * * * * * * * * * * * * * * * * * * * * * * * *
namespace System.Runtime.CompilerServices
{
    public class CallerMemberNameAttribute : Attribute { }
}
 * * * * * * * * * * * * * * * * * * * * * * * * * * */

namespace mle_app.Common
{
    public abstract class NotifyBase : INotifyPropertyChanged
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();
        ~NotifyBase() { _properties = null; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        protected T Get<T>([CallerMemberName] string memberName = null)
        {
            _properties.TryGetValue(memberName, out object o);

            return (T)o;
        }

        protected void Set<T>(T value, [CallerMemberName] string memberName = null)
        {
            _properties.TryGetValue(memberName, out object o);
            if (ReferenceEquals(o, value)) return;

            _properties[memberName] = value;
            OnPropertyChanged(memberName);
        }

    }
}
