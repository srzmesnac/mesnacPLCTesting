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

        private List<HistoryData> _hda;
        public List<HistoryData> ListData
        {
            get
            {
                return _hda;
            }
        }
        private static object _lock = new object();
        private int _DataLength;
        private string Addr;
        private string IP;
        private comm.DataTyte _dataTyte;
        private string _ThreadCount;
        public ThreadingTest(int length, string Addr, string IP, comm.DataTyte dataTyte, string ThreadCount = null)
        {
            this._DataLength = length;
            this.Addr = Addr;
            this.IP = IP;
            _hda = new List<HistoryData>();
            this._ThreadCount = ThreadCount;
            this._dataTyte = dataTyte;
            AsyncStart();
        }

        public List<ThreadingTest> ThreadingTests { set; get; }


        public string ThreadCount
        {
            get
            {
                return _ThreadCount;
            }

            set
            {
                value = _ThreadCount;

            }
        }


        public comm.DataTyte DataTyte
        {
            get
            {
                return _dataTyte;
            }

        }


        private void AsyncStart()
        {
            try
            {
                string Addr = this.Addr;
                string IP = this.IP;
                LIBnodavePLC _LIBnodavePLC = new LIBnodavePLC();
                bool isopen = _LIBnodavePLC.Init(Addr, IP); ;
                bool check = false;
                string send = "";
                string recive = "";
                int length = 0;
                IEnumerable sendBytes;
                Task.Run(() =>
                {
                    while (true)
                    {

                        Thread.Sleep(10);
                        switch (this._dataTyte)
                        {
                            case comm.DataTyte.BOOL:

                                break;
                            case comm.DataTyte.WORD:
                                length = this._DataLength / 2;
                                sendBytes = GetRandomUshort(length);
                                _LIBnodavePLC.WriteInt16s(sendBytes as Int16[]);
                                _LIBnodavePLC.ReadInt16s(length);
                                check = Check(sendBytes, _LIBnodavePLC.Int16s);
                                break;
                            case comm.DataTyte.INT16:
                                length = this._DataLength / 2;
                                sendBytes = GetRandomInt16(length);
                                _LIBnodavePLC.WriteInt16s(sendBytes as Int16[]);
                                _LIBnodavePLC.ReadInt16s(length);
                                check = Check(sendBytes, _LIBnodavePLC.Int16s);


                                break;
                            case comm.DataTyte.DINT32:
                                length = this._DataLength / 4;
                                sendBytes = GetRandomInt32(length);
                                _LIBnodavePLC.WriteInt32s(sendBytes as Int32[]);
                                _LIBnodavePLC.ReadInt32s(length);
                                check = Check(sendBytes, _LIBnodavePLC.Int32s);
                                break;
                            case comm.DataTyte.REAL:
                                length = this._DataLength / 2;
                                sendBytes = GetRandomfloat(length);
                                _LIBnodavePLC.WriteFloats(sendBytes as float[]);
                                _LIBnodavePLC.ReadFloats(length);
                                check = Check(sendBytes, _LIBnodavePLC.Floats);

                                break;
                            case comm.DataTyte.STRING:
                                length = this._DataLength * 2;
                                send = GetRandomString(length);
                                _LIBnodavePLC.WriteBytes(SoftBasic.HexStringToBytes(send));
                                byte[] ReciveData = _LIBnodavePLC.ReadBytes(length / 2);
                                recive = SoftBasic.ByteToHexString(ReciveData);
                                check = send.Equals(recive);

                                break;
                            default:
                                break;
                        }


                        double during = (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds;
                        //logNet2.RecordMessage(HslMessageDegree.DEBUG, null, "耗时" + (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds + "字节数" + send.Length.ToString() + "是否正常" + check);

                        _hda.Add(new HistoryData(during, _DataLength, check, DateTime.Now));

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

        public string GetRandomString(int length, bool useNum = true, bool useLow = false, bool useUpp = false)
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




        private ushort[] GetRandomUshort(int length)
        {

            List<ushort> Listvalues = new List<ushort>();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                ushort a = (ushort)rd.Next(65535);

                Listvalues.Add(a);
            }

            return Listvalues.ToArray();

        }

        private Int32[] GetRandomInt32(int length)
        {

            List<Int32> Listvalues = new List<Int32>();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                Int32 a = rd.Next(-247483648, 2147483647);

                Listvalues.Add(a);
            }

            return Listvalues.ToArray();

        }

        private float[] GetRandomfloat(int length)
        {
            List<float> floats = new List<float>();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                int a = rd.Next(100000);
                float f = (float)(a * 0.01);
                floats.Add(f);
            }

            return floats.ToArray();
        }
        private Int16[] GetRandomInt16(int length)
        {

            List<Int16> Listvalues = new List<Int16>();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                int a = rd.Next(-32768, 32767);
                Int16 data = (Int16)a;
                Listvalues.Add(data);
            }

            return Listvalues.ToArray();

        }






















    }
}
