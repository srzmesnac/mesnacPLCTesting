using HslCommunication.BasicFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
   public class ThreadingTest
    {

        private List<HistoryData> _hdaStr;
      public List<HistoryData> ListData
        {
            get
            {
                return _hdaStr;
            }
        }
        public ThreadingTest()
        {
            _hdaStr = new List<HistoryData>();
        }











        private void AsyncStrStart(int length, string Addr, string IP)
        {
            try
            {

             
               
                LIBnodavePLC _LIBnodavePLC = new LIBnodavePLC();
                bool isopen = _LIBnodavePLC.Init(Addr, IP); ;
                bool check = false;
                string send = "";
                string recive = ""; ;
                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        send = GetRandomString(length);
                        _LIBnodavePLC.WriteBytes(SoftBasic.HexStringToBytes(send));
                        byte[] ReciveData = _LIBnodavePLC.ReadBytes(send.Length / 2);
                        recive = SoftBasic.ByteToHexString(ReciveData);
                        if (send.CompareTo(recive) == 0)
                            check = true;
                        else
                            check = false;
                                            
                        double during = (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds;
                        //logNet2.RecordMessage(HslMessageDegree.DEBUG, null, "耗时" + (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds + "字节数" + send.Length.ToString() + "是否正常" + check);

                        _hdaStr.Add(new HistoryData(during, send.Length, check, DateTime.Now));

                    }



                }
                );
            }
            catch (Exception ex)
            {

               // MessageBox.Show(ex.Message);
            }

        }
            private bool Check(IEnumerable arr1, IEnumerable arr2)
            {
                return (arr1 as IStructuralEquatable).Equals(arr2, StructuralComparisons.StructuralEqualityComparer);
            }

            public  string GetRandomString(int length, bool useNum = true, bool useLow = false, bool useUpp = false)
            {
                byte[] b = new byte[4];
                new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
                Random r = new Random(BitConverter.ToInt32(b, 0));
                string s = null, str = "";
                if (useNum == true) { str += "0123456789"; }
                if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
                if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }

                for (int i = 0; i < length; i++)
                {
                    s += str.Substring(r.Next(0, str.Length - 1), 1);
                }
                return s;
            }


        






















    }
}
