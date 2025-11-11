using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class Flash : Form
    {
        public Flash()
        {
            InitializeComponent();
        }

        private void Flash_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.Wheat;
            this.BackColor = Color.Wheat;
            timer1.Interval = 50;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // tăng dần progress
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 2;

                // hiệu ứng mờ dần khi gần xong
                if (progressBar1.Value > 80)
                    this.Opacity -= 0.05;
            }
            else
            {
                // khi đầy 100%, dừng timer và đóng form
                timer1.Stop();
                this.Close();
            }
        }
    }
}
