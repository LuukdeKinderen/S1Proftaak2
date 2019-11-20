using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace ByteToMicroController
{
    public partial class Form1 : Form
    {
        SerialPort serialPort1 = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            serialPort1.Write("1");
            serialPort1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            serialPort1.Write("yeet");
            byte[] byteArray = new byte[300];

            serialPort1.Read(byteArray, 0, 1);
            serialPort1.Close();
        }

        public static void SerialDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
        {

            SerialPort serialPort1 = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            serialPort1.Open();
            serialPort1.Write("yeet");
            byte[] byteArray = new byte[300];

            serialPort1.Read(byteArray, 0, 1);
            serialPort1.Close();
        }
            
        
    }
}
