using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    interface IPLC
    {
        bool Init(string ADD, string IP);

         string Address { set; get; }
       IEnumerable ReadBuff { set; get; }
         bool IsConnect { get;  }  
         string ErrorCode { get; set; }
         DateTime Sendtime
        {
            get;

        }
         DateTime Readtime
        {
            get;
        }

         string IP
        {
            get;set;

        }
         bool IsClosed
        {
         set;get;
        }
        bool Connect();
        void Dispose();
    
        int ReadUInt16s(int length);
        int WriteUInt16s(ushort[] v);
        int WriteInt16s(short[] v);
        int ReadInt16s(int length);
        int ReadFloats(int length);
        int WriteFloats(float[] v);
        int WriteInt32s(int[] v);
        int ReadInt32s(int length);
        byte[] ReadBytes(int v);
        int ReadBytesResult(int dataLenth);
        int WriteBytes(byte[] a, bool Isstart = false);
    }
}
