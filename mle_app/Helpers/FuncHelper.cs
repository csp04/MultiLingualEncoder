using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mle_app.Helpers
{
    public static class FuncHelper<T>
    {
        public static event EventHandler<T> Return;
        private static Func<T> current;

        private static AsyncCallback CallBack = ar =>
        {
            var func = ar.AsyncState as Func<T>;

            if(ReferenceEquals(current, func))
            {
                var result = func.EndInvoke(ar);
                Return?.Invoke(func, result);
            }
        };
       

        public static void Race(Func<T> func)
        {
            current = func;
            func.BeginInvoke(CallBack, func);
        }
       
    }
}
