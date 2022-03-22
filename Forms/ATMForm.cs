using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMSimulator.Classes;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ATMSimulator.Forms
{
    public partial class ATMForm : Form
    {
        private string _stage;
        private Account _account;
        private string _pinEntered;

        private Dictionary<string, Account> _accounts;

        private bool _useSemaphore;


        public ATMForm(Dictionary<string, Account> accountsDict, bool useSemaphoreInit)
        {
            InitializeComponent();
            setScreen("<html><body style='background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please click the card reader to enter your card:</p></div></body></html>");
            _stage = "waiting_on_card"; //variable to store what stage in the ATM process you are at.
            _accounts =  accountsDict;
            _useSemaphore = useSemaphoreInit;
        }

        private void setScreen(string html)
        {
            this.webBrowser1.DocumentText = html;
        }

        /**
        * Event handler for on card reader click
        **/
        private void btnCardReader_Click(object sender, EventArgs e)
        {
            if(_stage == "waiting_on_card")
            {
                string input = Interaction.InputBox("Please enter the account number of the card to insert...", "Insert card", "", this.Location.X + 200, this.Location.Y + 140);
                try
                {
                    int accountNumber = Int32.Parse(input);
                    Account a1;
                    if(_accounts.TryGetValue(input, out a1))
                    {
                        setScreen("<html><body style='background-color: #01b0f1; color: white;'>" + input + "</body></html>");
                        _stage = "waiting_on_pin";
                        _account = a1;
                        _pinEntered = "";
                    }
                    else
                    {
                        setScreen("<html><body style='background-color: #01b0f1; font-family: 'Courier New', Courier, monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>Please try again to enter a valid account number</p></div></body></html>");
                    }
                }
                catch
                {
                    setScreen("<html><body style='background-color: #01b0f1; font-family: 'Courier New', Courier, monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>Please try again to enter a valid account number</p></div></body></html>");
                }
            }
            return;
        }

        private void btnKeypad_Click(object sender, EventArgs e)
        {
            ValueButton button = sender as ValueButton;
            int keyValue = button.ButtonValue;
            
            if(_stage == "waiting_on_pin")
            {
                if (keyValue < 10) {
                    if (_pinEntered.Length < 4) {
                        _pinEntered += keyValue.ToString();
                    }
                }
                if(keyValue == 10)
                {
                    if(_pinEntered == _account.GetPin())
                    {
                        setScreen("main menu");
                        _stage = "main_menu";
                    }
                    else 
                    {
                        setScreen("Invalid pin");
                        _stage = "locked";

                        wait(5000);

                        setScreen("<html><body style='background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please click the card reader to enter your card:</p></div></body></html>");
                        _stage = "waiting_on_card";
                    }
                }
                if(keyValue == 11)
                {
                    setScreen("remember to take your card");
                    _stage = "locked";

                    wait(5000);

                    setScreen("<html><body style='background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please click the card reader to enter your card:</p></div></body></html>");
                    _stage = "waiting_on_card";
                }
            }
            return;
        }

        // https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void btnScreenLeft1_Click(object sender, EventArgs e)
        {
            if(_stage == "main_menu")
            {
                // View Balance
            }
        }

        private void btnScreenLeft2_Click(object sender, EventArgs e)
        {
            if (_stage == "main_menu")
            {
                // Deposit Cash
            }
        }

        private void btnScreenLeft3_Click(object sender, EventArgs e)
        {
            if (_stage == "main_menu")
            {
                // Cancel
                setScreen("remember to take your card");
                _stage = "locked";

                wait(5000);

                setScreen("<html><body style='background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please click the card reader to enter your card:</p></div></body></html>");
                _stage = "waiting_on_card";
            }
        }

        private void btnScreenRight1_Click(object sender, EventArgs e)
        {
            if (_stage == "main_menu")
            {
                // Withdraw Cash
            }
        }

        private void btnScreenRight2_Click(object sender, EventArgs e)
        {
            if (_stage == "main_menu")
            {
                // Change PIN
            }
        }

        private void btnScreenRight3_Click(object sender, EventArgs e)
        {

        }
    }
}
