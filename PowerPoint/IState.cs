using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    interface IState
    {
        void PressPointer();
        
        void MovePointer();

        void ReleasePointer();
    }
}
