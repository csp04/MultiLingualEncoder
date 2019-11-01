using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace mle_app
{
    public class ProducerConsumer<T> : IDisposable
    {
        private readonly Queue<T> m_queue;

        public EventHandler<T> Consuming;
        private bool stop = false;

        public bool AlwaysConsumeLastItem { get; set; } = false;

        private void onConsuming(T item)
        {
            Consuming?.Invoke(this, item);
        }

        public ProducerConsumer()
        {
            m_queue = new Queue<T>();

            Task.Run(() => consume());
        }

        public void Produce(T item)
        {
            lock (m_queue)
            {
                m_queue.Enqueue(item);
                Monitor.Pulse(m_queue);
            }
        }

        private void consume()
        {
            while (true)
            {
                lock (m_queue)
                    if (m_queue.Count == 0)
                        Monitor.Wait(m_queue);

                if (stop) break;

                if (AlwaysConsumeLastItem)
                {
                    lock (m_queue)
                        while (m_queue.Count > 1)
                            m_queue.Dequeue();
                }
                onConsuming(m_queue.Dequeue());
            }
        }

        public void Dispose()
        {
            stop = true;

            lock (m_queue)
                Monitor.Pulse(m_queue);
        }
    }
}
