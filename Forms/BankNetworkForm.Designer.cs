
namespace ATMSimulator.Forms
{
    partial class BankNetworkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankNetworkForm));
            this.txbLogWindow = new System.Windows.Forms.TextBox();
            this.ckbSemaphores = new System.Windows.Forms.CheckBox();
            this.txbDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbATMSettings = new System.Windows.Forms.GroupBox();
            this.btnCreateATM = new System.Windows.Forms.Button();
            this.lblLogWindow = new System.Windows.Forms.Label();
            this.gpbAddAccount = new System.Windows.Forms.GroupBox();
            this.txbBalance = new System.Windows.Forms.TextBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.txbPin = new System.Windows.Forms.TextBox();
            this.lblPin = new System.Windows.Forms.Label();
            this.txbAccountNumber = new System.Windows.Forms.TextBox();
            this.lblAccountNumber = new System.Windows.Forms.Label();
            this.txbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.gpbManage = new System.Windows.Forms.GroupBox();
            this.btnViewAllAccounts = new System.Windows.Forms.Button();
            this.btnUnlockAccount = new System.Windows.Forms.Button();
            this.gpbATMSettings.SuspendLayout();
            this.gpbAddAccount.SuspendLayout();
            this.gpbManage.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbLogWindow
            // 
            this.txbLogWindow.Location = new System.Drawing.Point(12, 231);
            this.txbLogWindow.Multiline = true;
            this.txbLogWindow.Name = "txbLogWindow";
            this.txbLogWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbLogWindow.Size = new System.Drawing.Size(335, 469);
            this.txbLogWindow.TabIndex = 0;
            // 
            // ckbSemaphores
            // 
            this.ckbSemaphores.AutoSize = true;
            this.ckbSemaphores.Location = new System.Drawing.Point(6, 19);
            this.ckbSemaphores.Name = "ckbSemaphores";
            this.ckbSemaphores.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckbSemaphores.Size = new System.Drawing.Size(134, 17);
            this.ckbSemaphores.TabIndex = 1;
            this.ckbSemaphores.Text = "ATM uses semaphores";
            this.ckbSemaphores.UseVisualStyleBackColor = true;
            // 
            // txbDelay
            // 
            this.txbDelay.Location = new System.Drawing.Point(106, 43);
            this.txbDelay.Name = "txbDelay";
            this.txbDelay.Size = new System.Drawing.Size(34, 20);
            this.txbDelay.TabIndex = 2;
            this.txbDelay.Text = "3";
            this.txbDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Transaction Delay";
            // 
            // gpbATMSettings
            // 
            this.gpbATMSettings.Controls.Add(this.btnCreateATM);
            this.gpbATMSettings.Controls.Add(this.ckbSemaphores);
            this.gpbATMSettings.Controls.Add(this.label1);
            this.gpbATMSettings.Controls.Add(this.txbDelay);
            this.gpbATMSettings.Location = new System.Drawing.Point(12, 12);
            this.gpbATMSettings.Name = "gpbATMSettings";
            this.gpbATMSettings.Size = new System.Drawing.Size(148, 100);
            this.gpbATMSettings.TabIndex = 4;
            this.gpbATMSettings.TabStop = false;
            this.gpbATMSettings.Text = "Start new ATM";
            // 
            // btnCreateATM
            // 
            this.btnCreateATM.Location = new System.Drawing.Point(10, 69);
            this.btnCreateATM.Name = "btnCreateATM";
            this.btnCreateATM.Size = new System.Drawing.Size(130, 23);
            this.btnCreateATM.TabIndex = 5;
            this.btnCreateATM.Text = "Create ATM";
            this.btnCreateATM.UseVisualStyleBackColor = true;
            this.btnCreateATM.Click += new System.EventHandler(this.btnCreateATM_Click);
            // 
            // lblLogWindow
            // 
            this.lblLogWindow.AutoSize = true;
            this.lblLogWindow.Location = new System.Drawing.Point(15, 215);
            this.lblLogWindow.Name = "lblLogWindow";
            this.lblLogWindow.Size = new System.Drawing.Size(153, 13);
            this.lblLogWindow.TabIndex = 5;
            this.lblLogWindow.Text = "Bank Network Logging System";
            // 
            // gpbAddAccount
            // 
            this.gpbAddAccount.Controls.Add(this.txbBalance);
            this.gpbAddAccount.Controls.Add(this.lblBalance);
            this.gpbAddAccount.Controls.Add(this.txbPin);
            this.gpbAddAccount.Controls.Add(this.lblPin);
            this.gpbAddAccount.Controls.Add(this.txbAccountNumber);
            this.gpbAddAccount.Controls.Add(this.lblAccountNumber);
            this.gpbAddAccount.Controls.Add(this.txbName);
            this.gpbAddAccount.Controls.Add(this.lblName);
            this.gpbAddAccount.Controls.Add(this.btnAddAccount);
            this.gpbAddAccount.Location = new System.Drawing.Point(166, 12);
            this.gpbAddAccount.Name = "gpbAddAccount";
            this.gpbAddAccount.Size = new System.Drawing.Size(182, 186);
            this.gpbAddAccount.TabIndex = 6;
            this.gpbAddAccount.TabStop = false;
            this.gpbAddAccount.Text = "Open new account";
            // 
            // txbBalance
            // 
            this.txbBalance.Location = new System.Drawing.Point(76, 98);
            this.txbBalance.Name = "txbBalance";
            this.txbBalance.Size = new System.Drawing.Size(100, 20);
            this.txbBalance.TabIndex = 8;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(6, 101);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(66, 13);
            this.lblBalance.TabIndex = 7;
            this.lblBalance.Text = "Balance in £";
            // 
            // txbPin
            // 
            this.txbPin.Location = new System.Drawing.Point(76, 72);
            this.txbPin.Name = "txbPin";
            this.txbPin.Size = new System.Drawing.Size(100, 20);
            this.txbPin.TabIndex = 6;
            // 
            // lblPin
            // 
            this.lblPin.AutoSize = true;
            this.lblPin.Location = new System.Drawing.Point(6, 75);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(25, 13);
            this.lblPin.TabIndex = 5;
            this.lblPin.Text = "PIN";
            // 
            // txbAccountNumber
            // 
            this.txbAccountNumber.Location = new System.Drawing.Point(76, 46);
            this.txbAccountNumber.Name = "txbAccountNumber";
            this.txbAccountNumber.Size = new System.Drawing.Size(100, 20);
            this.txbAccountNumber.TabIndex = 4;
            // 
            // lblAccountNumber
            // 
            this.lblAccountNumber.AutoSize = true;
            this.lblAccountNumber.Location = new System.Drawing.Point(6, 49);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.Size = new System.Drawing.Size(65, 13);
            this.lblAccountNumber.TabIndex = 3;
            this.lblAccountNumber.Text = "Account no.";
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(76, 19);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(100, 20);
            this.txbName.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.Location = new System.Drawing.Point(6, 154);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(170, 23);
            this.btnAddAccount.TabIndex = 0;
            this.btnAddAccount.Text = "Open Account";
            this.btnAddAccount.UseVisualStyleBackColor = true;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddCount_Click);
            // 
            // gpbManage
            // 
            this.gpbManage.Controls.Add(this.btnViewAllAccounts);
            this.gpbManage.Controls.Add(this.btnUnlockAccount);
            this.gpbManage.Location = new System.Drawing.Point(12, 118);
            this.gpbManage.Name = "gpbManage";
            this.gpbManage.Size = new System.Drawing.Size(148, 80);
            this.gpbManage.TabIndex = 6;
            this.gpbManage.TabStop = false;
            this.gpbManage.Text = "Account Management";
            // 
            // btnViewAllAccounts
            // 
            this.btnViewAllAccounts.Location = new System.Drawing.Point(10, 48);
            this.btnViewAllAccounts.Name = "btnViewAllAccounts";
            this.btnViewAllAccounts.Size = new System.Drawing.Size(130, 23);
            this.btnViewAllAccounts.TabIndex = 6;
            this.btnViewAllAccounts.Text = "View All Accounts";
            this.btnViewAllAccounts.UseVisualStyleBackColor = true;
            this.btnViewAllAccounts.Click += new System.EventHandler(this.btnViewAllAccounts_Click);
            // 
            // btnUnlockAccount
            // 
            this.btnUnlockAccount.Location = new System.Drawing.Point(10, 19);
            this.btnUnlockAccount.Name = "btnUnlockAccount";
            this.btnUnlockAccount.Size = new System.Drawing.Size(130, 23);
            this.btnUnlockAccount.TabIndex = 5;
            this.btnUnlockAccount.Text = "Unlock Account";
            this.btnUnlockAccount.UseVisualStyleBackColor = true;
            this.btnUnlockAccount.Click += new System.EventHandler(this.btnUnlockAccount_Click);
            // 
            // BankNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 712);
            this.Controls.Add(this.gpbManage);
            this.Controls.Add(this.gpbAddAccount);
            this.Controls.Add(this.lblLogWindow);
            this.Controls.Add(this.gpbATMSettings);
            this.Controls.Add(this.txbLogWindow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BankNetworkForm";
            this.Text = "Central Bank Network";
            this.gpbATMSettings.ResumeLayout(false);
            this.gpbATMSettings.PerformLayout();
            this.gpbAddAccount.ResumeLayout(false);
            this.gpbAddAccount.PerformLayout();
            this.gpbManage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbLogWindow;
        private System.Windows.Forms.CheckBox ckbSemaphores;
        private System.Windows.Forms.TextBox txbDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpbATMSettings;
        private System.Windows.Forms.Button btnCreateATM;
        private System.Windows.Forms.Label lblLogWindow;
        private System.Windows.Forms.GroupBox gpbAddAccount;
        private System.Windows.Forms.TextBox txbBalance;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.TextBox txbPin;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.TextBox txbAccountNumber;
        private System.Windows.Forms.Label lblAccountNumber;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.GroupBox gpbManage;
        private System.Windows.Forms.Button btnViewAllAccounts;
        private System.Windows.Forms.Button btnUnlockAccount;
    }
}