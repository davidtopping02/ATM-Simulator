using System;
using System.Collections.Generic;
using ATMSimulator.Classes;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ATMSimulator.Forms
{
    public partial class ATMForm : Form
    {
        private string _stage;
        private string _accountNumber;
        private string _pinEntered;

        private Dictionary<string, Account> _accounts;

        private bool _useSemaphore;
        private int _transactionDelay;
        private string _machineIdentifier;


        public ATMForm(Dictionary<string, Account> accountsDict, bool useSemaphoreInit, int transactionDelay, string machineID)
        {
            InitializeComponent();
            setScreen("<html><body style='background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please click the card reader to enter your card:</p></div></body></html>");
            _stage = "waiting_on_card"; //variable to store what stage in the ATM process you are at.
            _accounts = accountsDict;
            _useSemaphore = useSemaphoreInit;
            _transactionDelay = transactionDelay * 1000;
            _machineIdentifier = machineID;
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
            if (_stage == "waiting_on_card")
            {
                string input = Interaction.InputBox("Please enter the account number of the card to insert...", "Insert card", "", this.Location.X + 200, this.Location.Y + 140);
                try
                {
                    int accountNumber = Int32.Parse(input);
                    Account a1;
                    if (_accounts.TryGetValue(input, out a1))
                    {
                        _accountNumber = input;
                        _pinEntered = "";

                        setStage("waiting_on_pin", new[] { _pinEntered });
                    }
                    else
                    {
                        // Account number doesnt exist
                        setStage("invalid_account");
                    }
                }
                catch
                {
                    // Account number is not numeric
                    setStage("invalid_account");
                }
            }
            return;
        }

        private void btnKeypad_Click(object sender, EventArgs e)
        {
            ValueButton button = sender as ValueButton;
            int keyValue = button.ButtonValue;

            if (_stage == "waiting_on_pin")
            {
                if (keyValue < 10)
                {
                    if (_pinEntered.Length < 4)
                    {
                        _pinEntered += keyValue.ToString();
                        setStage("waiting_on_pin", new[] { _pinEntered });
                    }
                }
                if (keyValue == 10)
                {
                    if (_accounts.TryGetValue(_accountNumber, out Account a))
                    {
                        if (_pinEntered == a.GetPin())
                        {
                            setStage("main_menu");
                        }
                        else
                        {
                            setStage("invalid_pin");
                        }
                    }
                }
                if (keyValue == 11)
                {
                    setStage("take_card");
                }
            }

            if (_stage == "change_pin")
            {
                if (keyValue < 10)
                {
                    if (_pinEntered.Length < 4)
                    {
                        _pinEntered += keyValue.ToString();
                        setStage("change_pin", new[] { _pinEntered });
                    }
                }
                if (keyValue == 10)
                {
                    if (_pinEntered.Length == 4)
                    {
                        if (_accounts.TryGetValue(_accountNumber, out Account a))
                        {
                            if (_useSemaphore)
                            {
                                a.UpdatePin(_pinEntered, _transactionDelay, _machineIdentifier);
                            }
                            else
                            {
                                a.UpdatePinUnsafe(_pinEntered, _transactionDelay, _machineIdentifier);
                            }
                            setStage("change_pin_success");
                        }
                    }
                    else
                    {
                        setStage("invalid_pin_change");
                    }
                }
                if (keyValue == 11)
                {
                    setStage("main_menu");
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
            if (_stage == "main_menu")
            {
                if (_accounts.TryGetValue(_accountNumber, out Account a))
                {
                    string account_balance = a.GetBalance().ToString();

                    setStage("view_balance", new[] { account_balance });
                }
            }

            if(_stage == "withdraw_cash")
            {
                // £5 withdrawal
            }
        }

        private void btnScreenLeft2_Click(object sender, EventArgs e)
        {
            if (_stage == "main_menu")
            {
                // Deposit Cash
            }

            if (_stage == "withdraw_cash")
            {
                // £10 withdrawal
            }
        }

        private void btnScreenLeft3_Click(object sender, EventArgs e)
        {
            if (_stage == "main_menu")
            {
                // Cancel
                setStage("take_card");
            }

            if (_stage == "withdraw_cash")
            {
                // £20 withdrawal
            }
        }

        private void btnScreenRight1_Click(object sender, EventArgs e)
        {
            if (_stage == "main_menu")
            {
                // Withdraw Cash
                setStage("withdraw_cash");
            }

            if (_stage == "withdraw_cash")
            {
                // £50 withdrawal
            }
        }

        private void btnScreenRight2_Click(object sender, EventArgs e)
        {
            if (_stage == "main_menu")
            {
                _pinEntered = "";
                setStage("change_pin", new[] { _pinEntered });
            }

            if (_stage == "withdraw_cash")
            {
                // £100 withdrawal
            }
        }

        private void btnScreenRight3_Click(object sender, EventArgs e)
        {
            if (_stage == "withdraw_cash")
            {
                // Other withdrawal amount
            }
        }

        public void setStage(string newStage, string[] args = null)
        {
            _stage = newStage;

            if (_stage == "waiting_on_card")
            {
                setScreen("<html><body style='background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Click the card reader to insert your card</p></div></body></html>");
                return;
            }

            if (_stage == "invalid_pin")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>The PIN entered is incorrect.</p></div></body></html>");
                wait(5000);
                setStage("waiting_on_card");
                return;
            }

            if (_stage == "invalid_account")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>The account number entered does not exist.</p></div></body></html>");
                wait(5000);
                setStage("waiting_on_card");
                return;
            }

            if (_stage == "waiting_on_pin")
            {
                int pinLength = args[0].Length;
                string pinObfuscated = "";
                for (int i = 0; i < 4; i++)
                {
                    if (i < pinLength)
                    {
                        pinObfuscated += "*";
                    }
                    else
                    {
                        pinObfuscated += "_";
                    }
                }
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please enter your pin in the keypad.</p><p>Confirm using GREEN and Cancel using RED.</p><p>" + pinObfuscated + "</p></div></body></html>");
                return;
            }

            if (_stage == "main_menu")
            {
                setScreen("main menu here");
                return;
            }

            if (_stage == "take_card")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: yellow; border-style: solid; text-align: center;'><p>Remember To Take Your Card</p></div></body></html>");
                wait(5000);
                setStage("waiting_on_card");
                return;
            }

            if (_stage == "take_cash_card")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: yellow; border-style: solid; text-align: center;'><p>Remember To Take Your Cash and Card</p></div></body></html>");
                wait(5000);
                setStage("waiting_on_card");
                return;
            }

            if (_stage == "change_pin")
            {
                int pinLength = args[0].Length;
                string pinObfuscated = "";
                for (int i = 0; i < 4; i++)
                {
                    if (i < pinLength)
                    {
                        pinObfuscated += "*";
                    }
                    else
                    {
                        pinObfuscated += "_";
                    }
                }
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please enter your new pin using the keypad.</p><p>Confirm using GREEN and Cancel using RED.</p><p>" + pinObfuscated + "</p></div></body></html>");
                return;
            }

            if (_stage == "invalid_pin_change")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder; padding-top: 50%; padding-bottom: 50%'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>The PIN must be exactly four numbers long.</p></div></body></html>");
                wait(5000);
                setStage("main_menu");
                return;
            }

            if (_stage == "change_pin_success")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: yellow; border-style: solid; text-align: center;'><p>Pin changed successfully.</p></div></body></html>");
                wait(5000);
                setStage("main_menu");
                return;
            }

            if (_stage == "view_balance")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: yellow; border-style: solid; text-align: center;'><p>Current Balance: £" + args[0] + "</p></div></body></html>");
                wait(5000);
                setStage("main_menu");
            }

            if (_stage == "withdraw_cash")
            {
                setScreen("Select denomination: 5, 10, 20, 50, 100, other");
                return;
            }
        }
    }
}
