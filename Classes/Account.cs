﻿using System;
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


        /**
        * Withdraws the amount using semaphores
        */
        public void WithdrawAmount(double amount, int delay, string atm)
        {
            if (amount == 0)
            {
                throw new InvalidOperationException("Withdrawal amount cannot be zero.");
            }
            else if(this._balance < amount) 
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            else
            {
                if (_balanceSemaphore.WaitOne(15000))
                {
                    double tempBalance = _balance;
                    tempBalance -= amount;
                    wait(delay);
                    _balance = tempBalance;
                    _balanceSemaphore.Release(1);
                }
                else
                {
                    // Handle timeout here
                    throw new TimeoutException("Withdrawal timed out. The money has not been taking out of the account.");
                }
            }
        }

        public void WithdrawAmountUnsafe(double amount, int delay, string atm) 
        {
            if (amount == 0)
            {
                throw new InvalidOperationException("Withdrawal amount cannot be zero.");
            }
            else if(this._balance < amount){
                throw new InvalidOperationException("Insufficient funds");
            }
            else
            {
                double tempBalance = _balance;
                tempBalance -= amount;
                wait(delay);
                _balance = tempBalance;
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
                if (_balanceSemaphore.WaitOne(15000))
                {
                    // simulating data race
                    double tempBalance = _balance;
                    tempBalance += amount;
                    wait(delay);
                    _balance = tempBalance;
                    _balanceSemaphore.Release(1);
                }
                else
                {
                    // Handle timeout here
                    throw new TimeoutException("Deposit timed out. The money has not been deposited into the account.");
                }
            }
        }

        public void DepositAmountUnsafe(double amount, int delay, string atm)
        {
            if (amount == 0)
            {
                throw new InvalidOperationException("Deposit amount cannot be zero.");
            }
            else
            {
                double tempBalance = _balance;
                tempBalance += amount;
                wait(delay);
                _balance = tempBalance;
            }
        }

        /*
         * Updates the pin number for a bank account (semaphore)
         */
        public void UpdatePin(string newPin, int delay, string atm)
        {
            if (newPin == _pin)
            {
                throw new InvalidOperationException("New PIN must be different from old PIN.");
            }
            else
            {
                _parent.PostLogMessage("[" + atm + "] Waiting for account PIN access...");
                if (_pinSemaphore.WaitOne(15000))
                {
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE AQUIRED...");
                    string tempPin = _pin;
                    tempPin = newPin;
                    _parent.PostLogMessage("[" + atm + "] Current account PIN loaded...");
                    _parent.PostLogMessage(atm + " PIN transaction started...");
                    wait(delay);
                    _pin = tempPin;
                    _parent.PostLogMessage("[" + atm + "] PIN transaction completed...");
                    _pinSemaphore.Release(1);
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE RELEASED...");
                }
                else
                {
                    // Handle timeout here
                    _parent.PostLogMessage("[" + atm + "] SEMAPHORE REQUEST TIMEOUT...");
                    throw new TimeoutException("PIN update timed out. The PIN has not been changed.");
                }
            }

        }

        /*
         * Updates the pin for the bank account in an unsafe way
         * (not using semaphores)
         */
        public void UpdatePinUnsafe(string newPin, int delay, string atm)
        {
            //If this.pin is not equal to the new pin then
            //this.pin is set to the new pin and true is returned
            if (_pin != newPin)
            {
                string tempPin = _pin;
                tempPin = newPin;
                wait(delay);
                _pin = tempPin;
            }

            //else false is returned
            else
            {
                throw new InvalidOperationException("New PIN must be different from old PIN.");
            }
        }

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
    }
}
