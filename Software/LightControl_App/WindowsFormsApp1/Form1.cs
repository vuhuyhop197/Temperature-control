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
using ZedGraph;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int TickStart, intMode = 1;
        int i = 0;
        double tam;
        private void Form1_Load(object sender, EventArgs e)
        {
            GraphPane myPane = zed.GraphPane;
            myPane.Title.Text = "Temperature";
            myPane.XAxis.Title.Text = "Time(s)";
            myPane.YAxis.Title.Text = "Temp(Cdeg)";
            RollingPointPairList list = new RollingPointPairList(6000);
            RollingPointPairList list1 = new RollingPointPairList(6000);

            LineItem curve = myPane.AddCurve("Set value", list, Color.Red, SymbolType.None);
            LineItem curve1 = myPane.AddCurve("Current Value", list1, Color.Blue, SymbolType.None);
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 30;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MajorStep = 5;

            zed.AxisChange();
            }
        private void PbConnect_Click(object sender, EventArgs e)
        {
            if (LbStatus.Text == "Disconnect")
            {
                try
                {
                    if (txtSetPoint.Text == string.Empty)
                    {
                        throw new Exception();
                    }
                    if (Convert.ToInt32(txtSetPoint.Text) > 75 || Convert.ToInt32(txtSetPoint.Text) < 30)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    Com.PortName = CbSecCom.Text;
                    Com.Open();
                    LbStatus.Text = "Connect";
                    PbConnect.Text = "Disconnect";
                    lberror.Text = string.Empty;
                    TickStart = Environment.TickCount;

                }
                catch (IndexOutOfRangeException)
                {
                    lberror.Text = "Set point is out of range! Temperature is in range of 30 - 75 deg.";
                }
                catch (Exception)
                {
                    lberror.Text = "Please enter set point!";
                }
                
            }
            else
            {
                Com.Close();
                LbStatus.Text = "Disconnect";
                PbConnect.Text = "Connect";
            }
        }
        int intlen = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if (intlen != ports.Length)
            {
                intlen = ports.Length;
                for (int j = 0; j < intlen; j++)
                {
                    CbSecCom.Items.Add(ports[j]);
                }
            }
           
        }
        private void PbSend_Click(object sender, EventArgs e)
        {
            string s;
            if (LbStatus.Text == "Connect")
            {
                if (txtDeadband.Enabled == true)
                {
                    s = "#" + "0" + "#" + "0" + "#" + "0" + "#" + txtSetPoint.Text + "#" + txtDeadband.Text + "\0";
                }
                else
                {
                    s = "#" + txtKp.Text + "#" + txtKi.Text + "#" + txtKd.Text + "#" + txtSetPoint.Text + "#" + txtDeadband.Text + "\0";
                }
                    int a = s.Length;
                Com.Write(Convert.ToString(a));
                Com.Write(s);
                //txtReceive.Text = s;
            }
        }
        private void OnCom(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string temp;
                temp = Com.ReadLine();
                Display(temp);
            }
            catch (System.IO.IOException error)
            {
                return;
            }
            catch (System.InvalidOperationException error)
            {
                return;
            }
        }
        private delegate void DlDisplay(string s);
        private void Display(string s)
        {
            if (txtReceive.InvokeRequired)
            {
                DlDisplay sd = new DlDisplay(Display);
                txtReceive.Invoke(sd, new object[] { s });
            }
            else
            {
                txtReceive.Text = Convert.ToString(s);
                draw(txtSetPoint.Text, txtReceive.Text);
            }
        }
        private void PbMode_Click(object sender, EventArgs e)
        {
            if (PbMode.Text == "SROLL")
            {
                intMode = 1;
                PbMode.Text = "COMPACT";
            }
            else
            {
                intMode = 0;
                PbMode.Text = "SROLL";
            }
        }

        private void PbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

     
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (LbStatus.Text == "Connect"&&OnOffCtl.Checked==false)
            {
                Com.Write("01");
                Com.Write("P");
                txtKd.Enabled = true;
                txtKi.Enabled = true;
                txtKp.Enabled = true;
                txtDeadband.Enabled = false;
            }
        }

        private void PidCtl_CheckedChanged(object sender, EventArgs e)
        {
            if (LbStatus.Text == "Connect"&&PidCtl.Checked==false)
            {
                Com.Write("01");
                Com.Write("O");
                txtDeadband.Enabled = true;
                txtKd.Enabled = false;
                txtKi.Enabled = false;
                txtKp.Enabled = false;
            }
        }
        private void draw (string set_point, string current)
        {
            double intsetpoint;
            double intcurrent;
            double.TryParse(set_point,out intsetpoint);
            double.TryParse(current, out intcurrent);
            
            if (intcurrent > 100.0)
            {
                intcurrent = tam;
            }
            else if (intcurrent < 15.0)
            {
                intcurrent = tam;
            }
            tam = intcurrent;
            if (zed.GraphPane.CurveList.Count <= 0)
            {
                return;
            }
            LineItem curve = zed.GraphPane.CurveList[0] as LineItem;
            LineItem curve1 = zed.GraphPane.CurveList[1] as LineItem;
            if (curve==null)
            {
                return;
            }
            if (curve1 == null)
            {
                return;
            }
            IPointListEdit list = curve.Points as IPointListEdit;
            IPointListEdit list1 = curve1.Points as IPointListEdit;
            if (list == null)
            {
                return;
            }
            if (list1 == null)
            {
                return;
            }
            double time = (Environment.TickCount - TickStart) / 1000;
            list.Add(time, intsetpoint);
            list1.Add(time, intcurrent);

            Scale xScale = zed.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                if (intMode == 1)
                {
                    xScale.Max = time + xScale.MajorStep;
                    xScale.Min = xScale.Max - 30;
                }
                if (intMode == 0)
                {
                    xScale.Max = time + xScale.MajorStep;
                    xScale.Min = 0;
                }
            }
            zed.AxisChange();
            zed.Invalidate();
        }

    }
}
