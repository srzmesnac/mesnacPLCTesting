using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
  public  struct HistoryData
    {
      
        public double  During { set; get; }
        public int Lenth { set; get; }
        public bool Value { set; get; }
        public DateTime TimeStamp { set; get; }



        public HistoryData(double During, int lenth, bool value, DateTime timeStamp)
        {       
            this.During = During;
            this.Lenth = lenth;
            Value = value;
            TimeStamp = timeStamp;
        }
    }


}
