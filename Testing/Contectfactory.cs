using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
  public  class Contectfactory
    {
        LIBnodavePLC _libnodavePLC;
      

      public  Contectfactory(LIBnodavePLC lI)
        {
            this._libnodavePLC = lI;


        }


        //public void ReadData()
        //{

        //    switch (_libnodavePLC.dataTyte)
        //    {
        //        case  comm.DataTyte.DWORD:
        //            _libnodavePLC.WriteInt32s(data);
        //            break;
        //        case comm.DataTyte.WORD:

        //            break;
        //        case comm.DataTyte.FLOAT:

        //            break;
        //        case comm.DataTyte.STR:

        //            break;
        //    }



        //}

        private Type GetType<T>(T t)
        {
            return t.GetType();
        }
    }
}
