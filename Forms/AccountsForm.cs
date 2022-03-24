using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATMSimulator.Classes;

namespace ATMSimulator.Forms
{
    public partial class AccountsForm : Form
    {
        public AccountsForm(Dictionary<string, Account> accountsDict)
        {
            InitializeComponent();

            foreach (var account in accountsDict)
            {
                Account a = account.Value;
                DataGridViewRow row = (DataGridViewRow)dgvAccounts.Rows[0].Clone();

                row.Cells[0].Value = a.GetNumber();
                row.Cells[1].Value = a.GetName();
                row.Cells[2].Value = a.GetBalance();
                row.Cells[3].Value = a.GetPin();
                row.Cells[4].Value = a.IsLocked() ? "True" : "False";

                dgvAccounts.Rows.Add(row);
            }
        }
    }
}
