using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace totalizator
{
    //ставка
    public class Bet
    {
        
        public Gambler gambler;//игрок сделавший ставку
        public int bet;//ставка
        private Bug bug;//участник на которого сделана ставка

        
        //игрк сделавший ставку
        public Gambler PlayGambler
        {
            get { return gambler; }
            set { gambler = value; }
        }

        //размер ставки
        public int Money
        {
            get { return bet; }
            set { bet = value; }
        }

        //таракан на которого сделана ставка
        public Bug Roach
        {
            get { return bug; }
            set { bug = value; }
        }


        //конструктор ставки
        public Bet(Gambler player,int money,Bug runner)
        {
            PlayGambler = player;
            Money = money;
            Roach = runner;
            
        }

        // Был написан здесь, чтобы убрать предупреждение.
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
