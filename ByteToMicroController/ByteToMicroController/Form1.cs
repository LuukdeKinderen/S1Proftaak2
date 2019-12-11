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
using System.Net;
using System.Text;

namespace ByteToMicroController
{


    public partial class Form1 : Form
    {

        String[] ports;
        SerialPort serialPort1 = new SerialPort("COM4", 9600);
        public Form1()
        {
            InitializeComponent();
            getAvailableComPorts();
            foreach (string serialPort1 in ports)
            {
                comboBox1.Items.Add(serialPort1);
                Console.WriteLine(serialPort1);
                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            
            }
        }

        private void loop()
        {
            serialPort1.PortName = comboBox1.SelectedItem.ToString();
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

        void getAvailableComPorts()
        {

         ports = SerialPort.GetPortNames(); 

        }
            
        void test()
        {
            string urlString = "192.168.2.16/Fetch.php?UID=";
            urlString += "test";
            urlString += "&bal=";
            urlString += "19";
            urlString += "&in=";
            urlString += "true";

            WebRequest request = WebRequest.Create(urlString);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseFromServer = response.ToString();

            Console.WriteLine(responseFromServer);
        }
        
    }
}
