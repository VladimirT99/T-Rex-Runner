using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Game
{
    public partial class Form1 : Form
    {
        bool gameOver = false;
        bool inJump;
        int jumpSpeed;
        int force;
        int score;
        int obstacleSpeed = 15;
        Random r = new Random();
        int position;


        public Form1()
        {
            InitializeComponent();
            restartGame();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TRex.Top += jumpSpeed;
            lblScore.Text= "Score: " + score;
            if(inJump == true && force < 0)
            {
                inJump = false;
            }
            
            if(inJump == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if(TRex.Top > 224 && inJump == false )
            {
                force = 12;
                TRex.Top = 225;
                jumpSpeed = 0;
            }

            foreach(Control c in this.Controls)
            {
                if(c is PictureBox && (string)c.Tag == "Obstacles")
                {
                    c.Left -= obstacleSpeed;
                    if(c.Left < -50)
                    {
                        c.Left = this.ClientSize.Width + r.Next(150, 500) + c.Width * 7;
                        c.Top = 225;
                        score++;
                    }

                    if (TRex.Bounds.IntersectsWith(c.Bounds))
                    {
                        timer1.Stop();
                        lblScore.Text = "GAME OVER\n" + "Your final score is " + score + "\n" + "Press R to restart.";
                        gameOver = true;
                    }
                    if(score == 10)
                    {
                        obstacleSpeed = 20;
                    }
                    if (score == 20)
                    {
                        obstacleSpeed = 25;
                    }
                    if(score == 30)
                    {
                        obstacleSpeed = 30;
                    }
                    if(score == 35)
                    {
                        timer1.Stop();
                        lblScore.Text = "YOU WON!";
                        gameOver = true;
                    }
                }
            }
        }

        public void restartGame()
        {
            gameOver = false;
            inJump = false;
            jumpSpeed = 0;
            force = 12;
            score = 0;
            lblScore.Text = "Score: " + score;
            TRex.Top = 225;
            foreach(Control c in this.Controls)
            {
                if (c is PictureBox && (string)c.Tag == "Obstacles")
                {
                    position = this.ClientSize.Width + r.Next(200, 500);
                    c.Left = position;
                    c.Top = 225;
                }

            }
            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && inJump == false)
            {
                inJump = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (inJump == true)
            {
                inJump = false;
            }
            if (e.KeyCode == Keys.R && gameOver == true)
            {
                restartGame();
            }
        }
    }
}
