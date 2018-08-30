namespace code_PC_doan
{
    partial class Dangnhapdatabase
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
            this.BT_connect = new System.Windows.Forms.Button();
            this.TB_database = new System.Windows.Forms.TextBox();
            this.database = new System.Windows.Forms.Label();
            this.TB_iphost = new System.Windows.Forms.TextBox();
            this.IP_host = new System.Windows.Forms.Label();
            this.TB_password = new System.Windows.Forms.TextBox();
            this.LB_password = new System.Windows.Forms.Label();
            this.TB_user = new System.Windows.Forms.TextBox();
            this.LB_user = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BT_connect
            // 
            this.BT_connect.Location = new System.Drawing.Point(128, 178);
            this.BT_connect.Name = "BT_connect";
            this.BT_connect.Size = new System.Drawing.Size(75, 23);
            this.BT_connect.TabIndex = 9;
            this.BT_connect.Text = "Connect";
            this.BT_connect.UseVisualStyleBackColor = true;
            this.BT_connect.Click += new System.EventHandler(this.BT_connect_Click);
            // 
            // TB_database
            // 
            this.TB_database.Location = new System.Drawing.Point(114, 66);
            this.TB_database.Name = "TB_database";
            this.TB_database.Size = new System.Drawing.Size(100, 20);
            this.TB_database.TabIndex = 8;
            // 
            // database
            // 
            this.database.AutoSize = true;
            this.database.Location = new System.Drawing.Point(42, 69);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(51, 13);
            this.database.TabIndex = 7;
            this.database.Text = "database";
            // 
            // TB_iphost
            // 
            this.TB_iphost.Location = new System.Drawing.Point(114, 35);
            this.TB_iphost.Name = "TB_iphost";
            this.TB_iphost.Size = new System.Drawing.Size(100, 20);
            this.TB_iphost.TabIndex = 6;
            // 
            // IP_host
            // 
            this.IP_host.AutoSize = true;
            this.IP_host.Cursor = System.Windows.Forms.Cursors.Default;
            this.IP_host.Location = new System.Drawing.Point(42, 38);
            this.IP_host.Name = "IP_host";
            this.IP_host.Size = new System.Drawing.Size(40, 13);
            this.IP_host.TabIndex = 5;
            this.IP_host.Text = "IP host";
            // 
            // TB_password
            // 
            this.TB_password.Location = new System.Drawing.Point(114, 128);
            this.TB_password.Name = "TB_password";
            this.TB_password.Size = new System.Drawing.Size(100, 20);
            this.TB_password.TabIndex = 13;
            // 
            // LB_password
            // 
            this.LB_password.AutoSize = true;
            this.LB_password.Location = new System.Drawing.Point(42, 131);
            this.LB_password.Name = "LB_password";
            this.LB_password.Size = new System.Drawing.Size(52, 13);
            this.LB_password.TabIndex = 12;
            this.LB_password.Text = "password";
            // 
            // TB_user
            // 
            this.TB_user.Location = new System.Drawing.Point(114, 97);
            this.TB_user.Name = "TB_user";
            this.TB_user.Size = new System.Drawing.Size(100, 20);
            this.TB_user.TabIndex = 11;
            // 
            // LB_user
            // 
            this.LB_user.AutoSize = true;
            this.LB_user.Cursor = System.Windows.Forms.Cursors.Default;
            this.LB_user.Location = new System.Drawing.Point(42, 100);
            this.LB_user.Name = "LB_user";
            this.LB_user.Size = new System.Drawing.Size(55, 13);
            this.LB_user.TabIndex = 10;
            this.LB_user.Text = "ussename";
            // 
            // Dangnhapdatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 235);
            this.Controls.Add(this.TB_password);
            this.Controls.Add(this.LB_password);
            this.Controls.Add(this.TB_user);
            this.Controls.Add(this.LB_user);
            this.Controls.Add(this.BT_connect);
            this.Controls.Add(this.TB_database);
            this.Controls.Add(this.database);
            this.Controls.Add(this.TB_iphost);
            this.Controls.Add(this.IP_host);
            this.MaximumSize = new System.Drawing.Size(286, 274);
            this.MinimumSize = new System.Drawing.Size(286, 274);
            this.Name = "Dangnhapdatabase";
            this.Text = "Đăng nhập database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BT_connect;
        private System.Windows.Forms.TextBox TB_database;
        private System.Windows.Forms.Label database;
        private System.Windows.Forms.TextBox TB_iphost;
        private System.Windows.Forms.Label IP_host;
        private System.Windows.Forms.TextBox TB_password;
        private System.Windows.Forms.Label LB_password;
        private System.Windows.Forms.TextBox TB_user;
        private System.Windows.Forms.Label LB_user;
    }
}