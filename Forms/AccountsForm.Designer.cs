namespace ATMSimulator.Forms
{
    partial class AccountsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsForm));
            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.account_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_owner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_pin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_locked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.account_number,
            this.account_owner,
            this.account_balance,
            this.account_pin,
            this.account_locked});
            this.dgvAccounts.Location = new System.Drawing.Point(12, 12);
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.Size = new System.Drawing.Size(776, 426);
            this.dgvAccounts.TabIndex = 0;
            // 
            // account_number
            // 
            this.account_number.HeaderText = "Account number";
            this.account_number.Name = "account_number";
            this.account_number.ReadOnly = true;
            // 
            // account_owner
            // 
            this.account_owner.HeaderText = "Account owner";
            this.account_owner.Name = "account_owner";
            this.account_owner.ReadOnly = true;
            // 
            // account_balance
            // 
            this.account_balance.HeaderText = "Balance";
            this.account_balance.Name = "account_balance";
            this.account_balance.ReadOnly = true;
            // 
            // account_pin
            // 
            this.account_pin.HeaderText = "PIN";
            this.account_pin.Name = "account_pin";
            this.account_pin.ReadOnly = true;
            // 
            // account_locked
            // 
            this.account_locked.HeaderText = "Locked?";
            this.account_locked.Name = "account_locked";
            this.account_locked.ReadOnly = true;
            // 
            // AccountsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvAccounts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AccountsForm";
            this.Text = "Accounts Database";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAccounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_owner;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_pin;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_locked;
    }
}