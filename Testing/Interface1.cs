using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public interface Interface1
    {
        comm.DataTyte dataTyte { get; set; }
        float[] Floats { get; }
        short[] Int16s { get; }
        int[] Int32s { get; }
        string IP { set; }
        bool IsClosed { get; set; }
        bool IsConnect { get; }

        int WriteUint16(ushort value);
    }
}