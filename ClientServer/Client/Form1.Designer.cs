namespace Client
{
    partial class Form1
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
            this.grptrading = new System.Windows.Forms.GroupBox();
            this.btnSell = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQnty = new System.Windows.Forms.TextBox();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lblrate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStockname = new System.Windows.Forms.TextBox();
            this.Query = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblusernameResponse = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.grptrading.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grptrading
            // 
            this.grptrading.Controls.Add(this.btnSell);
            this.grptrading.Controls.Add(this.label5);
            this.grptrading.Controls.Add(this.txtQnty);
            this.grptrading.Controls.Add(this.btnBuy);
            this.grptrading.Controls.Add(this.lblrate);
            this.grptrading.Controls.Add(this.label1);
            this.grptrading.Controls.Add(this.txtStockname);
            this.grptrading.Controls.Add(this.Query);
            this.grptrading.Location = new System.Drawing.Point(28, 106);
            this.grptrading.Name = "grptrading";
            this.grptrading.Size = new System.Drawing.Size(447, 145);
            this.grptrading.TabIndex = 5;
            this.grptrading.TabStop = false;
            this.grptrading.Text = "Stock Trading";
            // 
            // btnSell
            // 
            this.btnSell.Location = new System.Drawing.Point(117, 100);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(75, 23);
            this.btnSell.TabIndex = 15;
            this.btnSell.Text = "Sell";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Quantity";
            // 
            // txtQnty
            // 
            this.txtQnty.Location = new System.Drawing.Point(92, 60);
            this.txtQnty.Name = "txtQnty";
            this.txtQnty.Size = new System.Drawing.Size(100, 20);
            this.txtQnty.TabIndex = 13;
            // 
            // btnBuy
            // 
            this.btnBuy.Location = new System.Drawing.Point(23, 100);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(75, 23);
            this.btnBuy.TabIndex = 9;
            this.btnBuy.Text = "Buy";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lblrate
            // 
            this.lblrate.AutoSize = true;
            this.lblrate.Location = new System.Drawing.Point(201, 36);
            this.lblrate.Name = "lblrate";
            this.lblrate.Size = new System.Drawing.Size(128, 13);
            this.lblrate.TabIndex = 8;
            this.lblrate.Text = "<Show Responses Here>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Stock Name";
            // 
            // txtStockname
            // 
            this.txtStockname.Location = new System.Drawing.Point(92, 33);
            this.txtStockname.Name = "txtStockname";
            this.txtStockname.Size = new System.Drawing.Size(100, 20);
            this.txtStockname.TabIndex = 6;
            // 
            // Query
            // 
            this.Query.Location = new System.Drawing.Point(335, 31);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(75, 23);
            this.Query.TabIndex = 5;
            this.Query.Text = "Query";
            this.Query.UseVisualStyleBackColor = true;
            this.Query.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblusernameResponse);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtuser);
            this.groupBox2.Controls.Add(this.btnLogin);
            this.groupBox2.Location = new System.Drawing.Point(28, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 70);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Login";
            // 
            // lblusernameResponse
            // 
            this.lblusernameResponse.AutoSize = true;
            this.lblusernameResponse.Location = new System.Drawing.Point(201, 31);
            this.lblusernameResponse.Name = "lblusernameResponse";
            this.lblusernameResponse.Size = new System.Drawing.Size(123, 13);
            this.lblusernameResponse.TabIndex = 17;
            this.lblusernameResponse.Text = "<Show Response Here>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "UserName";
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(92, 28);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(100, 20);
            this.txtuser.TabIndex = 15;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(335, 25);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 278);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grptrading);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.grptrading.ResumeLayout(false);
            this.grptrading.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grptrading;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQnty;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Label lblrate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStockname;
        private System.Windows.Forms.Button Query;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblusernameResponse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.Button btnLogin;

    }
}

