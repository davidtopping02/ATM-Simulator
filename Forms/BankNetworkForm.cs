using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ATMSimulator.Classes;


namespace ATMSimulator.Forms
{
    public partial class BankNetworkForm : Form
    {
        private int _atmCounter;
        private Dictionary<string, Account> dict;

        public BankNetworkForm()
        {
            InitializeComponent();
            Account a1 = new Account("123456", "1234", 123, "Peter", this);
            Account a2 = new Account("234567", "1234", 123, "Peter", this);
            Account a3 = new Account("345678", "1234", 123, "Peter", this);

            dict = new Dictionary<string, Account>();
            dict.Add(a1.GetNumber(), a1);
            dict.Add(a2.GetNumber(), a2);
            dict.Add(a3.GetNumber(), a3);
            _atmCounter = 0;
        }

        private void btnCreateATM_Click(object sender, EventArgs e)
        {
            _atmCounter++;
            bool semaphores = ckbSemaphores.Checked;

            try
            {
                int delay = Int32.Parse(txbDelay.Text);

                Thread newATMThread = new Thread(create);
                newATMThread.SetApartmentState(ApartmentState.STA);
                newATMThread.IsBackground = true;
                newATMThread.Start();

                PostLogMessage("[NETWORK] ATM " + _atmCounter + " created with semaphores and " + delay + "s delay");

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
        public void createNewAccount() {
            
            //Try creating an account
            // try {

                    //if (txbAccountNumber.Text)
            // //    
            //Account a4 = new Account(txbAccountNumber.Text, txbPin.Text, txbBalance.Text, txbName.Text, this);
            // }

            // catch {
                //delay is not an int
                // MessageBox.Show("Invalid delay", "Error");
                // txbDelay.Text = "";

                //check acc pin (is four) and double bal are numeric

            // }
        }
    }
}
