using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace totalizator
{

    public class GameController
    {
        private Random rand;
        public List<Bug> bug;//список участников
        public List<Gambler> gambler;//список игроков
        public List<Bug> betterBug;//список победителей 
        public List<Bet> bets;//список ставок

        private Pen p;//ручка для отрисовки дорожек
        private Size size;//размер дорожки, определяющий положение таракана на картинке


        //список тараканов
        public List<Bug> Bugs
        {
            get { return bug; }
            private set { bug = value; }
        }

        //тараканы победители
        public List<Bug> Better
        {
            get { return betterBug; }
            private set { betterBug = value; }
        }

        //список игроков
        public List<Gambler> Gamblers
        {
            get { return gambler; }
            private set { gambler = value; }
        }

        //список ставок
        public List<Bet> Bets
        {
            get { return bets; }
            private set { bets = value; }
        }

        //конструктор
        public GameController(Size cursize)
        {
            Bets = new List<Bet>();
            Bugs = new List<Bug>();
            Better = new List<Bug>();
            Gamblers = new List<Gambler>();
            rand = new Random();
            p = new Pen(Brushes.Red,2);
            size = cursize;
            

            for (int i = 0; i < 4; i++)
            {
                Bugs.Add(new Bug("Участник" + (i + 1), i, totalizator.Properties.Resources.roachPicture, new Point(0, 1)));
            }

            for (int i = 0; i < 3; i++)
            {
                Gamblers.Add(new Gambler("Игрок" + (i+1),i,rand.Next(0,1000)));
            }

        }


        //создаем ставку (параметры: номер поставившего игрока,размер ставки,номер таракана)
        public void CreateBet(int numPlayer, int money, int numRoach,out bool Problem)
        {
            try
            {

                var curBet = new Bet(Gamblers[numPlayer], money, Bugs[numRoach]);

                if (Gamblers[numPlayer].MoneyPlayer < money)
                {
                    throw new MyException();
                    
                }

                Gamblers[numPlayer].stavka(money, Bugs[numRoach]);
                Bets.Add(curBet);
                Problem = false;
            }
            catch(MyException ex)
            {
                ex.ShowMessage();
                Problem = true;
            }
        }

        //двигаем тараканов по дорожкам
        public void MoveTo()
        {
            foreach (var v in Bugs)
            {
                v.Move(rand);
                if (v.Position.X >= size.Width)
                {
                    Better.Add(v);
                }
            }
            if (Better.Count > 0)
            {
                GetPrize();
            }
            
        }

        //собираем информацию о проигравших и выигравших
        public List<string> GetTextWinBets()
        {
            var text = new List<string>();
            foreach (var bet in Bets)
            {
                if (Better.Contains(bet.Roach))
                {
                    text.Add(bet.PlayGambler.NamePlayer + " выиграл  — " + bet.Money);
                }
                else
                {
                    text.Add(bet.PlayGambler.NamePlayer + " проиграл");
                }
            }
            return text;
        }

        //раздаем приз
        private void GetPrize()
        {
            foreach (var bug in Better)
            {
                foreach (var bet in Bets)
                {
                    if (bet.Roach == bug)
                    {
                        bet.PlayGambler.GetPrize(bet.Money * 10);
                    }
                }
            }
        }

        //очищаем данные для нового забега
        public void ClearResult()
        {
            Better.Clear();
            Bets.Clear();
            Bugs.ForEach(i => i.Position = new Point(0, i.Position.Y));
        }

        //отрисовываем один шаг игры
        public void Draw(List<Graphics> g)
        {
            for (int i = 0; i < Bugs.Count; i++)
            {
                DrawScene(g[i], i);
            }
        }

        //функция для отрисовки сцены
        public void DrawScene(Graphics g, int i)
        {
            g.DrawLine(p, new Point(0, size.Height), new Point(size.Width, size.Height));
            Bugs[i].DrawRoach(g, new Size(size.Height - 2, size.Height - 2));
        }

    }
}
