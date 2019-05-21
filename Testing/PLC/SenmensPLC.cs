using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class SenmensPLC:IPLC
    {
        OperateResult operateResult = null;

        private SiemensS7Net siemensTcpNet;
        private DateTime _sendTime;
        private int[] _int32;
        private ushort[] _Uint16s;
        private short[] _int16s;
        private float[] _float;
        private DateTime _readtime;
        private byte[] _readBuff;
        private string _IP;
        private byte[] _reciveDate;
        private bool _closed;
        
        public bool IsConnect { get; set; }
        public string Address { set; get; }
        public string ErrorCode { get; set; }
        public DateTime Sendtime
        {
            get
            {
                return _sendTime;
            }

        }
        public DateTime Readtime
        {

            get
            {
                return _readtime;

            }
        }
      
        public string IP
        {
            get
            {
                return _IP;
            }
            set
            {
                _IP = value;
            }


        }
        public bool IsClosed
        {
            set
            {
                _closed = value;
            }
            get
            {
                return _closed;
            }
        }  

        public IEnumerable ReadBuff { get ; set ; }

        public bool Connect()
        {

            OperateResult connect = siemensTcpNet.ConnectServer();
            
            if (connect.IsSuccess)
            {
                return true;
            }
            else
            {
                ErrorCode = connect.ToMessageShowString();
                return false;
               
            }
        }

        public SenmensPLC()
        {
            siemensTcpNet = new SiemensS7Net(SiemensPLCS.S300);
            siemensTcpNet.IpAddress = _IP;
            siemensTcpNet.ConnectTimeOut = 5000;
           

        }



        //public int ReadBytesResult(int length)
        //{
        //    siemensTcpNet.Write();

        //    siemensTcpNet.Read();
        //    _readtime = DateTime.Now;




        //}
        public int WriteInt32s(Int32[] data)
        {
            List<byte> bytesContent = new List<byte>();
            foreach (var item in data)
            {
                byte[] b = BitConverter.GetBytes(item);
                //Array.Reverse(b);
                bytesContent.AddRange(b);
            }
            byte[] bytes = bytesContent.ToArray();
            bytesContent.Clear();
            return WriteBytes(bytes);

        }

        private int WriteBytes(byte[] bytes)
        {
          OperateResult result=  siemensTcpNet.Write(Address,bytes);
            if (!result.IsSuccess)
            {
                ErrorCode = result.ToMessageShowString();
                return result.ErrorCode;
            }
            return 0;
        }

        public int WriteInt16s(Int16[] data)
        {
            List<byte> bytesContent = new List<byte>();

            _sendTime = DateTime.Now;
            foreach (var item in data)
            {
                byte[] b = BitConverter.GetBytes(item);
                //Array.Reverse(b);
                bytesContent.AddRange(b);
            }
            byte[] bytes = bytesContent.ToArray();
            bytesContent.Clear();
            return WriteBytes(bytes, true);

        }
      


        public int WriteBytes(byte[] bytes, bool IsTime=false)
        {
            if (!IsTime)
            {
                _sendTime = DateTime.Now;
            }
          
            OperateResult result = siemensTcpNet.Write(Address, bytes);
            if (!result.IsSuccess)
            {
                ErrorCode = result.ToMessageShowString();
                return result.ErrorCode;
              
            }
            return 0;
        }

        public int WriteUInt16s(UInt16[] data)
        {
            List<byte> bytesContent = new List<byte>();

            _sendTime = DateTime.Now;
            foreach (var item in data)
            {
                byte[] b = BitConverter.GetBytes(item);
                //Array.Reverse(b);
                bytesContent.AddRange(b);
            }
            byte[] bytes = bytesContent.ToArray();
            bytesContent.Clear();
            return WriteBytes(bytes, true);

        }


        public int WriteFloats(float[] flaots)
        {
            _sendTime = DateTime.Now;
            List<byte> bytesContent = new List<byte>();
            foreach (var item in flaots)
            {
                byte[] b = BitConverter.GetBytes(item);
                //Array.Reverse(b);
                bytesContent.AddRange(b);
            }
            byte[] bytes = bytesContent.ToArray();
            bytesContent.Clear();
            return WriteBytes(bytes, true);

        }

        public int ReadInt32s(int length)
        {
            try
            {
                List<Int32> data = new List<Int32>();
                byte[] readbytes = ReadBytes(Convert.ToUInt16(length * 4));

                for (int i = 0; i < length; i++)
                {
                    //floats.Add(dc.getFloat());
                    data.Add(BitConverter.ToInt32(readbytes, i * 4));

                }
                ReadBuff = data.ToArray();
                _readtime = DateTime.Now;
                data.Clear();

                return 0;

            }
            catch (Exception)
            {

                throw;
            }



        }

        public byte[] ReadBytes(int Length)
        {

            OperateResult<byte[]> result = siemensTcpNet.Read(Address,(ushort)Length);
           
            if (!result.IsSuccess)
            {
                ErrorCode = result.ToMessageShowString();
            }
            _readtime = DateTime.Now;
            ReadBuff = result.Content;
            return result.Content;

        }

        public int ReadFloats(int length)
        {
            try
            {
                List<float> floats = new List<float>();
                byte[] readbytes = ReadBytes(length * 4);

                for (int i = 0; i < length; i++)
                {
                    //floats.Add(dc.getFloat());
                    floats.Add(BitConverter.ToSingle(readbytes, i * 4));

                }
                ReadBuff = floats.ToArray();
                _readtime = DateTime.Now;
                floats.Clear();
                return 0;

            }
            catch (Exception)
            {
                return -1;
                throw;
            }

        }
        public int ReadInt16s(int length)
        {
            try
            {
                List<Int16> data = new List<Int16>();
                byte[] readbytes = ReadBytes(Convert.ToUInt16(length * 2));

                for (int i = 0; i < length; i++)
                {
                    //floats.Add(dc.getFloat());
                    data.Add(BitConverter.ToInt16(readbytes, i * 2));

                }
                ReadBuff = data.ToArray();
                _readtime = DateTime.Now;
                data.Clear();
                return 0;

            }
            catch (Exception)
            {
                return -1;
                throw;
            }

        }

        public int ReadUInt16s(int length)
        {
            try
            {
                List<UInt16> data = new List<UInt16>();
                byte[] readbytes = ReadBytes(Convert.ToUInt16(length * 2));

                for (int i = 0; i < length; i++)
                {
                    //floats.Add(dc.getFloat());
                    data.Add(BitConverter.ToUInt16(readbytes, i * 2));

                }
                ReadBuff = data.ToArray();
                _readtime = DateTime.Now;
                data.Clear();
                return 0;

            }
            catch (Exception)
            {
                return -1;
                throw;
            }

        }

        public bool Init(string ADD, string IP)
        {
            this.Address=ADD;
            this._IP = IP;
            siemensTcpNet.IpAddress = _IP;
       
            return Connect();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

      

        public int ReadBytesResult(int dataLenth)
        {
            OperateResult<byte[]> result = siemensTcpNet.Read(Address, (ushort)dataLenth);

            if (!result.IsSuccess)
            {
                ErrorCode = result.ToMessageShowString();
                return 0xffff;
            }
            _readtime = DateTime.Now;
            ReadBuff = result.Content;
            return 0;

        }
    }
}
