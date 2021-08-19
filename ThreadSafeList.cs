using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal
{
    class ThreadSafeList<T>
    {
        private List<T> _list;
        private object _sync = new object();

        public ThreadSafeList(int MaximumEntries = 0)
        {
            if (MaximumEntries > 0)
                _list = new List<T>(MaximumEntries);
            else
                _list = new List<T>();
        }

        public void ForEach(Action<T> Func)
        {
            lock (_sync)
                _list.ForEach(Func);
        }


        public void Add(T val)
        {
            lock (_sync)
                _list.Add(val);
        }

        public void AddRange(T[] values)
        {
            lock (_sync)
                _list.AddRange(values);
        }

        public T GetLast()
        {
            T next;
            lock (_sync)
                next = _list.Last();
            Remove(next);
            return next;

        }

        public T GetFirst()
        {
            T first;
            lock (_sync)
                first = _list.FirstOrDefault();
            Remove(first);
            return first;
        }

        public void Push(T obj)
        {
            lock (_sync)
                _list.Insert(0, obj);
        }

        public void Remove(T obj)
        {
            lock (_sync)
            {
                _list.Remove(obj);
            }
        }

        public int GetIndex(T obj)
        {
            int index = 0;
            lock (_sync)
                index = _list.IndexOf(obj);
            return index;
        }

        public bool Exists(T obj)
        {
            bool e = false;
            lock (_sync)
                e = _list.Contains(obj);
            return e;
        }

        public T ElementAt(int index)
        {
            T elemAt;
            lock (_sync)
                elemAt = _list.ElementAt(index);
            return elemAt;
        }


        public int Count()
        {
            int num = 0;
            lock (_sync)
                num = _list.Count;
            return num;
        }

        public T[] ToArray()
        {
            T[] newArray;
            lock (_sync)
                newArray = _list.ToArray();
            return newArray;
        }
        public List<T> ToList()
        {
            List<T> newArray;
            lock (_sync)
                newArray = _list.ToList();
            return newArray;
        }
        public void Clear()
        {
            lock (_sync)
                _list.Clear();
        }
    }
}
