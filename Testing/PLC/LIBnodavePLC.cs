using HslCommunication.LogNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WindowsFormsApp1
{
    public class LIBnodavePLC:IPLC
    {
        private ILogNet logNet = new LogNetSingle("LIBnodavelog.txt");
        private static readonly object _async = new object();
        libnodave.daveOSserialType fds;
        libnodave.daveInterface di;
        internal libnodave.daveConnection dc;
        int _rack = 0;//机架号
        int _slot = 0;//PLC槽号
        string _IP;
        public comm.DataTyte dataTyte { get; set; }
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
      public IEnumerable ReadBuff { set;get;  }
        public float[] Floats
        {
            get
            {

                return _float;
            }
        }
        public Int32[] Int32s
        {
            get
            {

                return _int32;

            }


        }
        public Int16[] Int16s
        {
            get
            {
                return _int16s;
            }

        }
        public UInt16[] UInt16s
        {
            get
            {
                return _Uint16s;
            }

        }
        public string IP
        {
            set
            {
                _IP = value;
            }
            get
            {
                return _IP;
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
        public string Address
        {
            set
            {
                _address = value;              
              Device=GetDeviceAddress(value);
            }
            get
            {
                return _address;
            }

            
        }
        public byte[] ReciveDate
        {
            get
           {

                return _reciveDate;
            }


        }
        private byte[] _reciveDate;
        DateTime _sendTime;
        DateTime _readtime;
        float[] _float;
        Int16[] _int16s;
        UInt16[] _Uint16s;
        Int32[] _int32;
        //byte[] content;
       bool _closed = true;
        DateTime _closeTime = DateTime.Now;
        int TimeOut = 10000;
        bool _isconnect = false;
        public bool IsConnect
        {
            get
            {
                return _isconnect;

            }
        }

        public string ErrorCode { get ; set ; }

        public bool Connect()
        {

            lock (_async)
            {
                if (!_closed) return true;
                double sec = (DateTime.Now - _closeTime).TotalMilliseconds;
                if (sec < 6000)
                    System.Threading.Thread.Sleep(6000 - (int)sec);
                fds.rfd = libnodave.openSocket(102, _IP);
                fds.wfd = fds.rfd;
                if (fds.rfd > 0)
                {
                    di = new libnodave.daveInterface(fds, "IF1", 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
                    di.setTimeout(TimeOut);
                    //	    res=di.initAdapter();	// does nothing in ISO_TCP. But call it to keep your programs indpendent of protocols
                    //	    if(res==0) {
                 
                        dc = new libnodave.daveConnection(di, 0, _rack, _slot); // logNet.RecordMessage(HslMessageDegree.INFO,"断开已重连" +DateTime.Now);

                                     
                    if (0 == dc.connectPLC())
                    {
                        _closed = false;
                        _isconnect = true;
                        return true;
                    }
                }
                //if (dc != null) dc.disconnectPLC();
                //libnodave.closeSocket(fds.rfd);
            }
            _closed = true;
            return false;
        }
        // DeviceAddress device = new DeviceAddress();
        private DeviceAddress Device;
        private string _address;

        /// <summary>
        /// 带返回结果的ReadBytes方法,获取读取字节请获取ReciveDate
        /// </summary>
        /// <param name="len">读取字节数组长度</param>
        /// <returns><eturns>
        public int ReadBytesResult(int len)//从PLC中读取自己数组
        {
            
            List<byte> bytesContent = new List<byte>();
            
                if ((dc != null))
                {

                    int Start = Device.Start;
                    //if (IsConnect)
                    //{
                    //    this.Connect();
                    //}
                    byte[] buffer;
                    ushort alreadyFinished = 0;
                    while (alreadyFinished < len)
                    {
                        logNet.RecordMessage(HslMessageDegree.INFO, "已读取读取长度", alreadyFinished.ToString() + DateTime.Now);
                        ushort readLength = (ushort)Math.Min(len - alreadyFinished, 200);
                        logNet.RecordMessage(HslMessageDegree.INFO, "读取长度", readLength.ToString() + DateTime.Now);

                        int res = -1;
                        buffer = new byte[readLength];
                        res =dc.readBytes(Device.Area, Device.DBNumber, Start, readLength, buffer);
                        logNet.RecordMessage(HslMessageDegree.INFO, "读取数据返回结果：", daveStrerror(res) + DateTime.Now);

                        if (res != 0)
                        {
                        ErrorCode = daveStrerror(res);
                        int i = 0;
                        while (i<3)

                            {
                              i++;
                            _isconnect = false;
                            _closed = true;  _closeTime = DateTime.Now;
                                this.Connect();
                                res = dc.readBytes(Device.Area, Device.DBNumber, Start, readLength, buffer);
                            if (res == 0)
                               break;
                                                      
                            }
                        return res;
                        }
                        bytesContent.AddRange(buffer);
                        alreadyFinished += readLength;
                        Start += readLength;

                    }
                    ReadBuff= bytesContent.ToArray();
                
                    _readtime = DateTime.Now;



                    return 0;

                }

            return 0xffff;
           
        }

       

        public byte[] ReadBytes(int len)//从PLC中读取自己数组
        {
            List<byte> bytesContent = new List<byte>();
            byte[] a = new byte[0];
            try
            {
                if ((dc != null))
                {

                    int Start = Device.Start;
                    //if (IsConnect)
                    //{
                    //    this.Connect();
                    //}
                    byte[] buffer;
                    ushort alreadyFinished = 0;
                    while (alreadyFinished < len)
                    {
                        logNet.RecordMessage(HslMessageDegree.INFO, "已读取读取长度", alreadyFinished.ToString() + DateTime.Now);
                        ushort readLength = (ushort)Math.Min(len - alreadyFinished, 200);
                        logNet.RecordMessage(HslMessageDegree.INFO, "读取长度", readLength.ToString() + DateTime.Now);

                        int res = -1;
                        buffer = new byte[readLength];
                        res =  dc.readBytes(Device.Area, Device.DBNumber, Start, readLength, buffer);
                        logNet.RecordMessage(HslMessageDegree.INFO, "读取数据返回结果：", daveStrerror(res) + DateTime.Now);

                        if (res != 0)
                        {
                            ErrorCode = daveStrerror(res);
                            _isconnect = false;
                            _closed = true;
                            Connect();
                            res =  dc.readBytes(Device.Area, Device.DBNumber, Start, readLength, buffer);
                            
                        }
                        bytesContent.AddRange(buffer);
                        alreadyFinished += readLength;
                        Start += readLength;

                    }

                    _readtime = DateTime.Now;

                    if (bytesContent.ToArray()==null)
                    {
                        return a ;
                    }
                    ReadBuff = bytesContent.ToArray();
                    return bytesContent.ToArray();

                }
            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }


        public int WriteBytes(byte[] a, bool Isstart = false)
        {
            try
            {
                if (dc != null)
                {
                    if (!Isstart)
                    {
                        _sendTime = DateTime.Now;
                    }

                    int Start = Device.Start;

                    int length = a.Length;
                    ushort alreadyFinished = 0;

                    while (alreadyFinished < length)
                    {

                        ushort writeLength = (ushort)Math.Min(length - alreadyFinished, 200);
                        logNet.RecordMessage(HslMessageDegree.INFO, "读取数据长度" + writeLength.ToString(), DateTime.Now.ToString());
                        byte[] sendBuffer = TransByte(a, alreadyFinished, writeLength);
                        int res = dc.writeBytes(Device.Area, Device.DBNumber, Start, sendBuffer.Length, sendBuffer);
                        if (res != 0)
                        {
                            ErrorCode = daveStrerror(res);

                            int i = 0;

                            while (i < 3)
                            {
                                i++;
                                _closed = true; _closeTime = DateTime.Now;
                                Connect();
                                res = dc.writeBytes(Device.Area, Device.DBNumber, Start, sendBuffer.Length, sendBuffer);
                                if (res == 0)
                                    return res;

                            }
                            return res;
                        }

                      
                        logNet.RecordMessage(HslMessageDegree.INFO, "发送数据结果:" + daveStrerror(res), DateTime.Now.ToString());
                        //if (alreadyFinished==3000)
                        //{
                        //     res = dc.writeBytes(Device.Area, Device.DBNumber, Start, sendBuffer.Length, sendBuffer);
                        //}
                      
                        alreadyFinished += writeLength;
                        logNet.RecordMessage(HslMessageDegree.INFO, "已读取数据长度:" + alreadyFinished.ToString(), DateTime.Now.ToString());

                        Start += writeLength;
                    }
                  

                    return 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return -1;


        }

        public bool Init(string ADD, string IP)

        {
            Address = ADD;
            _IP = IP;
            Device.Area = libnodave.daveDB;
            Device.DBNumber = ushort.Parse(ADD.Substring(2, 2));
            Device.Start = 0;
            return Connect();
            

        }

        public bool Init(string IP)
        {
            _IP = IP;
            Device.Area = libnodave.daveDB;
            Device.DBNumber = 2;
            Device.Start = 0;
            return Connect();
        }

       public string daveStrerror(int code)
        {
            switch (code)
            {
                case 0: return "ok";
                case 6: return "the CPU does not support reading a bit block of length<>1";
                case 10: return "the desired item is not available in the PLC";
                case 3: return "the desired item is not available in the PLC (200 family)";
                case 5: return "the desired address is beyond limit for this PLC";
                case -124: return "the PLC returned a packet with no result data";
                case -125: return "the PLC returned an error code not understood by this library";
                case -126: return "this result contains no data";
                case -127: return "cannot work with an undefined result set";
                case -123: return "cannot evaluate the received PDU";
                case 7: return "Write data size error";
                case 1: return "No data from I/O module";
                case -128: return "Unexpected function code in answer";
                case -129: return "PLC responds with an unknown data type";
                case -1024: return "Short packet from PLC";
                case -1025: return "Timeout when waiting for PLC response";
                case 0x8000: return "function already occupied.";
                case 0x8001: return "not allowed in current operating status.";
                case 0x8101: return "hardware fault.";
                case 0x8103: return "object access not allowed.";
                case 0x8104: return "context is not supported. Step7 says:Function not implemented or error in telgram.";
                case 0x8105: return "invalid address.";
                case 0x8106: return "data type not supported.";
                case 0x8107: return "data type not consistent.";
                case 0x810A: return "object does not exist.";
                case 0x8301: return "insufficient CPU memory ?";
                case 0x8402: return "CPU already in RUN or already in STOP ?";
                case 0x8404: return "severe error ?";
                case 0x8500: return "incorrect PDU size.";
                case 0x8702: return "address invalid."; ;
                case 0xd002: return "Step7:variant of command is illegal.";
                case 0xd004: return "Step7:status for this command is illegal.";
                case 0xd0A1: return "Step7:function is not allowed in the current protection level.";
                case 0xd201: return "block name syntax error.";
                case 0xd202: return "syntax error function parameter.";
                case 0xd203: return "syntax error block type.";
                case 0xd204: return "no linked block in storage medium.";
                case 0xd205: return "object already exists.";
                case 0xd206: return "object already exists.";
                case 0xd207: return "block exists in EPROM.";
                case 0xd209: return "block does not exist/could not be found.";
                case 0xd20e: return "no block present.";
                case 0xd210: return "block number too big.";
                //	case 0xd240: return "unfinished block transfer in progress?";  // my guess
                case 0xd240: return "Coordination rules were violated.";
                /*  Multiple functions tried to manipulate the same object.
                    Example: a block could not be copied,because it is already present in the target system
                    and
                */
                case 0xd241: return "Operation not permitted in current protection level.";
                /**/
                case 0xd242: return "protection violation while processing F-blocks. F-blocks can only be processed after password input.";
                case 0xd401: return "invalid SZL ID.";
                case 0xd402: return "invalid SZL index.";
                case 0xd406: return "diagnosis: info not available.";
                case 0xd409: return "diagnosis: DP error.";
                case 0xdc01: return "invalid BCD code or Invalid time format?";
                default: return "no message defined!";
            }
        }
        public struct DeviceAddress
        {
            public int Area;
            public int Start;
            public ushort DBNumber;
            public ushort DataSize;
            public ushort CacheIndex;
            public byte Bit;



        }

        public float ReadFloat()
        {
            if (dc != null)
            {
                dc.readBytes(Device.Area, Device.DBNumber, Device.Start, 4, null);
                return dc.getFloat();

            }
            return 0;
        }

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
                _Uint16s = data.ToArray();
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




        public int WriteFloat(float value)

        {
            byte[] b = BitConverter.GetBytes(value);
            Array.Reverse(b);

            return dc == null ? -1 : dc.writeBytes(Device.Area, Device.DBNumber, Device.Start, 4, b);

        }
        public int WriteInt32(int value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            return dc == null ? -1 : dc.writeBytes(Device.Area, Device.DBNumber, Device.Start, 4, b);
        }

        public int WriteUint32(uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            return dc == null ? -1 : dc.writeBytes(Device.Area, Device.DBNumber, Device.Start, 4, b);

        }
        public int WriteInt16(short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            return dc == null ? -1 : dc.writeBytes(Device.Area, Device.DBNumber, Device.Start, 2, b);
        }
        public int WriteUint16(ushort value)
        {
            if (dc != null)
            {
                byte[] b = BitConverter.GetBytes(value);
                Array.Reverse(b);
                return dc == null ? -1 : dc.writeBytes(Device.Area, Device.DBNumber, Device.Start, 2, b);
            }
            return 0;

        }

        public int WriteBit(bool bit)
        {

            return dc == null ? -1 : dc.writeBits(Device.Area, Device.DBNumber, Device.Start * 8 + Device.Bit, 1, bit ? new byte[] { 0x1 } : new byte[] { 0x00 });
        }
        public bool ReadBit()
        {
            if (dc != null)
            {
                dc.readBits(Device.Area, Device.DBNumber, Device.Start * 8 + Device.Bit, 1, null);
                return dc.getS8() != 0;
            }
            return false;
        }
        public ushort ReadUInt16()
        {
            if (dc != null)
            {
                dc.readBytes(Device.Area, Device.DBNumber, Device.Start, 2, null);
                return (ushort)dc.getU16();
            }
            return 0;
        }
        public short ReadInt16()
        {
            if (dc != null)
            {
                dc.readBytes(Device.Area, Device.DBNumber, Device.Start, 2, null);
                return (short)dc.getU16();
            }
            return 0;
        }
        public uint ReadUInt32()
        {
            if (dc != null)
            {
                dc.readBytes(Device.Area, Device.DBNumber, Device.Start, 4, null);
                return (uint)dc.getU32();
            }
            return 0;
        }
        public int ReadInt32()
        {
            if (dc != null)
            {
                dc.readBytes(Device.Area, Device.DBNumber, Device.Start, 4, null);
                return (int)dc.getU32();
            }
            return 0;
        }
        public DeviceAddress GetDeviceAddress(string address)
        {
            DeviceAddress plcAddr = new DeviceAddress();
            if (string.IsNullOrEmpty(address) || address.Length < 2) return plcAddr;
            if (address.Substring(0, 2) == "DB")
            {
                int index = 2;
                for (int i = index; i < address.Length; i++)
                {
                    if (!char.IsDigit(address[i]))
                    {
                        index = i; break;
                    }
                }
                plcAddr.Area = libnodave.daveDB;
                plcAddr.DBNumber = ushort.Parse(address.Substring(2, index - 2));
                string str = address.Substring(index + 1);
                if (!char.IsDigit(str[0]))
                {
                    for (int i = 1; i < str.Length; i++)
                    {
                        if (char.IsDigit(str[i]))
                        {
                            index = i; break;
                        }
                    }
                    if (str[2] == 'W')
                    {
                        int index1 = str.IndexOf('.');
                        if (index1 > 0)
                        {
                            int start = int.Parse(str.Substring(3, index1 - 3));
                            byte bit = byte.Parse(RightFrom(str, index1));
                            plcAddr.Start = bit > 8 ? start : start + 1;
                            plcAddr.Bit = (byte)(bit > 7 ? bit - 8 : bit);
                            return plcAddr;
                        }
                    }
                    str = str.Substring(index);
                }
                index = str.IndexOf('.');
                if (index < 0)
                    plcAddr.Start = int.Parse(str);
                else
                {
                    plcAddr.Start = int.Parse(str.Substring(0, index));
                    plcAddr.Bit = byte.Parse(RightFrom(str, index));
                }
            }
          
            return plcAddr;
        }
        public void Dispose()
        {
            dc.disconnectPLC();

        }

        private void GetResult()
        {

        }

        public string RightFrom(string text, int index)
        {
            return text.Substring(index + 1, text.Length - index - 1);
        }
        /// <summary>
        /// 从缓存中提取byte数组结果
        /// </summary>
        /// <param name="buffer">缓存数据</param>
        /// <param name="index">索引位置</param>
        /// <param name="length">读取的数组长度</param>
        /// <returns>byte数组对象<eturns>
        public virtual byte[] TransByte(byte[] buffer, int index, int length)
        {
            byte[] tmp = new byte[length];
            Array.Copy(buffer, index, tmp, 0, length);
            return tmp;
        }

       
    }
}