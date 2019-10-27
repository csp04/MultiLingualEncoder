using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mle_app
{
    public static class ListUtils
    {
        public static void Each(this IEnumerable items, Action<object> fn)
        {
            foreach (var item in items)
            {
                fn(item);
            }
        }

        public static void Each<T>(this IEnumerable<T> items, Action<T> fn)
        {
            foreach (var item in items)
            {
                fn(item);
            }
        }

        public static void Each(this IEnumerable items, Action<object, int> fn)
        {
            int index = 0;
            foreach (var item in items)
            {
                fn(item, index++);
            }
        }

        public static void Each<T>(this IEnumerable<T> items, Action<T, int> fn)
        {
            int index = 0;
            foreach (var item in items)
            {
                fn(item, index++);
            }
        }

        public static void Each(this IList items, Action<object> fn)
        {
            foreach (var item in items)
            {
                fn(item);
            }
        }

        public static void Each<T>(this IList<T> items, Action<T> fn)
        {
            foreach (var item in items)
            {
                fn(item);
            }
        }

        public static void Each(this IList items, Action<object, int> fn)
        {
            int index = 0;
            foreach (var item in items)
            {
                fn(item, index++);
            }
        }

        public static void Each<T>(this IList<T> items, Action<T, int> fn)
        {
            int index = 0;
            foreach (var item in items)
            {
                fn(item, index++);
            }
        }

        public static void Each(this ICollection items, Action<object> fn)
        {
            foreach (var item in items)
            {
                fn(item);
            }
        }

        public static void Each<T>(this ICollection<T> items, Action<T> fn)
        {
            foreach (var item in items)
            {
                fn(item);
            }
        }

        public static void Each(this ICollection items, Action<object, int> fn)
        {
            int index = 0;
            foreach (var item in items)
            {
                fn(item, index++);
            }
        }

        public static void Each<T>(this ICollection<T> items, Action<T, int> fn)
        {
            int index = 0;
            foreach (var item in items)
            {
                fn(item, index++);
            }
        }
    }
}
