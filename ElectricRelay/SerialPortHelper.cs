using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricRelay
{
    /// <summary>
    /// 串口连接帮助类
    /// </summary>
    public class SerialPortHelper:IDisposable
    {
        #region 变量

        private SerialPort _port;

        #endregion

        #region 属性

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; }

        /// <summary>
        /// 控制协议
        /// </summary>
        public Handshake Handshake { get; set; }

        /// <summary>
        /// 输入缓冲区字节数
        /// </summary>
        public int ReceivedBytesThreshold { get; set; }

        /// <summary>
        /// 串口是否开启
        /// </summary>
        public bool IsOpen
        {
            get
            {
                if (_port == null)
                {
                    return false;
                }
                return _port.IsOpen;
            }
        }

        #endregion

        #region 事件

        public event ReceivedEventHandle DataReceived;

        public event SerialPortErrorEventHandle SerialPortError;

        public event SerialErrorReceivedEventHandler ErrorReceived;

        public event SerialPinChangedEventHandler PinChanged;

        public event EventHandler Disposed;

        #endregion

        #region 构造函数

        /// <summary>
        /// 串口构造函数
        /// </summary>
        public SerialPortHelper(string portName)
            : this(portName, 9600, 8, Parity.None, StopBits.One, Handshake.None, 8)
        {

        }

        /// <summary>
        /// 串口构造函数
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <param name="baudRate">8波特率</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="parity">奇偶校验位</param>
        /// <param name="stopBits">停止位数</param>
        /// <param name="handshake">控制协议</param>
        /// <param name="receivedBytesThreshold">输入缓冲区字节数</param>
        public SerialPortHelper(
            string portName,
            int baudRate,
            int dataBits,
            Parity parity,
            StopBits stopBits,
            Handshake handshake,
            int receivedBytesThreshold)
        {
            PortName = portName;
            BaudRate = baudRate;
            DataBits = dataBits;
            Parity = parity;
            StopBits = stopBits;
            Handshake = handshake;
            ReceivedBytesThreshold = receivedBytesThreshold;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化串口
        /// </summary>
        public void Init()
        {
            if (string.IsNullOrEmpty(PortName))
            {
                throw new ArgumentNullException(nameof(PortName));
            }
            _port = new SerialPort(PortName)
            {
                BaudRate = BaudRate,
                DataBits = DataBits,
                Parity = Parity,
                StopBits = StopBits,
                Handshake = Handshake,
                ReceivedBytesThreshold = ReceivedBytesThreshold,
                //Encoding = Encoding.GetEncoding("GB2312"),
                DtrEnable = true
            };
            _port.DataReceived += _port_DataReceived;
            _port.PinChanged += PinChanged;
            _port.Disposed += Disposed;
            _port.ErrorReceived += _port_ErrorReceived;
        }

        private void _port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ErrorReceived?.Invoke(sender, e);
        }

        /// <summary>
        /// 处理接收到的消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(20);
                DataReceived?.Invoke(this, new ReceivedArgs(_port.ReadExisting()));
            }
            catch (Exception ex)
            {
                SerialPortError?.Invoke(this, new SerialPortErrorArgs(ex));
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 开启串口
        /// </summary>
        /// <returns></returns>
        public bool OpenPort()
        {
            try
            {
                _port.Open();
                return true;
            }
            catch (Exception ex)
            {
                SerialPortError?.Invoke(this, new SerialPortErrorArgs(ex));
                return false;
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void ClosePort()
        {
            try
            {
                _port?.Close();
            }
           catch(Exception ex)
            {
                SerialPortError?.Invoke(this, new SerialPortErrorArgs(ex));
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg">字符串</param>
        public void Send(string msg)
        {
            Send(HexStringToByteArray(msg));
        }

        public static byte[] HexStringToByteArray(string s)
        {

            s = s.Replace(" ", "");

            byte[] buffer = new byte[s.Length / 2];

            for (int i = 0; i < s.Length; i += 2)

                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);

            return buffer;

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="buffer">字节流</param>
        public bool Send(byte[] buffer)
        {
            try
            {
                //if (_port == null || !_port.IsOpen)
                //{
                //    OpenPort();
                //}
                if(_port.IsOpen)
                {
                    _port?.Write(buffer, 0, buffer.Length);
                    return true;
                }
                else
                {
                    SerialPortError?.Invoke(this, new SerialPortErrorArgs(new Exception("端口关闭")));
                    return false;
                }
            }
            catch (Exception ex)
            {
                SerialPortError?.Invoke(this, new SerialPortErrorArgs(ex));
                return false;
            }
        }

        /// <summary>
        /// 释放串口资源
        /// </summary>
        public void Dispose()
        {
            _port?.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// 接收信息委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ReceivedEventHandle(object sender, ReceivedArgs e);

    /// <summary>
    /// 串口异常错误处理委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SerialPortErrorEventHandle(object sender, SerialPortErrorArgs e);

    /// <summary>
    /// 接收参数类
    /// </summary>
    public class ReceivedArgs : EventArgs
    {
        /// <summary>
        /// 接收的数据字符串
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg"></param>
        public ReceivedArgs(string msg)
        {
            Message = msg;
        }
    }

    /// <summary>
    /// 串口错误参数类
    /// </summary>
    public class SerialPortErrorArgs : EventArgs
    {
        /// <summary>
        /// 异常提示信息
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Error { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public SerialPortErrorArgs(Exception e)
        {
            Error = e;
        }
        public SerialPortErrorArgs(string msg,Exception e)
        {
            Message = msg;
            Error = e;
        }
    }
}
