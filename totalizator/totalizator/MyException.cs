using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace totalizator
{
    [Serializable]
    class MyException:Exception
    {
        public MyException() : base("Игроку не хватает денег на ставку.") { }
		public MyException(string message) : base(message) { }
		public void ShowMessage()
		{
			MessageBox.Show(this.Message);
		}
    }
}
