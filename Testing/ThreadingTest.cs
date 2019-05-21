//传入参数，每一个IP对应一个THreadingTest，



using HslCommunication.BasicFramework;
using HslCommunication.LogNet;
using HslCommunication.Profinet.Siemens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsFormsApp1
{
    public class ThreadingTest
    {
        private ILogNet logNet;
        public    event  EventHandler MessageEventHandler;


        private List<HistoryData> _hda;
        public List<HistoryData> ListData
        {
            get
            {
                return _hda;
             }
        }
       /// <summary>
       /// 返回结果
       /// </summary>
        public  string Result { set; get; }
        /// <summary>
        /// 线程与PLC建立连接状态
        /// </summary>
        public bool IsConnect
        { get
            {
                return _LIBnodavePLC.IsConnect;
            }
        }
        public bool IsClosed { get {
                return _LIBnodavePLC.IsClosed;
            } }
        public HistoryData LastDate { get

            {
                if (_hda.Count==0)
                {
                    return new HistoryData();
                }
                return _hda.Last<HistoryData>();

            } }


        public string Name { get; set; }
        private System.Timers.Timer timer1 = new System.Timers.Timer();
        private static object _lock = new object();
        private int _DataLength;
        private string _Addr;
        private string _IP;
        private comm.DataTyte _dataTyte;
        private string _ThreadCount;
   
        CancellationTokenSource tokenSource = new CancellationTokenSource();
       private LIBnodavePLC _LIBnodavePLC ;
        //private IPLC PLC;
      /// <summary>
      /// 单块线程，一个线程一个DB块
      /// </summary>
      /// <param name="length">数据长度</param>
      /// <param name="Addr">地址块</param>
      /// <param name="IP">PLC的IP地址</param>
      /// <param name="dataTyte">数据类型</param>
      /// <param name="ThreadCount">线程数，不设置为空</param>
        public ThreadingTest(int length, string Addr, string IP, comm.DataTyte dataTyte, string ThreadCount = null)
        {
        
            timer1.Elapsed += timer1_Elapsed;
            logNet = new LogNetSingle("Treadinglog.txt");
            _LIBnodavePLC = new LIBnodavePLC();
            //PLC = new SenmensPLC();
            this._DataLength = length;
            this._Addr = Addr;
            this._IP = IP;
            _hda = new List<HistoryData>();
            this._ThreadCount = ThreadCount;
            this._dataTyte = dataTyte;
            Name = "单线程读取,地址：" + _Addr + "PLC IP:" + IP + "\r\n";

        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsConnect)
            {
                _LIBnodavePLC.Connect();
                if (MessageEventHandler != null)
                {
                    MessageEventHandler(Name + DateTime.Now + "已重新连接", null);
                }

            }
        }

        public ThreadingTest(string IP,string addr )
        {
            timer1.Elapsed += timer1_Elapsed;
            
         _LIBnodavePLC = new LIBnodavePLC();
            this._IP = IP;
            _Addr = addr;
            AsyncMultiDBStart();
            Name = "单线程多DB块同时读取,地址：" + _Addr + "PLC IP:" + IP+ "\r\n";
            timer1.Interval = 3000;
            timer1.Start();
        }

        public List<ThreadingTest> ThreadingTests { set; get; }
        public Task task;
        public string LogMessage { set; get; }
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
        public void Close()
        {

            tokenSource.Cancel();

        }
        /// <summary>
        /// 同一PLC不同DB块同时读取数据
        /// </summary>
        /// <param name="pLCAddrLists"></param>
        /// <returns><eturns>
        public int AsyncMultiDBStart()
        {
            _hda = new List<HistoryData>();
            List<PLCAddrList> pLCAddrLists = GetPLCAddrLists(_Addr);
            CancellationToken token = tokenSource.Token;       
            _dataTyte = comm.DataTyte.STRING;
            _LIBnodavePLC.IP = this._IP;
            _LIBnodavePLC.Connect();
            if (IsConnect)
            {
                Result = "与PLC连接成功:" + _IP+"地址为"+ _Addr+"---------" + DateTime.Now+"\r\n";
            }
            else
            {
              
                Result = "与PLC连接失败:" + _IP+"地址为"+ _Addr +"---------" + DateTime.Now+ "\r\n";
            }
           

            Task<int> task = new Task<int>(() =>
              {                                     
                  while (true)
                  {

                      try
                      {
                          string send = null;
                          string recive = null;
                          bool check = true;
                          double during = 0;
                          int length = 0;
                          string DBAddr = null;
                          foreach (var item in pLCAddrLists)
                          {

                              if (token.IsCancellationRequested)
                              {
                                  _hda.Clear();
                                  _LIBnodavePLC.Dispose();
                                  throw new OperationCanceledException(token);
                              }
                              send = GetRandomString(item.DataLenth * 2);
                            
                              _LIBnodavePLC.Address = item.Addr;

                              int ret = _LIBnodavePLC.WriteBytes(SoftBasic.HexStringToBytes(send));
                              if (ret != 0)
                              {

                                  string result = item.Addr + "写数据失败：" + _LIBnodavePLC.ErrorCode + "---------" + DateTime.Now + "\r\n";
                                  if (MessageEventHandler != null)
                                  {
                                      MessageEventHandler(result, null);
                                  }
                              }
                              ret = _LIBnodavePLC.ReadBytesResult(item.DataLenth);
                              if (ret != 0)
                              {
                                  string result = _IP + item.Addr + "读数据失败：" + _LIBnodavePLC.ErrorCode + "---------" + DateTime.Now + "\r\n";
                                  if (MessageEventHandler != null)
                                  {
                                      MessageEventHandler(result, null);
                                  }
                              }
                              byte[] ReciveData = _LIBnodavePLC.ReadBuff as byte[];
                              recive = SoftBasic.ByteToHexString(ReciveData);

                              during += (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds;
                              check &= send.Equals(recive);
                              DBAddr += item.Addr;
                              length += item.DataLenth;
                          }
                          _hda.Add(new HistoryData(during, length, check, DateTime.Now, DBAddr)); // UpdateUIDelegate(LogMessage);
                          if (ListData.Count > 10)
                          {
                              switch (DataTyte)
                              {
                                  case comm.DataTyte.BOOL:

                                      break;
                                  case comm.DataTyte.WORD:

                                      SaveData<HistoryData>("HistoryData_Word" + ThreadCount, ListData);
                                      break;
                                  case comm.DataTyte.INT16:

                                      SaveData<HistoryData>("HistoryData_Int16" + ThreadCount, ListData);
                                      break;
                                  case comm.DataTyte.INT32:
                                      SaveData<HistoryData>("HistoryData_Int32" + ThreadCount, ListData);
                                      break;
                                  case comm.DataTyte.REAL:
                                      SaveData<HistoryData>("HistoryData_real" + ThreadCount, ListData);
                                      break;
                                  case comm.DataTyte.STRING:
                                      SaveData<HistoryData>("HistoryData_str" + ThreadCount, ListData);
                                      break;
                                  default:
                                      break;
                              }
                          }
                      }
                      catch (Exception ex)
                      {
                          if (MessageEventHandler!=null)
                          {
                              MessageEventHandler(_LIBnodavePLC.ErrorCode + ex.Message.ToString(), null);
                          }
                      
                      }
                  }
              }, token);
            task.Start();
            return 0;
        }

        public void AsyncStart()
        {
            try
            {
              
                CancellationToken token = tokenSource.Token;
                string Addr = this._Addr;
                string IP = this._IP;
                if (!_LIBnodavePLC.IsConnect)
                {
                    logNet.RecordMessage(HslMessageDegree.INFO, "创建连接中-----线程"+ThreadCount,DateTime.Now.ToString() );
                    bool isopen = _LIBnodavePLC.Init(Addr, IP);
                    if (!isopen)
                    {
                        MessageEventHandler( _dataTyte.ToString() + "建立连接失败" + DateTime.Now + "\r\n", null);
                        return;  
                    }
                    LogMessage=LogMessage+ _dataTyte.ToString()+ "建立连接结果" + isopen.ToString()+"------"+DateTime.Now+"\r\n";
                    logNet.RecordMessage(HslMessageDegree.INFO, "创建连接完成线程" + ThreadCount, DateTime.Now.ToString());

                  //  _hda.Add(new HistoryData(during, _DataLength, check, DateTime.Now)); // UpdateUIDelegate(LogMessage);

                }

                bool check = false;
                string send = "";
                string recive = "";
                int length = 0;
                IEnumerable sendBytes;
               task=new Task(() =>
                {
                    while (true)
                    {
                        if (token.IsCancellationRequested)
                        {
                                _hda.Clear();
                                _LIBnodavePLC.Dispose();
                                throw new OperationCanceledException(token);
                        }

                        try
                        {
                            Thread.Sleep(10);
                            switch (this._dataTyte)
                            {
                                case comm.DataTyte.BOOL:

                                    break;
                                case comm.DataTyte.WORD:
                                    length = this._DataLength / 2;
                                    sendBytes = GetRandomUshort(length);
                                    _LIBnodavePLC.WriteUInt16s(sendBytes as UInt16[]);
                                    _LIBnodavePLC.ReadUInt16s(length);
                                    check = Check(sendBytes, _LIBnodavePLC.ReadBuff);
                                    break;
                                case comm.DataTyte.INT16:
                                    length = this._DataLength / 2;
                                    sendBytes = GetRandomInt16(length);
                                    _LIBnodavePLC.WriteInt16s(sendBytes as Int16[]);
                                    _LIBnodavePLC.ReadInt16s(length);
                                    check = Check(sendBytes, _LIBnodavePLC.ReadBuff);


                                    break;
                                case comm.DataTyte.INT32:
                                    length = this._DataLength / 4;
                                    sendBytes = GetRandomInt32(length);
                                    _LIBnodavePLC.WriteInt32s(sendBytes as Int32[]);
                                    _LIBnodavePLC.ReadInt32s(length);
                                    check = Check(sendBytes, _LIBnodavePLC.ReadBuff);
                                    break;
                                case comm.DataTyte.REAL:
                                    length = this._DataLength / 4;
                                    sendBytes = GetRandomfloat(length);
                                    _LIBnodavePLC.WriteFloats(sendBytes as float[]);
                                    _LIBnodavePLC.ReadFloats(length);
                                    check = Check(sendBytes, _LIBnodavePLC.ReadBuff);

                                    break;
                                case comm.DataTyte.STRING:
                                    length = this._DataLength * 2;
                                    send = GetRandomString(length);
                                    _LIBnodavePLC.WriteBytes(SoftBasic.HexStringToBytes(send));
                                     _LIBnodavePLC.ReadBytes(length / 2);
                                    byte[] ReciveData = _LIBnodavePLC.ReadBuff as byte[];
                                    recive = SoftBasic.ByteToHexString(ReciveData);
                                    check = send.Equals(recive);

                                    break;
                                default:
                                    break;
                            }
                            
                            double during = (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds;
                            //logNet2.RecordMessage(HslMessageDegree.DEBUG, null, "耗时" + (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds + "字节数" + send.Length.ToString() + "是否正常" + check);

                            _hda.Add(new HistoryData(during, _DataLength, check, DateTime.Now, _LIBnodavePLC.Address));

                            if (ListData.Count > 10)
                            {
                                switch (DataTyte)
                                {
                                    case comm.DataTyte.BOOL:

                                        break;
                                    case comm.DataTyte.WORD:

                                        SaveData<HistoryData>("HistoryData_Word" + ThreadCount, ListData);
                                        break;
                                    case comm.DataTyte.INT16:

                                        SaveData<HistoryData>("HistoryData_Int16" + ThreadCount, ListData);
                                        break;
                                    case comm.DataTyte.INT32:
                                        SaveData<HistoryData>("HistoryData_Int32" + ThreadCount, ListData);
                                        break;
                                    case comm.DataTyte.REAL:
                                        SaveData<HistoryData>("HistoryData_real" + ThreadCount, ListData);
                                        break;
                                    case comm.DataTyte.STRING:
                                        SaveData<HistoryData>("HistoryData_str" + ThreadCount, ListData);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageEventHandler(_LIBnodavePLC.ErrorCode+ex.Message.ToString(), null);
                        }
{ }
                    }

                },token
                );
                task.Start();
                LogMessage= LogMessage + _dataTyte.ToString() + "线程开始中-----------" + DateTime.Now + "\r\n";
                if (MessageEventHandler != null)
                {
                    MessageEventHandler(LogMessage, null);
                }
            }
            catch (Exception ex)
            {
                if (MessageEventHandler != null)
                {
                    MessageEventHandler(ex.Message.ToString(), null);
                }
                // MessageBox.Show(ex.Message);
            }

        }

        private bool Check(IEnumerable arr1, IEnumerable arr2)
        {
            return (arr1 as IStructuralEquatable).Equals(arr2, StructuralComparisons.StructuralEqualityComparer);
        }
        # region 获取随机数组
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
#endregion
        /// <summary>
        /// 获取PLC地址参数，IP地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns><eturns>
        private List<PLCAddrList> GetPLCAddrLists(string str)
        {
            List<PLCAddrList> addrLists = new List<PLCAddrList>();
             
                string[] arr = str.Split(';');
            foreach (var item in arr)
            {
                if (item=="")
                {
                    break;
                }
                string[] device= item.Split('|');
                addrLists.Add(new PLCAddrList() { Addr = device[0], DataLenth =int.Parse( device[1].ToString().Trim()) });    
            }
            return addrLists;


        }
        private void SaveData<T>(string TableName, List<T> ts)
        {
            try
            {
                lock (_lock)//lock研究
                {

                    using (SqlBulkCopy sbc = new SqlBulkCopy(comm.Connection))
                    {
                        using (DataTable dt = comm.ToDataTable(ts))
                        {

                            try
                            {

                                sbc.DestinationTableName = TableName;
                                sbc.WriteToServer(dt);
                                ts.Clear();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("BulkInsert调用异常", ex);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }



















    }
}
