using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATMSimulator.Forms;
using ATMSimulator.Classes;

namespace ATMSimulator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Account a1 = new Account("123456","1234",123,"Peter");
            Account a2 = new Account("234567", "1234", 123, "Peter");
            Account a3 = new Account("345678", "1234", 123, "Peter");

            Dictionary<string, Account> dict = new Dictionary<string, Account>();
            dict.Add(a1.GetNumber(), a1);
            dict.Add(a2.GetNumber(), a2);
            dict.Add(a3.GetNumber(), a3);

            Application.Run(new ATMForm(dict, true));
        }
    }
}
