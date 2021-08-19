using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Objects
{
    class GenericQueue<T>
    {
        private ConcurrentQueue<T> Queue = new ConcurrentQueue<T>();

        public T m_pAdd
        {
            set { Queue.Enqueue(value); }
        }

        public bool m_bAvailable
        {
            get { return !Queue.IsEmpty; }
        }

        public T m_pNext
        {
            get
            {
                Queue.TryDequeue(out T result);
                return result;
            }

        }


    }
}
