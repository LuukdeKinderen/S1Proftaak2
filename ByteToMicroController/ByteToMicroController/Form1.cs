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
        SerialPort serialPort1 = new SerialPort("COM4", 9600);
        public Form1()
        {
            InitializeComponent();
        }

        private void loop()
        {

            serialPort1.Open();
            serialPort1.ReadLine();
            string receivePayload = serialPort1.ReadLine();
            OutputBox.Text = receivePayload;
            Console.WriteLine(receivePayload);

            serialPort1.Close();

            string inputString = receivePayload;
            char[] inputChar = inputString.ToCharArray();
            char adress = inputChar[5];

            string substring = inputString.Substring((inputString.IndexOf("FxFF") + 7), (inputString.IndexOf("FxF0") - 7));
            OutputBox2.Text = substring;
            OutputBox3.Text = adress.ToString();


            loop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loop();
        }
            
        
    }
}
