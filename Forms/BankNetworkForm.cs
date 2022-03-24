using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ATMSimulator.Classes;
using Microsoft.VisualBasic;

namespace ATMSimulator.Forms
{
    public partial class BankNetworkForm : Form
    {
        //Creates atmCounter and new dictonary to contain all accounts
        private int _atmCounter;
        private Dictionary<string, Account> dict;

        public BankNetworkForm()
        {
            InitializeComponent();

            //Initialises accounts
            
            Account a1 = new Account("111111", "1111", 300, "Peter", this);
            Account a2 = new Account("222222", "2222", 750, "Pan", this);
            Account a3 = new Account("333333", "3333", 3000, "Wendy", this);
            Account a4 = new Account("444444", "4444", 30000, "Darling", this);
            Account a5 = new Account("555555", "5555", 0, "Captain", this);
            Account a6 = new Account("666666", "6666", 0, "Hook", this);
            Account a7 = new Account("777777", "7777", 2, "Lost", this);
            Account a8 = new Account("888888", "8888", 4, "Boys", this);

            //New instance of dictionary
            dict = new Dictionary<string, Account>();
            //Adds new accounts
            dict.Add(a1.GetNumber(), a1);
            dict.Add(a2.GetNumber(), a2);
            dict.Add(a3.GetNumber(), a3);
            dict.Add(a4.GetNumber(), a4);
            dict.Add(a5.GetNumber(), a5);
            dict.Add(a6.GetNumber(), a6);
            dict.Add(a7.GetNumber(), a7);
            dict.Add(a8.GetNumber(), a8);
            //atmCounter is set to 0
            _atmCounter = 0;
        }

        /**
        * On CreateATMButton click
        **/
        private void btnCreateATM_Click(object sender, EventArgs e)
        {
            //Atm counter is incremented
            _atmCounter++;
            bool semaphores = ckbSemaphores.Checked;

            //Try
            try
            {
                //Parses delay as integer
                int delay = Int32.Parse(txbDelay.Text);

                //Creates new thread
                Thread newATMThread = new Thread(create);
                newATMThread.SetApartmentState(ApartmentState.STA);
                newATMThread.IsBackground = true;
                newATMThread.Start();

                //Posts log 
                PostLogMessage("[NETWORK] ATM " + _atmCounter + " created with semaphores and " + delay + "s delay");

                //creates new ATM
                void create()
                {
                    ATMForm newATM = new ATMForm(dict, semaphores, delay, "ATM " + _atmCounter);
                    Application.Run(newATM);
                }
            }
            catch
            {
                //delay is not an int
                MessageBox.Show("Invalid delay", "Error");
                txbDelay.Text = "";
            }
        }

        /**
        * Enters a log
        **/
        public void PostLogMessage(string message)
        {
            if (txbLogWindow.InvokeRequired)
            { txbLogWindow.Invoke(new Action<String>(PostLogMessage), new object[] { message }); }
            else
            { txbLogWindow.Text = txbLogWindow.Text + message + Environment.NewLine; }
        }

        /**
        * Method to Create New Account
        **/
        private void btnAddCount_Click(object sender, EventArgs e)
        {

            //Try creating an account
            try
            {

                // Validates Account Number, if invalid, throws exception
                if ((txbAccountNumber.Text).Length != 6 || int.TryParse(txbAccountNumber.Text, out int accNum) != true)
                {
                    throw new ArgumentException("Account number is invalid.");
                }

                //If a bank account with that number already exists, throws exception
                else if (dict.ContainsKey(txbAccountNumber.Text)) {
                    throw new ArgumentException("An account with that number already exists.");
                }

                // Checks if name has any digits or is empty, if yes, throws exception
                else if (String.IsNullOrEmpty(txbName.Text))
                {
                    throw new ArgumentException("Name is invalid.");
                }

                // Checks if txbPin is four, empty, or invalid, if yes, throws exception
                else if (txbPin.Text.Length != 4 || int.TryParse(txbPin.Text, out int pin) != true)
                {
                    throw new ArgumentException("Pin number is invalid.");
                }

                // Checks if txtBalance is empty or invalid, if yes, throws exception
                else if (String.IsNullOrEmpty(txbBalance.Text) || double.TryParse(txbBalance.Text, out double bal) != true)
                {
                    throw new ArgumentException("Balance is invalid.");
                }

                //Else creates the account
                else
                {
                    Account a4 = new Account(txbAccountNumber.Text, txbPin.Text, double.Parse(txbBalance.Text), txbName.Text, this);
                    dict.Add(a4.GetNumber(), a4);
                }



            }

            //Catches the exception
            catch (ArgumentException ex)
            {
                //Displays error 
                MessageBox.Show(ex.Message, "Error");
                
            }
        }

        private void btnUnlockAccount_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Please enter the account number of the card to insert...", "Insert card", "", this.Location.X + 200, this.Location.Y + 140);
            if(int.TryParse(input, out int accountNumber))
            {
                if(dict.TryGetValue(input, out Account a1))
                {
                    if (a1.IsLocked())
                    {
                        a1.UnlockAccount();
                        //Displays error 
                        MessageBox.Show("The account is now unlocked.", "Account unlocked successfully");
                    }
                    else
                    {
                        //Displays error 
                        MessageBox.Show("The account is not locked.", "Already unlocked");
                    }
                }
                else
                {
                    //Displays error 
                    MessageBox.Show("The account specified does not exist.", "Could not find account");
                }
            }
            else
            {
                //Displays error 
                MessageBox.Show("Your account number cannot contain any letters or symbols.", "Account number must be numeric");
            }
        }

        private void btnViewAllAccounts_Click(object sender, EventArgs e)
        {
            //Creates new thread
            Thread newATMThread = new Thread(create);
            newATMThread.SetApartmentState(ApartmentState.STA);
            newATMThread.IsBackground = true;
            newATMThread.Start();

            //Posts log 
            PostLogMessage("[NETWORK] Central Computer accessed accounts database");

            //creates new ATM
            void create()
            {
                AccountsForm form = new AccountsForm(dict);
                Application.Run(form);
            }
        }
    }
}
