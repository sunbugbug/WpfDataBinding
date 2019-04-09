using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using WpfDataBinding.Model;

using System.Net.Sockets;
using System.Net;

namespace WpfDataBinding
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        SensorValue sv = new SensorValue();
        DispatcherTimer _timer;

        Socket transferSock;
        string str;

        public MainWindow()
        {
            InitializeComponent();

            IPAddress[] addresslist = Dns.GetHostAddresses("192.168.137.240");

            transferSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            transferSock.Connect(new IPEndPoint(addresslist[0], 1234));

            _timer = new DispatcherTimer();
            _timer.Tick += stringChange;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            _timer.Start();

        }

        void stringChange(object sender, EventArgs e)
        {
            byte[] receivedStr = new byte[10];

            transferSock.Receive(receivedStr);

            str = Encoding.Default.GetString(receivedStr);
            this.myLabel.Content = str;
        }

    }
}
