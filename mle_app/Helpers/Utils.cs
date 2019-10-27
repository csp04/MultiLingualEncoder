using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mle_app
{
    public static partial class Utils
    {
        public static double ToDouble(this object o) => Convert.ToDouble(o);

        public static int ToInt(this object o) => Convert.ToInt32(o);

        public static short ToShort(this object o) => Convert.ToInt16(o);

        public static long ToLong(this object o) => Convert.ToInt64(o);

        public static object ToObject(this object o) => o;

        public static byte ToByte(this object o) => Convert.ToByte(o);

        public static byte[] ToBytes(this string value, string encoding = "utf-8") => ASCIIEncoding.GetEncoding(encoding).GetBytes(value);

        public static string FromBytesToString(this byte[] value, string encoding = "utf-8") => ASCIIEncoding.GetEncoding(encoding).GetString(value);

        public static T CastTo<T>(this object o) => (T)o;

        public static object GetPropertyValue(object obj, string propertyName, object[] index = null) => obj.GetType().GetProperty(propertyName).GetValue(obj, index);

        public static void SetPropertyValue(object obj, string propertyName, object value, object[] index = null) => obj.GetType().GetProperty(propertyName).SetValue(obj, value, index);

        public static object GetPropertyValue<T>(this T obj, string propertyName, object[] index = null) => GetPropertyValue((object)obj, propertyName, index);

        public static void SetPropertyValue<T>(this T obj, string propertyName, object value, object[] index = null) => SetPropertyValue((object)obj, propertyName, value, index);

        public static bool IsPropertySettable<T>(this T obj, string propertyName) => obj.GetType().GetProperty(propertyName).CanWrite;

        public static bool IsPropertyGettable<T>(this T obj, string propertyName) => obj.GetType().GetProperty(propertyName).CanRead;
    }
}
