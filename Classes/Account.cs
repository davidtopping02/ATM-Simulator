using System;
using System.Threading;
using ATMSimulator.Forms;
using System.Windows.Forms;

namespace ATMSimulator.Classes
{
    /*
     * Holds all the data and functionallity for a user's bank account 
     */
    public class Account
    {
        private string _name;                   // name of the account holder
        private string _number;                 // the number of the account
        private string _pin;                    // the pin of the account
        private double _balance;               // the balance of the account

        private bool locked;                 // ifaccount is accessible

        private Semaphore _balanceSemaphore;    // the semaphore for balance
        private Semaphore _pinSemaphore;        // the semaphore for pin

        private BankNetworkForm _parent;

        /*
         * Overloaded constructor to set the new account details
         */
        public Account(string newNumber, string newPin, double newBalance, string newName, BankNetworkForm parent)
        {
            _number = newNumber;
            _pin = newPin;
            _balance = newBalance;
            _name = newName;
            _balanceSemaphore = new Semaphore(1, 1);
            _pinSemaphore = new Semaphore(1, 1);

            _parent = parent;

            parent.PostLogMessage("[ACCOUNT] New account " + _number + " created.");
        }

        //_____Getters_____

        public string GetName()
        {
            return _name;
        }

        public string GetNumber()
        {
            return _number;
        }

        public string GetPin()
        {
            return _pin;
        }

        public double GetBalance()
        {
            return _balance;
        }


        /*
         * Withdraws the amount using semaphores
         */
        public void WithdrawAmount(double amount, int delay, string atm)
        {
            // error handling for invalid amounts
            if (amount == 0)
            {
                throw new InvalidOperationException("Withdrawal amount cannot be zero.");
            }
            else if (this._balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            else
            {
                //withdraw cash...
                _parent.PostLogMessage("[" + atm + "] Waiting for Balance access...");
                if (_balanceSemaphore.WaitOne(15000))
                {
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE ACQUIRED...");
                    double tempBalance = _balance;
                    tempBalance -= amount;
                    _parent.PostLogMessage("[" + atm + "] Current balance loaded...");

                    _parent.PostLogMessage("[" + atm + "] Balance transaction started...");
                    wait(delay);
                    _balance = tempBalance;
                    _parent.PostLogMessage("[" + atm + "] Balance transaction completed...");
                    _balanceSemaphore.Release(1);
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE RELEASED...");
                    _parent.PostLogMessage("[" + atm + "] £" + amount + " withdrawn from account: " + _number);
                }
                else
                {
                    // Handle timeout here
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE REQUEST TIMEOUT...");
                    throw new TimeoutException("Withdrawal timed out. The money has not been taking out of the account.");
                }
            }
        }

        /*
         * Withdraws cash from an account (no semaphore)
         */
        public void WithdrawAmountUnsafe(double amount, int delay, string atm)
        {
            // error handling for invalid amounts
            if (amount == 0)
            {
                throw new InvalidOperationException("Withdrawal amount cannot be zero.");
            }
            else if (this._balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            else
            {
                // withdraw cash...
                _parent.PostLogMessage("[" + atm + "] Unsafe Balance access...");
                double tempBalance = _balance;
                tempBalance -= amount;
                _parent.PostLogMessage("[" + atm + "] Balance transaction started...");
                wait(delay);
                _balance = tempBalance;
                _parent.PostLogMessage("[" + atm + "] Balance transaction complete");
                _parent.PostLogMessage("[" + atm + "] £" + amount + " withdrawn from account: " + _number);
            }
        }

        /*
        * Deposits the amount using semaphores
        */
        public void DepositAmount(double amount, int delay, string atm)
        {
            if (amount == 0)
            {
                throw new InvalidOperationException("Deposit amount cannot be zero.");
            }
            else
            {
                _parent.PostLogMessage("[" + atm + "] Waiting for Balance access...");

                // wait for semaphore to becomee available 
                if (_balanceSemaphore.WaitOne(15000))
                {
                    // deposit cash...
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE ACQUIRED...");
                    double tempBalance = _balance;
                    tempBalance += amount;
                    _parent.PostLogMessage("[" + atm + "] Current balance loaded...");
                    _parent.PostLogMessage("[" + atm + "] Balance transaction started...");
                    wait(delay);
                    _balance = tempBalance;
                    _parent.PostLogMessage("[" + atm + "] Balance transaction completed...");
                    _balanceSemaphore.Release(1);
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE RELEASED...");
                    _parent.PostLogMessage("[" + atm + "] £" + amount + " deposited into account: " + _number);
                }
                else
                {
                    // Handle timeout here
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE REQUEST TIMEOUT...");
                    throw new TimeoutException("[" + atm + "] Deposit timed out. The money has not been deposited into the account.");
                }
            }
        }

        /*
         * Deposit amount (no semaphores)
         */
        public void DepositAmountUnsafe(double amount, int delay, string atm)
        {
            if (amount == 0)
            {
                throw new InvalidOperationException("Deposit amount cannot be zero.");
            }
            else
            {
                // deposit cash...
                _parent.PostLogMessage("[" + atm + "] Unsafe Balance access...");
                double tempBalance = _balance;
                tempBalance += amount;
                _parent.PostLogMessage("[" + atm + "] Balance transaction started...");
                wait(delay);
                _balance = tempBalance;
                _parent.PostLogMessage("[" + atm + "] Balance transaction complete");

                _parent.PostLogMessage("[" + atm + "] £" + amount + " deposited into account: " + _number);
            }
        }

        /*
         * Updates the pin number for a bank account (semaphore)
         */
        public void UpdatePin(string newPin, int delay, string atm)
        {
            // error handling
            if (newPin == _pin)
            {
                throw new InvalidOperationException("New PIN must be different from old PIN.");
            }
            else
            {

                // change pin
                _parent.PostLogMessage("[" + atm + "] Waiting for account PIN access...");
                if (_pinSemaphore.WaitOne(15000))
                {
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE AQUIRED...");
                    string tempPin = _pin;
                    tempPin = newPin;
                    _parent.PostLogMessage("[" + atm + "] Current account PIN loaded...");
                    _parent.PostLogMessage("[" + atm + "] PIN transaction started...");
                    wait(delay);
                    _pin = tempPin;
                    _parent.PostLogMessage("[" + atm + "] PIN transaction completed...");
                    _pinSemaphore.Release(1);
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE RELEASED...");
                    _parent.PostLogMessage("[" + atm + "] " + _number + " pin changed to: " + newPin);
                }
                else
                {
                    // Handle timeout here
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE REQUEST TIMEOUT...");
                    throw new TimeoutException("[" + atm + "] PIN update timed out. The PIN has not been changed.");
                }
            }
        }


        /*
         * Updates the pin for the bank account (no semaphores)
         */
        public void UpdatePinUnsafe(string newPin, int delay, string atm)
        {
            //If this.pin is not equal to the new pin then
            //this.pin is set to the new pin and true is returned
            if (_pin != newPin)
            {
                _parent.PostLogMessage("[" + atm + "] Unsafe PIN access...");
                string tempPin = _pin;
                tempPin = newPin;
                _parent.PostLogMessage("[" + atm + "] PIN transaction started...");
                wait(delay);
                _parent.PostLogMessage("[" + atm + "] PIN transaction completed...");
                _pin = tempPin;
                _parent.PostLogMessage("[" + atm + "] " + _number + " pin changed to: " + newPin);
            }

            //else false is returned
            else
            {
                throw new InvalidOperationException("New PIN must be different from old PIN.");
            }
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

        //_____Helper functions for locked account_____

        public Boolean IsLocked()
        {
            return locked;
        }

        public void LockAccount()
        {
            locked = true;
        }

        public void UnlockAccount()
        {
            locked = false;
        }
    }
}
