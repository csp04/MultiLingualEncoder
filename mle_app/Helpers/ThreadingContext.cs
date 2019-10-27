using System;
using System.Threading;
using System.Windows.Threading;

namespace mle_app.ThreadSafe
{

    public interface IThreadContext
    {

        bool IsSynchronized();

        void Invoke(Delegate action, params object[] args);

        void BeginInvoke(Delegate action, params object[] args);

        void AsyncInvoke(Delegate action, params object[] args);

        void AsyncBeginInvoke(Delegate action, params object[] args);

    }


    public sealed class SyncContext : IThreadContext
    {

        private SynchronizationContext _syncContext;

        private SyncContext()
        {
            _syncContext = SynchronizationContext.Current;
        }

        public void BeginInvoke(System.Delegate action, params object[] args)
        {
            _syncContext.Post(new SendOrPostCallback((o) => action.DynamicInvoke(args)), null);
        }

        public void Invoke(System.Delegate action, params object[] args)
        {
            _syncContext.Send(new SendOrPostCallback((o) => action.DynamicInvoke(args)), null);
        }

        public bool IsSynchronized()
        {
            return ReferenceEquals(_syncContext, SynchronizationContext.Current);
        }

        public void AsyncBeginInvoke(System.Delegate action, params object[] args)
        {
            if (this.IsSynchronized())
            {
                action.DynamicInvoke(args);
            }
            else
            {
                this.BeginInvoke(action, args);
            }

        }

        public void AsyncInvoke(System.Delegate action, params object[] args)
        {
            if (this.IsSynchronized())
            {
                action.DynamicInvoke(args);
            }
            else
            {
                this.Invoke(action, args);
            }

        }

        public static IThreadContext Create() => new SyncContext();
    }

    public sealed class WpfContext : IThreadContext
    {

        private Dispatcher _dispatcher;

        private WpfContext()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public void BeginInvoke(System.Delegate action, params object[] args)
        {
            _dispatcher.BeginInvoke(action, args);
        }

        public void Invoke(System.Delegate action, params object[] args)
        {
            _dispatcher.Invoke(action, args);
        }

        public bool IsSynchronized()
        {
            return _dispatcher.CheckAccess();
        }

        public void AsyncBeginInvoke(System.Delegate action, params object[] args)
        {
            if (this.IsSynchronized())
            {
                action.DynamicInvoke(args);
            }
            else
            {
                this.BeginInvoke(action, args);
            }

        }

        public void AsyncInvoke(System.Delegate action, params object[] args)
        {
            if (this.IsSynchronized())
            {
                action.DynamicInvoke(args);
            }
            else
            {
                this.Invoke(action, args);
            }

        }

        public static IThreadContext Create() => new WpfContext();
    }

    public static class ThreadContextExtensions
    {
        public static void Invoke(this IThreadContext context, Action fn)
        {
            context.Invoke(fn.CastTo<Delegate>());
        }

        public static void Invoke<T>(this IThreadContext context, Action<T> fn, T arg)
        {
            context.Invoke(fn.CastTo<Delegate>(), arg);
        }

        public static void BeginInvoke(this IThreadContext context, Action fn)
        {
            context.BeginInvoke(fn.CastTo<Delegate>());
        }

        public static void BeginInvoke<T>(this IThreadContext context, Action<T> fn, T arg)
        {
            context.BeginInvoke(fn.CastTo<Delegate>(), arg);
        }

        public static void AsyncInvoke(this IThreadContext context, Action fn)
        {
            context.AsyncInvoke(fn.CastTo<Delegate>());
        }

        public static void AsyncInvoke<T>(this IThreadContext context, Action<T> fn, T arg)
        {
            context.AsyncInvoke(fn.CastTo<Delegate>(), arg);
        }

        public static void AsyncBeginInvoke(this IThreadContext context, Action fn)
        {
            context.AsyncBeginInvoke(fn.CastTo<Delegate>());
        }

        public static void AsyncBeginInvoke<T>(this IThreadContext context, Action<T> fn, T arg)
        {
            context.AsyncBeginInvoke(fn.CastTo<Delegate>(), arg);
        }
    }

}