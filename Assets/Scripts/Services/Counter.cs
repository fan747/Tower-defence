using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Services
{
    public class Counter
    {
        private int _count = 0;
        public int Count => _count;

        public bool IsNoCount => _count < 1;

        public void AddCount(int count)
        {
            if (count > 0)
            {
                _count += count;
            }
        }

        public void RemoveOneCount()
        {
            _count--;
        }
    }
}
