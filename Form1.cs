using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Face
{
    public partial class Form1 : Form
    {
        private bool capture_in_progress;
        private VideoCapture capture;
        CascadeClassifier Detece_v = new CascadeClassifier("haarcascade_frontalface_alt2.xml");

        public Form1()
        {
            InitializeComponent();

            //open came
            try
            {
                capture = new VideoCapture();
                //capture = new VideoCapture("http://192.168.2.49:81/snapshot.cgi?user=admin&pwd=123456789&");

            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(this, "Error", ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Image<Bgr, byte> nextFrame = capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                if (nextFrame != null && capture_in_progress == true)
                {
                    Rectangle[] Head = Detece_v.DetectMultiScale(nextFrame, 1.4, 3, new Size(nextFrame.Width / 8, nextFrame.Width / 8));
                    foreach (Rectangle face in Head)
                    {

                        nextFrame.Draw(face, new Bgr(Color.BlanchedAlmond), 3);

                    }
                    imageBox1.Image = nextFrame;
                 //   pictureBox1.Image = nextFrame.ToBitmap();

                }

            }
        }

      
        private void fIleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //start
            if (capture_in_progress)
            {
                capture.Pause();
                fIleToolStripMenuItem.MergeIndex = 1;
                fIleToolStripMenuItem.Text = "Start Capturing";
                capture_in_progress = false;

            }
            else
            {
                //stop
                fIleToolStripMenuItem.Text = "Stop Capturing";
                capture_in_progress = true;
            }
        }
    }
}
