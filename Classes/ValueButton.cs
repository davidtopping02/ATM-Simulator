using System;
using System.Windows.Forms;

namespace ATMSimulator.Classes
{
    class ValueButton : Button
    {
        public int buttonValue;

        public int ButtonValue
        {
            get { return buttonValue; }
            set { buttonValue = value; }
        }
    }
}
