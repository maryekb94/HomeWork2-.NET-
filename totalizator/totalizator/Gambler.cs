using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace totalizator
{
    //игрок
    public class Gambler

    {
        public string name_player;
        public int number_player;
        public int money_player;

        // Имя игрока
        public string NamePlayer
        {
            get { return name_player; }
            private set { name_player = value; }
        }


        //номер игрока.
        public int NumberPlayer
        {
            get { return number_player + 1; }
            private set { number_player = value; }
        }


        // Количество днег у игрока
        public int MoneyPlayer
        {
            get { return money_player; }
            set { money_player = value; }
        }

        //конструктор игрока
        public Gambler(string name, int num, int curMoney)
        {
            NamePlayer = name;
            NumberPlayer = num;
            MoneyPlayer = curMoney;
        }

        //метод который делает ставку игрока
        public Bet stavka(int money,Bug bug)
        {
            MoneyPlayer -= money;
            return new Bet(this, money, bug);
        }

        //игрок забирает выигрыш
        public void GetPrize(int money)
        {
            MoneyPlayer += money;
        }
    }
}
