using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EnumTest
{
    public class Color : IEnumerator<KeyValuePair<string, int>>, IEnumerable<KeyValuePair<string, int>>
    {
        private Dictionary<String, int> _Color;
        private int index = -1;



        public Color()
        {
            _Color = new Dictionary<string, int>();
            _Color.Add("red", 0);
            _Color.Add("green", 1);
            _Color.Add("purple", 2);
            _Color.Add("orange", 3);
        }

        public KeyValuePair<string, int> Current
        {
            get
            {
                return  _Color.ElementAt(index);
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return _Color.ElementAt(index);
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }

        public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (index<_Color.Count-1)
            {
                index =index+1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            index = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public int this[string name]
        {
            get
            {
                return _Color[name];
            }
        }
        public KeyValuePair<string, int> this[int index]
        {
            get
            {
                if (index > 0 && index < _Color.Count)
                {
                    return _Color.ElementAt(index);
                }
                else
                {
                    throw new  IndexOutOfRangeException();
                }
            }
        }
    }
}
