using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace totalizator
{
    public partial class Form1 : Form
    {
        //используем буферизованную графику для отрисовки
        private GameController g;
        private List<Graphics> graphics;
        private BufferedGraphicsContext bufferedGraphicsContext;
        private List<BufferedGraphics> bufferedGraphics;

        public Form1()
        {
            InitializeComponent();
            g = new GameController(pictureBox1.Size);
            graphics = new List<Graphics>();
            bufferedGraphics = new List<BufferedGraphics>();
            bufferedGraphicsContext = new BufferedGraphicsContext();

            //сформировали список компонентов pictureBox
            var BoxList = panel1.Controls.OfType<PictureBox>().ToList();
            foreach (var pictBox in BoxList)
            {
                graphics.Add(pictBox.CreateGraphics());
            }

            for (int i = 0; i < graphics.Count; i++)
            {
                bufferedGraphics.Add(bufferedGraphicsContext.Allocate(graphics[i], new Rectangle(0, 0, panel1.Width, panel1.Height)));
                graphics[i] = bufferedGraphics[i].Graphics;
            }
            
        }


        
        
        //делаем ставки
        private void stavka_Click(object sender, EventArgs e)
        {
            var trueBug = groupBox2.Controls.OfType<RadioButton>().ToList().FindIndex(q => q.Checked);
            var trueGambler = groupBox1.Controls.OfType<RadioButton>().ToList().FindIndex(q => q.Checked);

            if (trueBug >= 0 && trueGambler >= 0)
            {
                try
                {
                    bool Problem;
                    var money = int.Parse(textBox1.Text);
                    g.CreateBet(trueGambler, money, trueBug,out Problem);
                    if (!Problem)
                    {
                        currentStavki.Items.Add(g.Gamblers[trueGambler].NamePlayer + " на " + g.Bugs[trueBug].Name + " - " + money);
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
            }
        }



        //начинаем гонку
        private void start_Click(object sender, EventArgs e)
        {
            if (g.Better.Count == 0)
            {
                timer1.Interval = 1;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("гонка закончилась");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            g.MoveTo();
            DrawMain();
            if (g.Better.Count > 0)
            {
                InformOfBetter();
            }

        }

        //вывод информации о победителе
        private void InformOfBetter()
        {
            timer1.Stop();
            string outStr = "";
            foreach (var better in g.Better)
            {
                outStr += better.Name + '\n';
            }
            var winGamblers = new List<string>();
            var text = g.GetTextWinBets();
            text.ForEach(str => result.Items.Add(str));
            result.Items.Add("победитель: " + outStr);

            
        }



        private void DrawMain()
        {
            bufferedGraphics.ForEach(gr => gr.Graphics.Clear(panel1.BackColor));
            g.Draw(graphics);
            bufferedGraphics.ForEach(gr => gr.Render());
        }

        
        private void Form1_Load_1(object sender, EventArgs e)
        {
            var pictBoxes = panel1.Controls.OfType<PictureBox>().ToList();
            for (int i = 0; i < pictBoxes.Count; i++)
            {
                pictBoxes[i].Image = new Bitmap(panel1.Width, panel1.Height);
                using (Graphics graphics_ = Graphics.FromImage(pictBoxes[i].Image))
                {
                    g.DrawScene(graphics_, i);
                }
            }
        }


        //новый забег
        private void button1_Click(object sender, EventArgs e)
        {
            g.ClearResult();
            currentStavki.Items.Clear();
            result.Items.Clear();
            
        }

        


        
        
        

        
       
    }
}
