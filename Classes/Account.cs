using System;
using System.Threading;

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
        private double _balance;                // the balance of the account

        private Boolean locked;                 // ifaccount is accessible

        private Semaphore _balanceSemaphore;    // the semaphore for balance
        private Semaphore _pinSemaphore;        // the semaphore for pin


        /*
         * Overloaded constructor to set the new account details
         */
        public Account(string newNumber, string newPin, double newBalance, string newName)
        {
            _number = newNumber;
            _pin = newPin;
            _balance = newBalance;
            _name = newName;
            _balanceSemaphore = new Semaphore(1, 1);
            _pinSemaphore = new Semaphore(1, 1);

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
        * Withdraws the amount using semiphores
        */
        public void WithdrawAmount(double amount)
        {
            if (amount == 0)
            {
                throw new InvalidOperationException("Withdrawal amount cannot be zero.");
            }
            else
            {
                if (_balanceSemaphore.WaitOne(10000))
                {
                    _balance -= amount;
                    _balanceSemaphore.Release(1);
                }
                else
                {
                    // Handle timeout here
                    throw new TimeoutException("Withdrawal timed out. The money has not been taking out of the account.");
                }
            }
        }

        /*
        * Deposits the amount using semaphores
        */
        public void DepositAmount(double amount)
        {
            if (amount == 0)
            {
                throw new InvalidOperationException("Deposit amount cannot be zero.");
            }
            else
            {
                if (_balanceSemaphore.WaitOne(10000))
                {
                    _balance += amount;
                    _balanceSemaphore.Release(1);
                }
                else
                {
                    // Handle timeout here
                    throw new TimeoutException("Deposit timed out. The money has not been deposited into the account.");
                }
            }
        }

        /*
         * Updates the pin number for a bank account (semaphore)
         */
        public void UpdatePin(string newPin)
        {
            if (newPin == _pin)
            {
                throw new InvalidOperationException("New PIN must be different from old PIN.");
            }
            else
            {
                if (_pinSemaphore.WaitOne(10000))
                {
                    _pin += newPin;
                    _pinSemaphore.Release(1);
                }
                else
                {
                    // Handle timeout here
                    throw new TimeoutException("PIN update timed out. The PIN has not been changed.");
                }
            }

        }

        /*
         * Updates the pin for the bank account in an unsafe way
         * (not using semaphores)
         */
        public void UpdatePinUnsafe(string newPin)
        {
            //If this.pin is not equal to the new pin then
            //this.pin is set to the new pin and true is returned
            if (_pin != newPin)
            {
                _pin = newPin;
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
    }
}
