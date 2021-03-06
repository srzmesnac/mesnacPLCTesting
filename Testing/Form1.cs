﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication;
using HslCommunication.BasicFramework;
using HslCommunication.Profinet.Siemens;
using HslCommunication.LogNet;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        SiemensS7Net s7Net { set; get; }
        OperateResult result = null;
        private static bool ThreadState1 = false;
        private static bool ThreadState2 = false;
        private static bool ThreadState3 = false;
        private static bool ThreadState4 = false;
        private static bool ThreadState = false;
        public Form1()
        {

            InitializeComponent();
            #region 添加启动日志 Srz
            logNetStart = new LogNetSingle("StartLog.txt");
            logNetStart.RecordMessage(HslMessageDegree.DEBUG, null, "程序启动" + DateTime.Now.ToString());
            #endregion
            //if (result.IsSuccess)
            //{
            //    s7Net.ReadByte("DB0.0");
            //  OperateResult<byte[]>  results = s7Net.Read("DB0.0",10);


            //}

        }
        private ILogNet logNetStart;
        private ILogNet logNet;
        private ILogNet logNet2;
        private ILogNet LogNetFloat;
        private ILogNet LogNetInt32;
        private ILogNet LogNetInt16;
        private ILogNet LogNetBit;
        private IPLC PLCTextBox;
        private static object _lock = new object();
        private List<HistoryData> _hdaInt16;
        private List<HistoryData> _hdaInt32;
        private List<HistoryData> _hdaStr;
        private List<HistoryData> _hdaFloat;
        private List<HistoryData> _hdaBit;
        private List<ThreadingTest> threadingTests;

        //
        //private void CSLInit()
        //{

        //    logNet = new LogNetSingle("log.txt");

        //    Txt_Recieve.Text = "";
        //    s7Net = new SiemensS7Net(SiemensPLCS.S300, Txt_IP.Text);
        //    s7Net.ConnectTimeOut = 3000;
        //    Task.Run(() =>
        //    {
        //        result = s7Net.ConnectServer();
        //        if (result.IsSuccess)
        //        {
        //            userLantern1.LanternBackground = Color.Green;
        //        }
        //        else
        //        {
        //            userLantern1.LanternBackground = Color.Red;

        //        }
        //    }
        //    );
        //    Thread.Sleep(1000);
        //    Random random = new Random();
        //    string SendAddr = txt_faddr.Text = "DB1.0";
        //    OperateResult<byte[]> ReadResult = null;
        //    Task.Run(() =>
        //    {
        //        while (true)
        //        {
        //            //Thread.Sleep(5);

        //            this.Invoke(new Action(() =>
        //            {
        //                lock (this)
        //                {

        //                    Txt_Send.Text = GetRandomString(6000);
        //                }

        //            }
        //      ));
        //            s7Net.Write(SendAddr, SoftBasic.HexStringToBytes(Txt_Send.Text));
        //            logNet.RecordMessage(HslMessageDegree.DEBUG, null, "写入" + Txt_Send.Text);
        //            //接受最多3000个字节
        //            ReadResult = s7Net.Read(SendAddr, 3000);
        //            this.Invoke(new Action(() =>
        //            {
        //                lock (this)
        //                {
        //                    Txt_Recieve.Text = SoftBasic.ByteToHexString(ReadResult.Content);
        //                    logNet.RecordMessage(HslMessageDegree.DEBUG, null, "读取" + Txt_Recieve.Text);
        //                    logNet.RecordMessage(HslMessageDegree.DEBUG, null, "是否正常" + Check(Txt_Recieve.Text, Txt_Send.Text));
        //                    //Thread.Sleep(500);                           
        //                }

        //            }
        //          ));




        //        }
        //    });


        //}






        private void userButton2_Click(object sender, EventArgs e)
        {


        }
        //private void LibCreate()
        //{
        //    _LIBnodavePLC = new LIBnodavePLC();
        //    bool isopen = _LIBnodavePLC.Init(Txt_IP.Text);
        //    if (isopen)
        //    {
        //        userLantern1.LanternBackground = Color.Green;
        //    }
        //    else
        //    {
        //        userLantern1.LanternBackground = Color.Red;

        //    }

        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            threadingTests = new List<ThreadingTest>();
            _hdaInt16 = new List<HistoryData>();
            _hdaInt32 = new List<HistoryData>();
            _hdaStr = new List<HistoryData>();
            _hdaFloat = new List<HistoryData>();
            _hdaBit = new List<HistoryData>();
           


            // Task.Run(() => LibCreate()) ;

            //LibInit();
            //CSLInit();



        }
        private bool Check(IEnumerable arr1, IEnumerable arr2)
        {
            return (arr1 as IStructuralEquatable).Equals(arr2, StructuralComparisons.StructuralEqualityComparer);
        }


        //private void AsyncStrStart(int length)
        //{
        //    try
        //    {

        //        ThreadState4 = true;
        //        logNet2 = new LogNetSingle("StrDataLog.txt");
        //        _LIBnodavePLC = new LIBnodavePLC();
        //        bool isopen = _LIBnodavePLC.Init(txt_straddr.Text, Txt_IP.Text); ;

        //        string send = "";
        //        string recive = "";
        //        bool check = false;
        //        Task.Run(() =>
        //        {
        //            while (true)
        //            {
        //                         Thread.Sleep(10);                      
        //                            send = GetRandomString(length);
        //                        _LIBnodavePLC.WriteBytes(SoftBasic.HexStringToBytes(send));
        //                byte[] ReciveData = _LIBnodavePLC.ReadBytes(send.Length / 2);
        //                    recive= SoftBasic.ByteToHexString(ReciveData);
        //                if (send.CompareTo(recive) == 0)
        //                    check = true;
        //                else
        //                    check = false;
        //                double during = (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds;
        //                    logNet2.RecordMessage(HslMessageDegree.DEBUG, null, "耗时" + (_LIBnodavePLC.Readtime - _LIBnodavePLC.Sendtime).TotalMilliseconds + "字节数" + send.Length.ToString() + "是否正常" + check);

        //                    _hdaStr.Add(new HistoryData(during, send.Length, check, DateTime.Now));

        //            }



        //        }
        //        );
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }


        //}






        #region 5.0 生成随机字符串 + static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串<eturns>
        public static string GetRandomString(int length, bool useNum = true, bool useLow = false, bool useUpp = false)
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
        #endregion
        public static Int32[] GetRandomInt32(int length)
        {

            List<Int32> Listvalues = new List<Int32>();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                Int32 a = rd.Next(100000);

                Listvalues.Add(a);
            }

            return Listvalues.ToArray();

        }

        public static float[] GetRandomfloat(int length)
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
        public static Int16[] GetRandomInt16(int length)
        {

            List<Int16> Listvalues = new List<Int16>();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                int a = rd.Next(100000);
                Int16 data = (Int16)a;
                Listvalues.Add(data);
            }

            return Listvalues.ToArray();

        }




        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    Connect();
        //    AsyncFloatStart();
        //}

        // private LIBnodavePLC Connect(string addr)
        // {

        ////
        // }


        private void start(Func<object> action)
        {


        }


        //private void AsyncFloatStart( LIBnodavePLC lIBnodavePLC, float[] floats)
        //{
        //    int _Lenth = floats.Length;
        //    int ERROR = 0;
        //    string recive = null;
        //    //string send = null;
        //    bool check= false;
        //    LogNetFloat = new LogNetSingle("FloatDataLog.txt");
        //    ThreadState1 = true;
        //    double during = 0;
        //    Task.Run(() => {

        //        while (true)
        //        {
        //            if (floats != null)
        //            {
        //                Thread.Sleep(10);
        //                floats = GetRandomfloat(750);
        //                lIBnodavePLC.WriteFloats(floats);
        //                lIBnodavePLC.ReadFloats(Convert.ToUInt16(floats.Length));
        //                check = (floats as IStructuralEquatable).Equals(lIBnodavePLC.Floats, StructuralComparisons.StructuralEqualityComparer);






        //                during = (lIBnodavePLC.Readtime - lIBnodavePLC.Sendtime).TotalMilliseconds;
        //                LogNetFloat.RecordMessage(HslMessageDegree.DEBUG, null, "耗时" + (lIBnodavePLC.Readtime - lIBnodavePLC.Sendtime).TotalMilliseconds + "字节数" + _Lenth.ToString() + "是否正常" + check);

        //                _hdaFloat.Add(new HistoryData(during,_Lenth,check,DateTime.Now,));
        //            }


        //            else
        //            {
        //                float _value = 1.00f;

        //                _value = _value + 0.01f;
        //                lIBnodavePLC.WriteFloat(_value);
        //                this.Invoke(new Action(() =>
        //                {
        //                    LogNetFloat.RecordMessage(HslMessageDegree.DEBUG, "写入", _value.ToString());
        //                    recive = lIBnodavePLC.ReadFloat().ToString();
        //                    LogNetFloat.RecordMessage(HslMessageDegree.DEBUG, "读取", recive);
        //                    if (_value.ToString() != recive.ToString())
        //                    {
        //                        ERROR++;
        //                    }
        //                    LogNetFloat.RecordMessage(HslMessageDegree.DEBUG, "ERROR", ERROR.ToString());


        //                }

        //                                  )
        //                                  );
        //            }
        //        }

        //    });



        //}


        //private void AsyncInt32Start(LIBnodavePLC lIBnodavePLC,int _Length)
        //{
        //    //if (ThreadState2)
        //    //{

        //    //    return;
        //    //}
        //    try
        //    {               
        //        int ERROR = 0;

        //        string log = string.Format("Int32DataLog.txt");
        //        LogNetInt32 = new LogNetSingle(log);
        //        string recive = null;
        //        int[] send=null;
        //        ThreadState2 = true;
        //        bool check = false;
        //        Task task = new Task(() =>
        //        {

        //            int _value = 1;
        //            while (true)
        //            {
        //                if (_Length!= 0)
        //                {
        //                    send = GetRandomInt32(_Length);
        //                    Thread.Sleep(10);
        //                    lIBnodavePLC.WriteInt32s(send);
        //                        lIBnodavePLC.ReadInt32s(_Length);

        //                    check = Check(lIBnodavePLC.Int32s,send);
        //                        //LogNetInt32.RecordMessage(HslMessageDegree.DEBUG, null, "耗时" + (lIBnodavePLC.Readtime - lIBnodavePLC.Sendtime).TotalMilliseconds + "字节数" + _Lenth.ToString() + "是否正常" + Check(recive, send));

        //                    _hdaInt32.Add(new HistoryData((lIBnodavePLC.Readtime - lIBnodavePLC.Sendtime).TotalMilliseconds, _Length*4, check, DateTime.Now));


        //                }

        //                else
        //                {
        //                    _value = _value + 1;
        //                    lIBnodavePLC.WriteInt32(_value);

        //                    //  Txt_Send.Text = _value.ToString();
        //                    LogNetInt32.RecordMessage(HslMessageDegree.DEBUG, "写入", _value.ToString());
        //                    recive = lIBnodavePLC.ReadInt32().ToString();
        //                    LogNetInt32.RecordMessage(HslMessageDegree.DEBUG, "读取", recive);

        //                    if (_value.ToString() != recive.ToString())
        //                    {
        //                        ERROR++;
        //                    }
        //                    LogNetInt32.RecordMessage(HslMessageDegree.DEBUG, "ERROR", ERROR.ToString());

        //                }

        //            }

        //        });

        //        task.Start();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}



        private void AsyncInt32Start(LIBnodavePLC lIBnodavePLC)
        {
            //if (ThreadState2)
            //{

            //    return;
            //}
            try
            {


                int ERROR = 0;
                string log = string.Format("Int32DataLog.txt");
                LogNetInt32 = new LogNetSingle(log);
                string recive = null;
                ThreadState2 = true;
                Task task = new Task(() =>
                {




                    int _value = 1;
                    while (true)
                    {


                        Thread.Sleep(10);
                        _value = _value + 1;
                        lIBnodavePLC.WriteInt32(_value);

                        //  Txt_Send.Text = _value.ToString();
                        LogNetInt32.RecordMessage(HslMessageDegree.DEBUG, "写入", _value.ToString());
                        recive = lIBnodavePLC.ReadInt32().ToString();
                        LogNetInt32.RecordMessage(HslMessageDegree.DEBUG, "读取", recive);

                        if (_value.ToString() != recive.ToString())
                        {
                            ERROR++;
                        }
                        LogNetInt32.RecordMessage(HslMessageDegree.DEBUG, "ERROR", ERROR.ToString());

                    }

                });

                task.Start();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void AsyncBitStart(LIBnodavePLC lIBnodavePLC)
        {

            if (!ThreadState3)
            {
                int ERROR = 0;
                ThreadState3 = true;
                bool flagl = true;
                string log = string.Format("BitDataLog.txt");
                LogNetBit = new LogNetSingle(log);
                string recive = null;
                Task task = new Task(() =>
                {
                    while (true)
                    {

                        Thread.Sleep(10);
                        flagl = !flagl;
                        lIBnodavePLC.WriteBit(flagl);
                        LogNetBit.RecordMessage(HslMessageDegree.DEBUG, "写入", flagl.ToString());
                        recive = lIBnodavePLC.ReadBit().ToString();
                        LogNetBit.RecordMessage(HslMessageDegree.DEBUG, "读入", recive);
                        if (flagl.ToString() != recive.ToString())
                        {
                            ERROR++;
                        }
                        LogNetBit.RecordMessage(HslMessageDegree.DEBUG, "ERROR", ERROR.ToString());

                    }
                }

              );

                task.Start();
            }

        }
        private void WriteData<T>(T t)
        {


        }

        //private void ReadData<T>(T t)
        //{
        //    AsyncStart(new LIBnodavePLC(), new byte[1]);


        //}



        //private void AsyncInt16Start(LIBnodavePLC lIBnodavePLC, int Length)
        //{
        //    if (ThreadState)
        //    {

        //        return;
        //    }
        //    short[] sendbyte = null;
        //    int ERROR = 0;
        //    bool check = false;
        //   // string send = null;
        //    string recive = null;
        //    string log = string.Format("Int16DataLog.txt");
        //    LogNetInt16 = new LogNetSingle(log);
        //    ThreadState = true;
        //    Task task = new Task(() =>
        //    {
        //        Int16 _value = 1;
        //        while (true)
        //        {
        //            if (Length != 0)
        //            {
        //                Thread.Sleep(10);
        //                sendbyte = GetRandomInt16(Length);
        //                lIBnodavePLC.WriteInt16s(GetRandomInt16(Length));
        //                lIBnodavePLC.ReadInt16s(sendbyte.Length);


        //               // LogNetInt16.RecordMessage(HslMessageDegree.DEBUG, null, "耗时" + (lIBnodavePLC.Readtime - lIBnodavePLC.Sendtime).TotalMilliseconds + "字节数" + sendbyte.Length.ToString() + "是否正常" + Check(recive, send));



        //                check = Check(lIBnodavePLC.Int16s, sendbyte);
        //                //LogNetInt32.RecordMessage(HslMessageDegree.DEBUG, null, "耗时" + (lIBnodavePLC.Readtime - lIBnodavePLC.Sendtime).TotalMilliseconds + "字节数" + _Lenth.ToString() + "是否正常" + Check(recive, send));

        //                _hdaInt16.Add(new HistoryData((lIBnodavePLC.Readtime - lIBnodavePLC.Sendtime).TotalMilliseconds, Length * 2, check, DateTime.Now));


        //            }

        //            else
        //            {
        //                _value++;
        //                lIBnodavePLC.WriteInt16(_value);
        //                LogNetInt16.RecordMessage(HslMessageDegree.DEBUG, "写入", _value.ToString());
        //                recive = lIBnodavePLC.ReadInt16().ToString();
        //                LogNetInt16.RecordMessage(HslMessageDegree.DEBUG, "读取", recive);
        //                if (_value.ToString() != recive.ToString())
        //                {
        //                    ERROR++;
        //                }
        //                LogNetInt16.RecordMessage(HslMessageDegree.DEBUG, "ERROR", ERROR.ToString());
        //                if (_value > 100)
        //                {
        //                    _value = 0;
        //                }
        //            }

        //        }



        //    });

        //    task.Start();
        //}





        private void AsyncInt16Start(LIBnodavePLC lIBnodavePLC)
        {
            if (ThreadState)
            {

                return;
            }
            int ERROR = 0;
            string recive = null;
            string log = string.Format("Int16DataLog.txt");
            LogNetInt16 = new LogNetSingle(log);
            ThreadState = true;
            Task task = new Task(() =>
            {




                Int16 _value = 1;
                while (true)
                {
                    Thread.Sleep(10);
                    _value++;
                    lIBnodavePLC.WriteInt16(_value);



                    LogNetInt16.RecordMessage(HslMessageDegree.DEBUG, "写入", _value.ToString());
                    recive = lIBnodavePLC.ReadInt16().ToString();
                    LogNetInt16.RecordMessage(HslMessageDegree.DEBUG, "读取", recive);
                    if (_value.ToString() != recive.ToString())
                    {
                        ERROR++;
                    }
                    LogNetInt16.RecordMessage(HslMessageDegree.DEBUG, "ERROR", ERROR.ToString());
                    if (_value > 100)
                    {
                        _value = 0;
                    }
                }

            });

            task.Start();
        }



        private void Btn_StrTest_Click(object sender, EventArgs e)
        {
            CreateStart(int.Parse(txt_strLength.Text), txt_straddr.Text, Txt_IP.Text, comm.DataTyte.STRING);
        }

        private void CreateStart(int length, string Addr, string IP, comm.DataTyte dataTyte, string ThreadCount = null)
        {
            if (!radioButton1.Checked)
            {
                PLCTextBox = new LIBnodavePLC();
            }
            else
                PLCTextBox = new SenmensPLC();
            ThreadingTest threadingTest = new ThreadingTest(length,Addr, IP,  dataTyte, PLCTextBox, ThreadCount);
            threadingTest.MessageEventHandler += UpdateUI;
            threadingTest.AsyncStart();
            threadingTests.Add(threadingTest);
        
           
        }

        private void Btn_bitTest_Click(object sender, EventArgs e)
        {

            if (ThreadState3)
            {
                return;
            }
          
            
         
        }

        private void UserButton1_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Int16Test_Click(object sender, EventArgs e)
        {
            CreateStart(int.Parse(txt_strLength.Text), txt_int16addr.Text, Txt_IP.Text, comm.DataTyte.INT16);
           // Task.Run(() => threadingTests.Add(new ThreadingTest(3000, txt_int16addr.Text, Txt_IP.Text, comm.DataTyte.INT16)));



        }



        private void Btn_FloatTest_Click(object sender, EventArgs e)
        {

            CreateStart(int.Parse(txt_strLength.Text), txt_faddr.Text, Txt_IP.Text, comm.DataTyte.REAL);
      
        }


        private void Btn_Int32Test_Click(object sender, EventArgs e)
        {


            CreateStart(int.Parse(txt_strLength.Text), txt_int32addr.Text, Txt_IP.Text, comm.DataTyte.INT32);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void OneDBMultiStarts()
        {
            string ss=null;
            string s;
            int j = int.Parse(txt_startDB.Text.Trim());
            int no = int.Parse(txt_ThreadNumber.Text.Trim());
            for (int i = 0; i < no; i++)
            {
                if (j<10)
                {
                    s = '0' + j.ToString();
                }
                else
                {
                    s = j.ToString();
                }
                    if (radioButton1.Checked)
                    {
                        ss = $"DB{s}" + "." + "0" ;
                    }
                    else
                        ss = $"DB{s}" + "," + "DBD0";

                    CreateStart(int.Parse(txt_strLength.Text), ss, Txt_IP.Text, comm.DataTyte.STRING, j.ToString());
                txt_log.Invoke(new MyDelegate(SetRich), "DB" + j.ToString());
                j++;
            }
        }
        delegate void MyDelegate(string str);//定义委托
        private void SetRich(string str)//委托
        {
            txt_log.Text+= str+"\r\n";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //起始块
            Task.Run(() => OneDBMultiStarts());
        
           

        }

        private void UpdateUI( object message,EventArgs e )
        {         
           txt_log.Invoke(new Action(() =>
           {
               txt_log.Text += message;
               txt_log.SelectionStart = txt_log.Text.Length; //设定光标位置
               txt_log.ScrollToCaret();

           }));
           
        }
       





        private void Timer1_Tick(object sender, EventArgs e)
        {


        }

        private void Txt_faddr_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btn_word_Click(object sender, EventArgs e)
        {
            CreateStart(int.Parse(txt_strLength.Text), txt_wordaddr.Text, Txt_IP.Text, comm.DataTyte.WORD);
         
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            foreach (var item in threadingTests)
            {
                item.Close();
                txt_log.Text += item.Name + "结束" + "-----------" + DateTime.Now;
                item.Dispose();
            }
           
           
            threadingTests.Clear();
        }



        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string ss = "";
                string s = "";
                for (int i = int.Parse(txt_startDB.Text); i < int.Parse(txt_ThreadNumber.Text)+1; i++)
                {
                    if (i < 10)
                    {
                        s = '0' + i.ToString();
                    }
                    else
                    {
                        s = i.ToString();
                    }
                    if (radioButton1.Checked)
                    {
                        ss += $"DB{s}" + "." + "0" + "|" + txt_strLength.Text.Trim() + ";";
                    }
                    else
                    ss += $"DB{s}" + "," + "DBD0" + "|" + txt_strLength.Text.Trim() + ";";
                   
                }
                StartDBList(Txt_IP.Text, ss);
            }
            catch (Exception)
            {

                MessageBox.Show("请输入正确的DB块数和起始DB块");
            }
         
        }
     
        private void StartDBList(string IP,string addr)
        {
            if (radioButton1.Checked)
            {
                PLCTextBox = new SenmensPLC();
            }
            else
                PLCTextBox = new LIBnodavePLC();
            ThreadingTest threadingTest = new ThreadingTest(IP,addr,PLCTextBox);
           
            txt_log.Text += "读取PLC:" + IP + threadingTest.Result;
            threadingTest.MessageEventHandler += UpdateUI;
            threadingTests.Add(threadingTest);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ListDataBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Txt_IP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
   