using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlcTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SiemensPLC plc = new SiemensPLC();


        private void Button2_Click(object sender, EventArgs e)
        {
            var ip = textBox1.Text;
            plc.Connect(SiemensPLCS.S300, ip);
            var conn = Common.PLC1.SiementsConnection();
            if (conn)
            {
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.Red;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var addres = textBox2.Text;
            var takeid = textBox3.Text;
            var coninfo = textBox5.Text;
            var code = textBox6.Text;

            plc.WriteBindingBox(addres, takeid, coninfo, code);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var addres = textBox4.Text;
            var values =  plc.ReadInt(addres);

            textBox7.Text = values.ToString();
        }
    }
    
}
