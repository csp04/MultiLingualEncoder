using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mle_app.Common
{
    public static class Mapper
    {

        public static TSource MapTo<TSource, TTarget>(this TSource source, TTarget target)
        {

            var src_props = TypeDescriptor.GetProperties(typeof(TSource)).Cast<PropertyDescriptor>();
            var trg_props = TypeDescriptor.GetProperties(typeof(TTarget)).Cast<PropertyDescriptor>();

            src_props
                .Each(src_pd =>
               {
                   trg_props
                   .Each(trg_pd =>
                   {
                       if (src_pd.Name == trg_pd.Name &&
                            src_pd.PropertyType == trg_pd.PropertyType &&
                            target.IsPropertySettable(trg_pd.Name))
                       {
                           var v = src_pd.GetValue(source);

                           trg_pd.SetValue(target, Convert.ChangeType(v, trg_pd.PropertyType));
                           return;
                       }
                   });
               });

            return source;
        }
    }
}
