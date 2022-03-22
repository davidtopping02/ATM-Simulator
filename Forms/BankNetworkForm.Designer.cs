
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
            this.txbLogWindow = new System.Windows.Forms.TextBox();
            this.ckbSemaphores = new System.Windows.Forms.CheckBox();
            this.txbDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbATMSettings = new System.Windows.Forms.GroupBox();
            this.btnCreateATM = new System.Windows.Forms.Button();
            this.lblLogWindow = new System.Windows.Forms.Label();
            this.gpbATMSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbLogWindow
            // 
            this.txbLogWindow.Location = new System.Drawing.Point(12, 143);
            this.txbLogWindow.Multiline = true;
            this.txbLogWindow.Name = "txbLogWindow";
            this.txbLogWindow.Size = new System.Drawing.Size(335, 445);
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
            this.lblLogWindow.Location = new System.Drawing.Point(14, 127);
            this.lblLogWindow.Name = "lblLogWindow";
            this.lblLogWindow.Size = new System.Drawing.Size(153, 13);
            this.lblLogWindow.TabIndex = 5;
            this.lblLogWindow.Text = "Bank Network Logging System";
            // 
            // BankNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 600);
            this.Controls.Add(this.lblLogWindow);
            this.Controls.Add(this.gpbATMSettings);
            this.Controls.Add(this.txbLogWindow);
            this.Name = "BankNetworkForm";
            this.Text = "BankNetworkForm";
            this.gpbATMSettings.ResumeLayout(false);
            this.gpbATMSettings.PerformLayout();
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
    }
}