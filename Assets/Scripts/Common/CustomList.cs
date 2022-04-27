using System;

namespace Common
{
    public struct CustomList<T> where T : struct
    {
        private T[] list;

        public int Length => list.Length;
    
        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }
    
        public CustomList(int length)
        {
            list = new T[length];
        }
        
        public void Add(T element)
        {
            var _list = new T[list.Length + 1];
            
            for (var i = 0; i < list.Length; i++)
                _list[i] = list[i];

            _list[_list.Length - 1] = element;
            
            list = _list;
        }

        public void RemoveAt(int index)
        {
            if (index <= list.Length - 1 && index >= 0)
            {
                var _list = new T[list.Length - 1];

                var j = 0;
                
                for (var i = 0; i < list.Length; i++)
                {
                    if (i == index) continue;
                    _list[j] = list[i];
                    j++;
                }

                list = _list;
            }
            else
            {
                throw new Exception("Out of range exception");
            }
        }
    }
}