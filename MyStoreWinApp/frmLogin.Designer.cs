namespace MyStoreWinApp
{
    partial class frmLogin
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
            lblEmail = new Label();
            lblPass = new Label();
            txtEmail = new TextBox();
            txtPass = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(158, 63);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(158, 141);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(70, 20);
            lblPass.TabIndex = 1;
            lblPass.Text = "Password";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(276, 63);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(232, 27);
            txtEmail.TabIndex = 2;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(276, 134);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(232, 27);
            txtPass.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(318, 215);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 29);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(613, 343);
            Controls.Add(btnLogin);
            Controls.Add(txtPass);
            Controls.Add(txtEmail);
            Controls.Add(lblPass);
            Controls.Add(lblEmail);
            Name = "frmLogin";
            Text = "frmLogin";
            Load += frmLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEmail;
        private Label lblPass;
        private TextBox txtEmail;
        private TextBox txtPass;
        private Button btnLogin;
    }
}