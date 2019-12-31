using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectricRelay
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPortHelper _serialPort;

        private byte[] oneOn = new byte[] { 0x55, 0x01, 0x01, 0x02, 0x00, 0x00, 0x00, 0x59 };
        private byte[] twoOn = new byte[] { 0x55, 0x01, 0x01, 0x00, 0x02, 0x00, 0x00, 0x59 };
        private byte[] threeOn = new byte[] { 0x55, 0x01, 0x01, 0x00, 0x00, 0x02, 0x00, 0x59 };
        private byte[] fourOn = new byte[] { 0x55, 0x01, 0x01, 0x00, 0x00, 0x00, 0x02, 0x59 };
        private byte[] oneDown = new byte[] { 0x55, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x58 };
        private byte[] twoDown = new byte[] { 0x55, 0x01, 0x01, 0x00, 0x01, 0x00, 0x00, 0x58 };
        private byte[] threeDown = new byte[] { 0x55, 0x01, 0x01, 0x00, 0x00, 0x01, 0x00, 0x58 };
        private byte[] fourDown = new byte[] { 0x55, 0x01, 0x01, 0x00, 0x00, 0x00, 0x01, 0x58 };

        //两组编码都可以控制4路继电器的路的开闭
        //private byte[] oneOn = new byte[] { 0x55, 0x01, 0x12, 0x00, 0x00, 0x00, 0x01, 0x69 };
        //private byte[] twoOn = new byte[] { 0x55, 0x01, 0x12, 0x00, 0x00, 0x00, 0x02, 0x6A };
        //private byte[] threeOn = new byte[] { 0x55, 0x01, 0x12, 0x00, 0x00, 0x00, 0x03, 0x6B };
        //private byte[] fourOn = new byte[] { 0x55, 0x01, 0x12, 0x00, 0x00, 0x00, 0x04, 0x6C };
        //private byte[] oneDown = new byte[] { 0x55, 0x01, 0x11, 0x00, 0x00, 0x00, 0x01, 0x68 };
        //private byte[] twoDown = new byte[] { 0x55, 0x01, 0x11, 0x00, 0x00, 0x00, 0x02, 0x69 };
        //private byte[] threeDown = new byte[] { 0x55, 0x01, 0x11, 0x00, 0x00, 0x00, 0x03, 0x6A };
        //private byte[] fourDown = new byte[] { 0x55, 0x01, 0x11, 0x00, 0x00, 0x00, 0x04, 0x6B };

        private string _backString;

        public MainWindow()
        {
            InitializeComponent();

            _serialPort = new SerialPortHelper("COM3");
            //_serialPort.BaudRate = 9600;
            //_serialPort.Parity = Parity.None;
            //_serialPort.StopBits = StopBits.One;
            //_serialPort.DataBits = 8;
            _serialPort.Init();
            _serialPort.SerialPortError += SerialPortError;
            //_serialPort.DataReceived += ReceivedMethod;
        }

        private void SerialPortError(object sender, SerialPortErrorArgs e)
        {
            MessageBox.Show(e.Error.ToString());
        }

        private void ReceivedMethod(object sender, ReceivedArgs e)
        {
            _backString = e.Message;
        }
        private void OpenOne_Click(object sender, RoutedEventArgs e)
        {
            //_serialPort.Open();
            //_serialPort.Write(oneOn,0,oneOn.Length);
            //byte[] recData = new byte[_serialPort.BytesToRead];
            //_serialPort.Read(recData, 0, recData.Length);
            //_serialPort.Close();
            if(_serialPort.IsOpen)
            {
                _serialPort.Send(oneOn);
            }
        }

        private void CloseOne_Click(object sender, RoutedEventArgs e)
        {
            //_serialPort.Open();
            //_serialPort.Write(oneDown,0,oneDown.Length);
            //byte[] recData = new byte[_serialPort.BytesToRead];
            //_serialPort.Read(recData, 0, recData.Length);
            //_serialPort.Close();

            if (_serialPort.IsOpen)
            {
                _serialPort.Send(oneDown);
            }
        }

        private void OpenTwo_Click(object sender, RoutedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Send(twoOn);
            }
        }

        private void CloseTwo_Click(object sender, RoutedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Send(twoDown);
            }
        }

        private void OpenThree_Click(object sender, RoutedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Send(threeOn);
            }
        }

        private void CloseThree_Click(object sender, RoutedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Send(threeDown);
            }
        }

        private void OpenFour_Click(object sender, RoutedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Send(fourOn);
            }
        }

        private void CloseFour_Click(object sender, RoutedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Send(fourDown);
            }
        }

        private void OpenCom_Click(object sender, RoutedEventArgs e)
        {
            _serialPort.OpenPort();
        }

        private void CloseCom_Click(object sender, RoutedEventArgs e)
        {
            _serialPort.ClosePort();
        }
    }
}
