using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    public partial class Menu : Form
    {
        static musicBoxForm music;
        string[] levels = { "","1", "2", "3", "4", "5" };
        int selectedLevel;
        public Menu()
        {
            InitializeComponent();
            this.selectedLevel = 2;
            label5.Text = levels[this.selectedLevel];
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            musicBoxLabel.BackColor = Color.Transparent;        
        }


        private void label2_Click(object sender, EventArgs e)
        {
            Game game = new Game(selectedLevel, this);                       
            this.Hide();
            game.ShowDialog();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            startLabel.ForeColor = Color.GreenYellow;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            startLabel.ForeColor = Color.DarkCyan;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (selectedLevel - 1 > 0) 
            {
                label5.Text = this.levels[--this.selectedLevel];
            }
            else
            {
                this.selectedLevel = 5;
                label5.Text = this.levels[this.selectedLevel];
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (selectedLevel + 1 < 6)
            {
                label5.Text = this.levels[++this.selectedLevel];
            }
            else
            {
                this.selectedLevel = 1;
                label5.Text = this.levels[this.selectedLevel];

            }
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.GreenYellow;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.DarkCyan;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.GreenYellow;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.DarkCyan;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            exitLabel.ForeColor = Color.GreenYellow;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            exitLabel.ForeColor = Color.DarkCyan;
        }

        private void musicBoxLabel_Click(object sender, EventArgs e)
        {
            if (music == null || music.IsDisposed)
            {
                music = new musicBoxForm();
                music.Show();
            }
        }

        private void musicBoxLabel_MouseEnter(object sender, EventArgs e)
        {
            musicBoxLabel.ForeColor = Color.GreenYellow;
        }

        private void musicBoxLabel_MouseLeave(object sender, EventArgs e)
        {
            musicBoxLabel.ForeColor = Color.DarkCyan;
        }
    }
}
