using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.IO;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        //matrix for storing images for each button
        string[,] matrix = new string[4, 6];
        //array with images
        string[] images = new string[] {
            Path.Combine(Environment.CurrentDirectory, @"Images\p001.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p002.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p003.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p004.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p005.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p006.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p007.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p008.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p009.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p010.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p011.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p012.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p001.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p002.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p003.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p004.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p005.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p006.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p007.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p008.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p009.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p010.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p011.png"),
            Path.Combine(Environment.CurrentDirectory, @"Images\p012.png")
        };
        //counter in seconds - 10 seconds
        int counter = 10;
        int triesLeft = 2; //number of tries left
        bool secondPress = false;//check if it's the second button which is pressed
        //images for comparison
        string prevImage;
        string curImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showImages();
        }

        //press button to reveal image
        private void buttonClick(object sender, EventArgs e)
        {

            if (triesLeft != 0)
            {
                Button curButton = sender as Button;
                int b1 = int.Parse(curButton.Name.Substring(1, 1));
                int b2 = int.Parse(curButton.Name.Substring(2, 1));

                if (secondPress)
                {
                    curImage = matrix[b1, b2];
                    if (prevImage == curImage)
                    {
                        curButton.BackgroundImage = Image.FromFile(matrix[b1, b2]);
                        curButton.Enabled = false;
                        secondPress = false;
                    }
                    else
                    {
                        triesLabel.Text = "Tries left: " + triesLeft;
                        triesLeft -= 1;
                        curButton.BackgroundImage = null;
                    }
                }
                else
                {
                    secondPress = true;
                    prevImage = matrix[b1, b2];
                    curButton.BackgroundImage = Image.FromFile(matrix[b1, b2]);
                    curButton.Enabled = false;
                }
            }
            else
            {
                triesLabel.Text = "GAME OVER!";
            }
        }

        //restart game button
        private void restartGame(object sender, EventArgs e)
        {
            triesLeft = 2;
            secondPress = false;
            //enabling buttons
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    string bName = "b" + i.ToString() + j.ToString();
                    Button btn = this.Controls[bName] as Button;
                    btn.Enabled = true;
                }
            }
            showImages();
        }


        //show images on buttons for 10 seconds
        private void showImages()
        {
            counter = 10;
            timer1.Enabled = true;
            timer1.Start();
            //randomize
            Random rnd = new Random();
            string[] randImages = images.OrderBy(x => rnd.Next()).ToArray();

            //add images to buttons and add them to the matrix also for further checkup
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {

                    string bName = "b" + i.ToString() + j.ToString();

                    Button btn = this.Controls[bName] as Button;
                    matrix[i, j] = randImages[k];
                    btn.BackgroundImage = Image.FromFile(randImages[k]);
                    //disable buttons so no pressing when timer is not 0
                    btn.Enabled = false;
                    k++;
                }
            }
        }

        //hide images after timeout
        private void hideImages()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    string bName = "b" + i.ToString() + j.ToString();
                    Button btn = this.Controls[bName] as Button;
                    btn.BackgroundImage = null;
                    btn.Enabled = true;
                }
            }
        }

        //timer countdown
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = counter.ToString();
            counter--;
            if (counter == 0)
            {
                timer1.Stop();
                hideImages();
                timeLabel.Text = "";
            }

        }

    }
}
