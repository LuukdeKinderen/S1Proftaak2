using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;
using System.Timers;




namespace TrainScheme1
{
    public partial class Form1 : Form
    {
        private SerialPort SerialPort1 = new SerialPort("COM14", 250000);
        private System.Timers.Timer timer = new System.Timers.Timer();
        private TrainBehaviour trainBehaviour = new TrainBehaviour();
        private UserManager userManager = new UserManager();
        private bool onHold = false;
        private WebRequest request = new WebRequest("http://192.168.50.6");

        public Form1()
        {
            SerialPort1.Open();

            SerialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            InitializeComponent();

            for (int i = 0; i < 240; i++)
            {

                PictureBox p = new PictureBox
                {
                    BackColor = SystemColors.ActiveBorder,
                    Name = "pictureBox1",
                    Size = new Size(9, 9)
                };

                tableLayoutPanel1.Controls.Add(p);
            }


            timer.Interval = 500;
            timer.Elapsed += CycleTick;
            timer.Start();

        }



        void CycleTick(object source, EventArgs e)
        {
            if (!onHold)
            {
                trainBehaviour.MoveTrains();
                DrawRails(trainBehaviour.GetRails());
                string s = GenerateHexString(trainBehaviour.GetRails());
                SerialPort1.Write(s);
                //Debug.WriteLine(s);
            }
        }

        private void Pausebutton(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            SerialPort sp = (SerialPort)sender;

            sp.NewLine = "~";
            string message = sp.ReadLine();
            char protocol = message[5];
            string command = message.Substring(7);

            switch (protocol)
            {
                case '3':
                    // TrainSelect Stuff
                    switch (command)
                    {
                        case "GetStations":
                            SerialPort1.Write("FxFF 3 " + trainBehaviour.StationStr() + "~");
                            onHold = true;
                            break;
                        default:
                            int stationIndex;
                            string UID = command.Split(',')[0];
                            if(int.TryParse(command.Split(',')[1], out stationIndex)){

                                Debug.WriteLine(userManager.GetUser(UID).checken(trainBehaviour.GetStation(stationIndex)));
                            }
                            onHold = false;
                            break;
                    }
                    break;


                case '1':
                    Debug.WriteLine("testie: "+command);
                    break;


                case '2':
                    string uid = command.Split(',')[0];
                    double _addition = 0;
                    if (double.TryParse(command.Split(',')[1], out _addition)){
                        double _current = request.GetData(uid);
                        double _new = _current + _addition;
                        request.SendGetData(uid, _new.ToString(), "0");
                        SerialPort1.Write("FxFF 4 " + request.GetData(uid).ToString() + "~");
                        Debug.WriteLine(command);
                    }
                    onHold = false;
                    break;


                default:
                    break;
            }

        }

        private string GenerateHexString(Rail[,] rails)
        {
            Rail[] allRails = new Rail[240];
            for (int ri = 0; ri < rails.GetLength(0); ri++)
            {
                for (int r = 0; r < rails.GetLength(1); r++)
                {
                    allRails[(ri * rails.GetLength(1)) + r] = rails[ri, r];
                }
            }

            string longString = "FxFF 9 ";



            for (int i = 0; i < allRails.Length; i++)
            {
                if (allRails[i].GetTrains().Count > 0)
                {
                    Color c = allRails[i].GetTrains()[0].GetColor();
                    longString += i.ToString("000") + c.R.ToString("000") + c.G.ToString("000") + c.B.ToString("000");
                }
                else if (allRails[i].GetWagon() != null)
                {
                    Color c = allRails[i].GetWagon().GetColor();
                    longString += i.ToString("000") + c.R.ToString("000") + c.G.ToString("000") + c.B.ToString("000");
                }
            }


            longString += "~";
            return longString;

        }


        private void DrawRails(Rail[,] rails)
        {
            for (int ri = 0; ri < rails.GetLength(0); ri++)
            {
                for (int r = 0; r < rails.GetLength(1); r++)
                {

                    if (rails[ri, r].GetTrains().Count > 0)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        Train train = rails[ri, r].GetTrains()[0];
                        p.BackColor = train.GetColor();
                    }

                    else if (rails[ri, r].GetWagon() != null)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        Wagon wagon = rails[ri, r].GetWagon();
                        p.BackColor = wagon.GetColor();
                    }
                    else if (rails[ri, r].GetStation() != null)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        if (rails[ri, r].GetStation().CentralStation())
                        {
                            p.BackColor = System.Drawing.ColorTranslator.FromHtml("#4a4a4a");
                        }
                        else
                        {
                            p.BackColor = System.Drawing.ColorTranslator.FromHtml("#878787");
                        }

                    }
                    else
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        p.BackColor = System.Drawing.SystemColors.Control;
                    }

                }
            }




        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
