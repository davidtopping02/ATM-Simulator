using System;
using System.Collections.Generic;
using ATMSimulator.Classes;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ATMSimulator.Forms
{

    /*
     * Form class to simulate an ATM
     */
    public partial class ATMForm : Form
    {

        private string _stage;
        private string _accountNumber;
        private string _pinEntered;
        private string _amountEntered;

        private Dictionary<string, Account> _accounts;

        private bool _useSemaphore;
        private int _transactionDelay;
        private string _machineIdentifier;

        private int _wrongPinCounter;
        private string _attemptedAccount;

        /*
         * Overloaded constructor for field values
         */
        public ATMForm(Dictionary<string, Account> accountsDict, bool useSemaphoreInit, int transactionDelay, string machineID)
        {
            InitializeComponent();
            setScreen("<html><body style='background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem; background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please click the card reader to enter your card:</p></div></body></html>");
            _stage = "waiting_on_card"; //variable to store what stage in the ATM process you are at.
            _accounts = accountsDict;
            _useSemaphore = useSemaphoreInit;
            _transactionDelay = transactionDelay * 1000;
            _machineIdentifier = machineID;

            _wrongPinCounter = 0;
            _attemptedAccount = "";

            this.Text = machineID;
        }

        /*
         *  Sets the screen to the HTML string
         */
        private void setScreen(string html)
        {
            this.webBrowser1.DocumentText = html;
        }

        /*
         * Event handler for on card reader click
         */
        private void btnCardReader_Click(object sender, EventArgs e)
        {

            //sound effect
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atm_beep);
            player.Play();

            if (_stage == "waiting_on_card")
            {
                string input = Interaction.InputBox("Please enter the account number of the card to insert...", "Insert card", "", this.Location.X + 200, this.Location.Y + 140);

                //verifying the input is an int
                try
                {
                    int accountNumber = Int32.Parse(input);
                    Account a1;

                    // verifies the account no. exists
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

        /*
         * Gives functionallity (event handlers) to the ATM keypad
         */
        private void btnKeypad_Click(object sender, EventArgs e)
        {
            //sound effect
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atm_beep);
            player.Play();

            ValueButton button = sender as ValueButton;
            int keyValue = button.ButtonValue;

            // initial waiting for pin to be entered
            if (_stage == "waiting_on_pin")
            {

                // functionallity for keypad buttons (0-9)
                if (keyValue < 10)
                {
                    // waiting for full pin to be entered...
                    if (_pinEntered.Length < 4)
                    {
                        _pinEntered += keyValue.ToString();
                        setStage("waiting_on_pin", new[] { _pinEntered });
                    }
                }

                // functionallity for enter (green) button
                if (keyValue == 10)
                {
                    // checking account exists using account no.
                    if (_accounts.TryGetValue(_accountNumber, out Account a))
                    {
                        if (_attemptedAccount != a.GetNumber())
                        {
                            _attemptedAccount = a.GetNumber();
                            _wrongPinCounter = 0;
                        }
                        if (_pinEntered == a.GetPin())
                        {
                            if (a.IsLocked())
                            {
                                // Show locked screen
                                setStage("account_locked");
                                return;
                            }
                            else
                            {
                                _wrongPinCounter = 0;
                                setStage("main_menu");
                                return;
                            }
                        }
                        else
                        {
                            if (a.IsLocked())
                            {
                                // Show locked screen
                                setStage("account_locked");
                                return;
                            }
                            _wrongPinCounter++;
                            if (_wrongPinCounter >= 3)
                            {
                                a.LockAccount();
                                setStage("lock_account");
                                return;
                            }
                            setStage("invalid_pin");
                        }
                    }
                }
                if (keyValue == 11)
                {
                    setStage("take_card");
                }
            }

            if (_stage == "custom_amount")
            {
                if (keyValue < 10)
                {
                    _amountEntered += keyValue.ToString();
                    setStage("custom_amount", new[] { _amountEntered });
                }
                if (keyValue == 10)
                {
                    // Confirm
                    if (int.TryParse(_amountEntered, out int t))
                    {
                        HandleWithdrawal(t);
                    }
                }
                if (keyValue == 11)
                {
                    // Cancel
                    setStage("main_menu");
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
                            try
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
                            catch (InvalidOperationException)
                            {
                                setStage("invalid_pin_change");
                            }
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

        /*
         *  Functionality for button around the ATM screen
         */
        private void btnScreenLeft1_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atm_beep);
            player.Play();

            if (_stage == "main_menu")
            {
                if (_accounts.TryGetValue(_accountNumber, out Account a))
                {
                    string account_balance = a.GetBalance().ToString();

                    setStage("view_balance", new[] { account_balance });
                }
                return;
            }

            if (_stage == "withdraw_cash")
            {
                // £5 withdrawal
                HandleWithdrawal(5);
                return;
            }
        }


        /*
         *  Functionality for button around the ATM screen
         */
        private void btnScreenLeft2_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atm_beep);
            player.Play();

            if (_stage == "main_menu")
            {
                // Deposit Cash
                string input = Interaction.InputBox("Please enter the amount to deposit...", "Insert cash", "", this.Location.X + 200, this.Location.Y + 140);
                if (int.TryParse(input, out int value))
                {
                    HandleDeposit(value);
                }
                else
                {
                    setStage("amount_not_zero");
                }
                return;
            }

            if (_stage == "withdraw_cash")
            {
                // £10 withdrawal
                HandleWithdrawal(10);
                return;
            }
        }

        /*
         *  Functionality for button around the ATM screen
         */
        private void btnScreenLeft3_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atm_beep);
            player.Play();

            if (_stage == "main_menu")
            {
                // Cancel
                setStage("take_card");
                return;
            }

            if (_stage == "withdraw_cash")
            {
                // £20 withdrawal
                HandleWithdrawal(20);
                return;
            }
        }

        /*
         *  Functionality for button around the ATM screen
         */
        private void btnScreenRight1_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atm_beep);
            player.Play();

            if (_stage == "main_menu")
            {
                // Withdraw Cash
                setStage("withdraw_cash");
                return;
            }

            else if (_stage == "withdraw_cash")
            {
                // £50 withdrawal
                HandleWithdrawal(50);
                return;
            }
        }

        /*
         *  Functionality for button around the ATM screen
         */
        private void btnScreenRight2_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atm_beep);
            player.Play();

            if (_stage == "main_menu")
            {
                _pinEntered = "";
                setStage("change_pin", new[] { _pinEntered });
                return;
            }

            if (_stage == "withdraw_cash")
            {
                // £100 withdrawal
                HandleWithdrawal(100);
                return;
            }
        }

        /*
         *  Functionality for button around the ATM screen
         */
        private void btnScreenRight3_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atm_beep);
            player.Play();

            if (_stage == "withdraw_cash")
            {
                // Other withdrawal amount
                _amountEntered = "";
                setStage("custom_amount", new[] { _amountEntered });
            }
        }


        /*
         * Used to change the screen text/formatting based on the current state (stage)
         */
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

            if (_stage == "account_locked")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>This account is locked.</p></div></body></html>");
                wait(5000);
                setStage("waiting_on_card");
                return;
            }

            if (_stage == "lock_account")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>You entered an incorrect PIN three times. Your account will now be locked.</p></div></body></html>");
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
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please enter your pin on the keypad.</p><p>Confirm using GREEN and Cancel using RED.</p><p>" + pinObfuscated + "</p></div></body></html>");
                return;
            }

            if (_stage == "custom_amount")
            {
                string amount = args[0];
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: black; border-style: solid; text-align: center;'><p>Please enter the amount you want to withdraw on the keypad.</p><p>Confirm using GREEN and Cancel using RED.</p><p>£" + amount + "</p></div></body></html>");
                return;
            }

            if (_stage == "main_menu")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: 'Courier New', Courier, monospace; color: white; font-weight: bolder; padding-top: 50%; padding-bottom: 50%;'><div style= 'height: 50%;'></div><div style='background-color: #01b0f1; color:black; text-align: center; position: relative; width: 100%'><div><div style='border-radius: 0.25rem;background-color: #ffffff; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: left; padding-bottom: 5px; margin-bottom: 2px'>< View Balance</div><div style='border-radius: 0.25rem;background-color: white; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: right;padding-bottom: 5px; margin-bottom: 2px'>Withdraw Cash > </div></div><div><div style='border-radius: 0.25rem;background-color: white; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: left; padding-bottom: 5px; margin-bottom: 2px; margin-top: 4px;'>< Deposit Cash</div><div style='border-radius: 0.25rem;background-color: white; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: right; padding-bottom: 5px; margin-bottom: 2px; margin-top: 4px;'>Change Pin ></div></div><div><div style='border-radius: 0.25rem;background-color: white; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: left; margin-bottom: 2px; margin-top: 4px'>< Cancel</div></div></div></body></html>");
                return;
            }

            if (_stage == "take_card")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: yellow; border-style: solid; text-align: center;'><p>Remember To Take Your Card</p></div></body></html>");
                wait(5000);
                setStage("waiting_on_card");
                return;
            }

            if (_stage == "transaction_pending")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: yellow; border-style: solid; text-align: center;'><p>Transaction pending...</p></div></body></html>");
                return;
            }

            if (_stage == "take_cash_card")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: yellow; border-style: solid; text-align: center;'><p>Remember To Take Your Cash and Card</p></div></body></html>");

                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.cash_dispense);
                player.Play();

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
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder; padding-top: 50%; padding-bottom: 50%'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>The PIN must be exactly four numbers long and different from the previous one.</p></div></body></html>");
                wait(5000);
                setStage("main_menu");
                return;
            }

            if (_stage == "insufficient_funds")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>You do not have enough money in your account.</p></div></body></html>");
                wait(5000);
                setStage("main_menu");
                return;
            }

            if (_stage == "amount_not_zero")
            {
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: monospace; color: white; font-weight: bolder;'><div style='border-radius: 0.25rem;background-color: white; color:black; border-color: red; border-style: solid; text-align: center;'><p>You must enter an amount more than £0.</p></div></body></html>");
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
                setScreen("<html><body style='height: 200px; background-color: #01b0f1; font-family: 'Courier New', Courier, monospace; color: white; font-weight: bolder; padding-top: 50%; padding-bottom: 50%;'><div style= 'height: 50%;'></div><div style='background-color: #01b0f1; color:black; text-align: center; position: relative; width: 100%'><div><div style='border-radius: 0.25rem;background-color: #ffffff; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: left; padding-bottom: 5px; margin-bottom: 2px'>< £5</div><div style='border-radius: 0.25rem;background-color: white; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: right;padding-bottom: 5px; margin-bottom: 2px'>£50 > </div></div><div><div style='border-radius: 0.25rem;background-color: white; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: left; padding-bottom: 5px; margin-bottom: 2px; margin-top: 4px;'>< £20</div><div style='border-radius: 0.25rem;background-color: white; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: right; padding-bottom: 5px; margin-bottom: 2px; margin-top: 4px;'>£100 ></div><div><div style='border-radius: 0.25rem;background-color: #ffffff; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: left; padding-bottom: 5px; margin-bottom: 2px'>< £20</div><div style='border-radius: 0.25rem;background-color: white; color:black; border-color:black; border-style: solid; text-align: center; width: 45%; float: right;padding-bottom: 5px; margin-bottom: 2px'>Other > </div></div></div></body></html>");
                return;
            }
        }

        /*
         * Withdraw cash using ATM
         */
        private void HandleWithdrawal(int amount)
        {
            setStage("transaction_pending");
            try
            {
                if (_accounts.TryGetValue(_accountNumber, out Account a))
                {
                    if (_useSemaphore == true)
                    {
                        a.WithdrawAmount(amount, _transactionDelay, _machineIdentifier);
                    }
                    else
                    {
                        a.WithdrawAmountUnsafe(amount, _transactionDelay, _machineIdentifier);
                    }
                    setStage("take_cash_card");
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Insufficient funds")
                {
                    setStage("insufficient_funds");
                }
                else if (ex.Message == "Withdrawal amount cannot be zero.")
                {
                    setStage("amount_not_zero");
                }
            }
        }

        private void HandleDeposit(int amount)
        {
            setStage("transaction_pending");
            try
            {
                if (_accounts.TryGetValue(_accountNumber, out Account a))
                {
                    if (_useSemaphore == true)
                    {
                        a.DepositAmount(amount, _transactionDelay, _machineIdentifier);
                    }
                    else
                    {
                        a.DepositAmountUnsafe(amount, _transactionDelay, _machineIdentifier);
                    }
                    setStage("take_card");
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Deposit amount cannot be zero.")
                {
                    setStage("amount_not_zero");
                }
            }
        }
    }
}
