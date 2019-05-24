using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WindowsFormsApp1
{
    public class LIBnodavePLC
    {
        private static readonly object _async = new object();
        libnodave.daveOSserialType fds;
        libnodave.daveInterface di;
        internal libnodave.daveConnection dc;
        int _rack = 0;
        int _slot = 0;
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

        DateTime _sendTime;
        DateTime _readtime;
        float[] _float;
        Int16[] _int16s;
        Int32[] _int32;
        byte[] content;
        bool _closed=true;
        DateTime _closeTime = DateTime.Now;
        int TimeOut = 10000;
        public bool IsConnect { set; get; }
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
                    dc = new libnodave.daveConnection(di, 0, _rack, _slot);
                    if (0 == dc.connectPLC())
                    {
                        _closed = false;
                        return true;
                    }
                }
                if (dc != null) dc.disconnectPLC();
                libnodave.closeSocket(fds.rfd);
            }
            _closed = true;
            return false;
        }
        // DeviceAddress device = new DeviceAddress();
        public DeviceAddress Device;

        public byte[] ReadBytes( int len)//从PLC中读取自己数组
        {
            List<byte> bytesContent = new List<byte>();
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
                        ushort readLength = (ushort)Math.Min(len - alreadyFinished, 200);
                        int res = -1;
                        buffer = new byte[readLength];
                        res = dc == null ? -1 : dc.readBytes(Device.Area, Device.DBNumber, Start, readLength, buffer);
                        bytesContent.AddRange(buffer);
                        alreadyFinished += readLength;
                        Start+= readLength;

                    }
                   
                       _readtime = DateTime.Now;
                    
                   

                    return bytesContent.ToArray();

                }
            }
            catch (Exception)
            {

                throw;
            }
         
            return null;
        }


        public int WriteBytes(byte[] a,bool Isstart=false)
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
                        byte[] sendBuffer = TransByte(a, alreadyFinished, writeLength);
                       int res = dc.writeBytes(Device.Area, Device.DBNumber, Start, sendBuffer.Length, sendBuffer);
                        alreadyFinished += writeLength;
                        Start += writeLength;
                    }
                    //if (res == 0)

                    //_closed = true; dc = null; _closeTime = DateTime.Now;
                   
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

            _IP = IP;
            Device.Area = libnodave.daveDB;
            Device.DBNumber = ushort.Parse(ADD.Substring(2, 1));
            Device.Start = 0;
            return Connect();


        }

        public bool Init( string IP)
        {
            _IP = IP;
            Device.Area = libnodave.daveDB;
            Device.DBNumber = 2;
            Device.Start = 0;
            return Connect();
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

        public  float  ReadFloat()
        {
            if (dc!=null)
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
            return WriteBytes(bytes,true);

        }
        public int  WriteFloats(float[] flaots)
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
            return    WriteBytes(bytes,true);
            
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
                _int32 = data.ToArray();
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
            byte[] readbytes=   ReadBytes(length*4);
             
                for (int i = 0; i < length; i++)
                {
                   //floats.Add(dc.getFloat());
                   floats.Add(BitConverter.ToSingle(readbytes, i * 4));
                    
                }
                _float =   floats.ToArray();
                _readtime=DateTime.Now;
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
                _int16s = data.ToArray();
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
         
           return dc==null?-1:dc.writeBytes(Device.Area, Device.DBNumber, Device.Start, 4, b);

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
            if (dc!=null)
            {
                byte[] b = BitConverter.GetBytes(value);
                Array.Reverse(b);
                return dc == null ? -1 : dc.writeBytes(Device.Area, Device.DBNumber, Device.Start, 2, b);
            }
            return 0;
          
        }

        public int WriteBit(bool bit)
        {
            
            return dc == null ? -1 : dc.writeBits(Device.Area, Device.DBNumber, Device.Start*8+Device.Bit, 1,bit? new byte[] {0x1 }:new byte[] { 0x00} );
        }
       public bool ReadBit()
        {
            if (dc!=null)
            {
                dc.readBits(Device.Area, Device.DBNumber, Device.Start * 8 + Device.Bit, 1, null);
                return dc.getS8()!=0;
            }
            return false;
        }
        public ushort ReadUInt16()
        {
            if (dc!=null)
            {
                dc.readBytes(Device.Area, Device.DBNumber, Device.Start, 2, null);
              return  (ushort)dc.getU16();
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
                            byte bit = byte.Parse(RightFrom(str,index1));
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
                    plcAddr.Bit = byte.Parse(RightFrom(str,index));
                }
            }
            
            return plcAddr;
        }
        public void Dispose()
        {
            dc.disconnectPLC();
       
        }

        public string RightFrom( string text, int index)
        {
            return text.Substring(index + 1, text.Length - index - 1);
        }
        /// <summary>
        /// 从缓存中提取byte数组结果
        /// </summary>
        /// <param name="buffer">缓存数据</param>
        /// <param name="index">索引位置</param>
        /// <param name="length">读取的数组长度</param>
        /// <returns>byte数组对象</returns>
        public virtual byte[] TransByte(byte[] buffer, int index, int length)
        {
            byte[] tmp = new byte[length];
            Array.Copy(buffer, index, tmp, 0, length);
            return tmp;
        }
     


    }
}