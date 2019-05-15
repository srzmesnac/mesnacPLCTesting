namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Txt_IP = new System.Windows.Forms.TextBox();
            this.userLantern1 = new HslCommunication.Controls.UserLantern();
            this.txt_faddr = new System.Windows.Forms.TextBox();
            this.Txt_Recieve = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_Send = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_FloatTest = new System.Windows.Forms.Button();
            this.btn_Int32Test = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_StrTest = new System.Windows.Forms.Button();
            this.btn_bitTest = new System.Windows.Forms.Button();
            this.btn_Int16Test = new System.Windows.Forms.Button();
            this.txt_int32addr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_straddr = new System.Windows.Forms.TextBox();
            this.txt_bitaddr = new System.Windows.Forms.TextBox();
            this.txt_int16addr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Txt_IP
            // 
            this.Txt_IP.Location = new System.Drawing.Point(68, 21);
            this.Txt_IP.Name = "Txt_IP";
            this.Txt_IP.Size = new System.Drawing.Size(103, 21);
            this.Txt_IP.TabIndex = 1;
            this.Txt_IP.Text = "192.168.0.1";
            // 
            // userLantern1
            // 
            this.userLantern1.BackColor = System.Drawing.Color.Transparent;
            this.userLantern1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.userLantern1.LanternBackground = System.Drawing.Color.Lime;
            this.userLantern1.Location = new System.Drawing.Point(779, 12);
            this.userLantern1.Name = "userLantern1";
            this.userLantern1.Size = new System.Drawing.Size(53, 54);
            this.userLantern1.TabIndex = 2;
            // 
            // txt_faddr
            // 
            this.txt_faddr.Location = new System.Drawing.Point(368, 20);
            this.txt_faddr.Name = "txt_faddr";
            this.txt_faddr.Size = new System.Drawing.Size(103, 21);
            this.txt_faddr.TabIndex = 3;
            this.txt_faddr.Text = "DB4,DBD0";
            this.txt_faddr.TextChanged += new System.EventHandler(this.Txt_faddr_TextChanged);
            // 
            // Txt_Recieve
            // 
            this.Txt_Recieve.Location = new System.Drawing.Point(246, 397);
            this.Txt_Recieve.Multiline = true;
            this.Txt_Recieve.Name = "Txt_Recieve";
            this.Txt_Recieve.Size = new System.Drawing.Size(475, 75);
            this.Txt_Recieve.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(106, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "DB接受区";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(106, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "DB发送区";
            // 
            // Txt_Send
            // 
            this.Txt_Send.Location = new System.Drawing.Point(246, 303);
            this.Txt_Send.Multiline = true;
            this.Txt_Send.Name = "Txt_Send";
            this.Txt_Send.Size = new System.Drawing.Size(475, 68);
            this.Txt_Send.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "PLCIP";
            // 
            // btn_FloatTest
            // 
            this.btn_FloatTest.Location = new System.Drawing.Point(494, 16);
            this.btn_FloatTest.Name = "btn_FloatTest";
            this.btn_FloatTest.Size = new System.Drawing.Size(78, 33);
            this.btn_FloatTest.TabIndex = 12;
            this.btn_FloatTest.Text = "FloatTest";
            this.btn_FloatTest.UseVisualStyleBackColor = true;
            this.btn_FloatTest.Click += new System.EventHandler(this.Btn_FloatTest_Click);
            // 
            // btn_Int32Test
            // 
            this.btn_Int32Test.Location = new System.Drawing.Point(494, 55);
            this.btn_Int32Test.Name = "btn_Int32Test";
            this.btn_Int32Test.Size = new System.Drawing.Size(78, 37);
            this.btn_Int32Test.TabIndex = 13;
            this.btn_Int32Test.Text = "Int32Test";
            this.btn_Int32Test.UseVisualStyleBackColor = true;
            this.btn_Int32Test.Click += new System.EventHandler(this.Btn_Int32Test_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "PLC地址";
            // 
            // btn_StrTest
            // 
            this.btn_StrTest.Location = new System.Drawing.Point(494, 184);
            this.btn_StrTest.Name = "btn_StrTest";
            this.btn_StrTest.Size = new System.Drawing.Size(78, 37);
            this.btn_StrTest.TabIndex = 16;
            this.btn_StrTest.Text = "StrTest";
            this.btn_StrTest.UseVisualStyleBackColor = true;
            this.btn_StrTest.Click += new System.EventHandler(this.Btn_StrTest_Click);
            // 
            // btn_bitTest
            // 
            this.btn_bitTest.Location = new System.Drawing.Point(494, 141);
            this.btn_bitTest.Name = "btn_bitTest";
            this.btn_bitTest.Size = new System.Drawing.Size(78, 37);
            this.btn_bitTest.TabIndex = 14;
            this.btn_bitTest.Text = "bitTest";
            this.btn_bitTest.UseVisualStyleBackColor = true;
            this.btn_bitTest.Click += new System.EventHandler(this.Btn_bitTest_Click);
            // 
            // btn_Int16Test
            // 
            this.btn_Int16Test.Location = new System.Drawing.Point(494, 98);
            this.btn_Int16Test.Name = "btn_Int16Test";
            this.btn_Int16Test.Size = new System.Drawing.Size(78, 37);
            this.btn_Int16Test.TabIndex = 17;
            this.btn_Int16Test.Text = "Int16Test";
            this.btn_Int16Test.UseVisualStyleBackColor = true;
            this.btn_Int16Test.Click += new System.EventHandler(this.Btn_Int16Test_Click);
            // 
            // txt_int32addr
            // 
            this.txt_int32addr.Location = new System.Drawing.Point(368, 64);
            this.txt_int32addr.Name = "txt_int32addr";
            this.txt_int32addr.Size = new System.Drawing.Size(103, 21);
            this.txt_int32addr.TabIndex = 18;
            this.txt_int32addr.Text = "DB2,DBD0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(317, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(305, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "PLC地址";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txt_straddr);
            this.groupBox1.Controls.Add(this.txt_bitaddr);
            this.groupBox1.Controls.Add(this.txt_int16addr);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_Int16Test);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btn_StrTest);
            this.groupBox1.Controls.Add(this.Txt_IP);
            this.groupBox1.Controls.Add(this.btn_bitTest);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn_Int32Test);
            this.groupBox1.Controls.Add(this.btn_FloatTest);
            this.groupBox1.Controls.Add(this.txt_faddr);
            this.groupBox1.Controls.Add(this.txt_int32addr);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(761, 274);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txt_straddr
            // 
            this.txt_straddr.Location = new System.Drawing.Point(368, 187);
            this.txt_straddr.Name = "txt_straddr";
            this.txt_straddr.Size = new System.Drawing.Size(103, 21);
            this.txt_straddr.TabIndex = 26;
            this.txt_straddr.Text = "DB4,DBD0";
            // 
            // txt_bitaddr
            // 
            this.txt_bitaddr.Location = new System.Drawing.Point(368, 150);
            this.txt_bitaddr.Name = "txt_bitaddr";
            this.txt_bitaddr.Size = new System.Drawing.Size(103, 21);
            this.txt_bitaddr.TabIndex = 25;
            this.txt_bitaddr.Text = "DB5,DBX102.1";
            // 
            // txt_int16addr
            // 
            this.txt_int16addr.Location = new System.Drawing.Point(368, 107);
            this.txt_int16addr.Name = "txt_int16addr";
            this.txt_int16addr.Size = new System.Drawing.Size(103, 21);
            this.txt_int16addr.TabIndex = 24;
            this.txt_int16addr.Text = "DB3,DBD0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(305, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "PLC地址";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(305, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "PLC地址";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(305, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "PLC地址";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 541);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Txt_Send);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_Recieve);
            this.Controls.Add(this.userLantern1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Txt_IP;
        private HslCommunication.Controls.UserLantern userLantern1;
        private System.Windows.Forms.TextBox Txt_Recieve;
        private System.Windows.Forms.TextBox txt_faddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_Send;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_FloatTest;
        private System.Windows.Forms.Button btn_Int32Test;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_StrTest;
        private System.Windows.Forms.Button btn_bitTest;
        private System.Windows.Forms.Button btn_Int16Test;
        private System.Windows.Forms.TextBox txt_int32addr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_straddr;
        private System.Windows.Forms.TextBox txt_bitaddr;
        private System.Windows.Forms.TextBox txt_int16addr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
    }
}

