using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    class MockRandom : Random
    {
        public int _i;
        public int _maxValue;

        public MockRandom()
        {
            _i = 0;
        }

        // 覆寫 Random 的 Next
        public override int Next(int maxValue)
        {
            _maxValue = maxValue;
            return _i++;
        }
    }
}
