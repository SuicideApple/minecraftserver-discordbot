using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ushort result;
            ushort.TryParse(textBox5.Text, out result);
            WpfApp1.Properties.Settings.Default.PlayingText = textBox1.Text;
            WpfApp1.Properties.Settings.Default.BotToken = textBox3.Text;
            WpfApp1.Properties.Settings.Default.Path = textBox4.Text;
            WpfApp1.Properties.Settings.Default.MinRam = comboBox1.Text;
            WpfApp1.Properties.Settings.Default.MaxRam = comboBox2.Text;
            WpfApp1.Properties.Settings.Default.ServerPort = result;
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings Saved", "Saved");
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = WpfApp1.Properties.Settings.Default.PlayingText;
            textBox3.Text = WpfApp1.Properties.Settings.Default.BotToken;
            textBox4.Text = WpfApp1.Properties.Settings.Default.Path;
            comboBox1.Text = WpfApp1.Properties.Settings.Default.MinRam;
            comboBox2.Text = WpfApp1.Properties.Settings.Default.MaxRam;
            textBox5.Text = Convert.ToString(WpfApp1.Properties.Settings.Default.ServerPort);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
