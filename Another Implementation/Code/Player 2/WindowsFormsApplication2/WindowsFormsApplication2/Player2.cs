using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Player2 : Form
    {
        GamePanel gp;
        public Player2()
        {
            gp = new GamePanel();
            
            InitializeComponent();
            this.Controls.Add(gp);
            
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                GamePanel.p1.dyU = -Constants.PLAYER_SPEED;
            if (e.KeyCode == Keys.Down)
                GamePanel.p1.dyD = Constants.PLAYER_SPEED;
            if (e.KeyCode == Keys.Left)
                GamePanel.p1.dxL = -Constants.PLAYER_SPEED;
            if (e.KeyCode == Keys.Right)
                GamePanel.p1.dxR = Constants.PLAYER_SPEED;
            
            
            

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                GamePanel.p1.dyU =0;
            if (e.KeyCode == Keys.Down)
                GamePanel.p1.dyD =0;
            if (e.KeyCode == Keys.Left)
                GamePanel.p1.dxL = 0;
            if (e.KeyCode == Keys.Right)
                GamePanel.p1.dxR = 0;
            if (e.KeyCode == Keys.D)
                GamePanel.p1.fire("RIGHT");
            if (e.KeyCode == Keys.A)
                GamePanel.p1.fire("LEFT");
            if (e.KeyCode == Keys.S)
                GamePanel.p1.fire("DOWN");
            if (e.KeyCode == Keys.W)
                GamePanel.p1.fire("UP");

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            gp.StartGame(); 
        }

        //private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == Keys.Up)
        //        GamePanel.p1.dy = 0;
        //    if (e.KeyCode == Keys.Down)
        //        GamePanel.p1.dy = 0;
        //    if (e.KeyCode == Keys.Left)
        //        GamePanel.p1.dx = 0;
        //    if (e.KeyCode == Keys.Right)
        //        GamePanel.p1.dx = 0; 
        //}
    }
}
