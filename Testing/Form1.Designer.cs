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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_strLength = new System.Windows.Forms.TextBox();
            this.txt_ThreadNumber = new System.Windows.Forms.TextBox();
            this.btn_word = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_wordaddr = new System.Windows.Forms.TextBox();
            this.txt_startDB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_straddr = new System.Windows.Forms.TextBox();
            this.txt_bitaddr = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_int16addr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txt_log = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.listDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.threadingTestBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadingTestBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Txt_IP
            // 
            this.Txt_IP.Location = new System.Drawing.Point(68, 14);
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
            this.userLantern1.Location = new System.Drawing.Point(701, 16);
            this.userLantern1.Name = "userLantern1";
            this.userLantern1.Size = new System.Drawing.Size(53, 54);
            this.userLantern1.TabIndex = 2;
            // 
            // txt_faddr
            // 
            this.txt_faddr.Location = new System.Drawing.Point(244, 15);
            this.txt_faddr.Name = "txt_faddr";
            this.txt_faddr.Size = new System.Drawing.Size(103, 21);
            this.txt_faddr.TabIndex = 3;
            this.txt_faddr.Text = "DB4,DBD0";
            this.txt_faddr.TextChanged += new System.EventHandler(this.Txt_faddr_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "PLCIP";
            // 
            // btn_FloatTest
            // 
            this.btn_FloatTest.Location = new System.Drawing.Point(370, 11);
            this.btn_FloatTest.Name = "btn_FloatTest";
            this.btn_FloatTest.Size = new System.Drawing.Size(78, 33);
            this.btn_FloatTest.TabIndex = 12;
            this.btn_FloatTest.Text = "FloatTest";
            this.btn_FloatTest.UseVisualStyleBackColor = true;
            this.btn_FloatTest.Click += new System.EventHandler(this.Btn_FloatTest_Click);
            // 
            // btn_Int32Test
            // 
            this.btn_Int32Test.Location = new System.Drawing.Point(370, 50);
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
            this.label4.Location = new System.Drawing.Point(181, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "PLC地址";
            // 
            // btn_StrTest
            // 
            this.btn_StrTest.Location = new System.Drawing.Point(370, 179);
            this.btn_StrTest.Name = "btn_StrTest";
            this.btn_StrTest.Size = new System.Drawing.Size(78, 37);
            this.btn_StrTest.TabIndex = 16;
            this.btn_StrTest.Text = "StrTest";
            this.btn_StrTest.UseVisualStyleBackColor = true;
            this.btn_StrTest.Click += new System.EventHandler(this.Btn_StrTest_Click);
            // 
            // btn_bitTest
            // 
            this.btn_bitTest.Location = new System.Drawing.Point(370, 136);
            this.btn_bitTest.Name = "btn_bitTest";
            this.btn_bitTest.Size = new System.Drawing.Size(78, 37);
            this.btn_bitTest.TabIndex = 14;
            this.btn_bitTest.Text = "bitTest";
            this.btn_bitTest.UseVisualStyleBackColor = true;
            this.btn_bitTest.Click += new System.EventHandler(this.Btn_bitTest_Click);
            // 
            // btn_Int16Test
            // 
            this.btn_Int16Test.Location = new System.Drawing.Point(370, 93);
            this.btn_Int16Test.Name = "btn_Int16Test";
            this.btn_Int16Test.Size = new System.Drawing.Size(78, 37);
            this.btn_Int16Test.TabIndex = 17;
            this.btn_Int16Test.Text = "Int16Test";
            this.btn_Int16Test.UseVisualStyleBackColor = true;
            this.btn_Int16Test.Click += new System.EventHandler(this.Btn_Int16Test_Click);
            // 
            // txt_int32addr
            // 
            this.txt_int32addr.Location = new System.Drawing.Point(244, 59);
            this.txt_int32addr.Name = "txt_int32addr";
            this.txt_int32addr.Size = new System.Drawing.Size(103, 21);
            this.txt_int32addr.TabIndex = 18;
            this.txt_int32addr.Text = "DB2,DBD0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "PLC地址";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_strLength);
            this.groupBox1.Controls.Add(this.txt_ThreadNumber);
            this.groupBox1.Controls.Add(this.btn_word);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_wordaddr);
            this.groupBox1.Controls.Add(this.txt_startDB);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txt_straddr);
            this.groupBox1.Controls.Add(this.txt_bitaddr);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txt_int16addr);
            this.groupBox1.Controls.Add(this.userLantern1);
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
            this.groupBox1.Size = new System.Drawing.Size(1150, 274);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(913, 145);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(218, 55);
            this.textBox1.TabIndex = 35;
            this.textBox1.Text = "DB1,DBD1|200;DB2,DBD1|200";
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(772, 153);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 38);
            this.button3.TabIndex = 34;
            this.button3.Text = "同一PLC多DB块同时读取";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(958, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "字节长度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(935, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 31;
            this.label2.Text = "DB块数";
            // 
            // txt_strLength
            // 
            this.txt_strLength.Location = new System.Drawing.Point(1028, 102);
            this.txt_strLength.Name = "txt_strLength";
            this.txt_strLength.Size = new System.Drawing.Size(103, 21);
            this.txt_strLength.TabIndex = 31;
            this.txt_strLength.Text = "3000";
            // 
            // txt_ThreadNumber
            // 
            this.txt_ThreadNumber.Location = new System.Drawing.Point(1031, 62);
            this.txt_ThreadNumber.Name = "txt_ThreadNumber";
            this.txt_ThreadNumber.Size = new System.Drawing.Size(100, 21);
            this.txt_ThreadNumber.TabIndex = 30;
            // 
            // btn_word
            // 
            this.btn_word.Location = new System.Drawing.Point(370, 226);
            this.btn_word.Name = "btn_word";
            this.btn_word.Size = new System.Drawing.Size(78, 37);
            this.btn_word.TabIndex = 30;
            this.btn_word.Text = "WordTest";
            this.btn_word.UseVisualStyleBackColor = true;
            this.btn_word.Click += new System.EventHandler(this.Btn_word_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(935, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "起始DB块";
            // 
            // txt_wordaddr
            // 
            this.txt_wordaddr.Location = new System.Drawing.Point(244, 235);
            this.txt_wordaddr.Name = "txt_wordaddr";
            this.txt_wordaddr.Size = new System.Drawing.Size(103, 21);
            this.txt_wordaddr.TabIndex = 29;
            this.txt_wordaddr.Text = "DB4,DBD0";
            // 
            // txt_startDB
            // 
            this.txt_startDB.Location = new System.Drawing.Point(1031, 25);
            this.txt_startDB.Name = "txt_startDB";
            this.txt_startDB.Size = new System.Drawing.Size(100, 21);
            this.txt_startDB.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(181, 244);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 28;
            this.label10.Text = "PLC地址";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(772, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 38);
            this.button2.TabIndex = 22;
            this.button2.Text = "停止测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // txt_straddr
            // 
            this.txt_straddr.Location = new System.Drawing.Point(244, 188);
            this.txt_straddr.Name = "txt_straddr";
            this.txt_straddr.Size = new System.Drawing.Size(103, 21);
            this.txt_straddr.TabIndex = 26;
            this.txt_straddr.Text = "DB10,DBD0";
            // 
            // txt_bitaddr
            // 
            this.txt_bitaddr.Location = new System.Drawing.Point(244, 145);
            this.txt_bitaddr.Name = "txt_bitaddr";
            this.txt_bitaddr.Size = new System.Drawing.Size(103, 21);
            this.txt_bitaddr.TabIndex = 25;
            this.txt_bitaddr.Text = "DB5,DBX102.1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(772, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 37);
            this.button1.TabIndex = 27;
            this.button1.Text = "多线程测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txt_int16addr
            // 
            this.txt_int16addr.Location = new System.Drawing.Point(244, 102);
            this.txt_int16addr.Name = "txt_int16addr";
            this.txt_int16addr.Size = new System.Drawing.Size(103, 21);
            this.txt_int16addr.TabIndex = 24;
            this.txt_int16addr.Text = "DB3,DBD0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(181, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "PLC地址";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(181, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "PLC地址";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(181, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "PLC地址";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(26, 39);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_log.Size = new System.Drawing.Size(442, 216);
            this.txt_log.TabIndex = 32;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_log);
            this.groupBox2.Location = new System.Drawing.Point(20, 292);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 363);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // listDataBindingSource
            // 
            this.listDataBindingSource.DataMember = "ListData";
            this.listDataBindingSource.DataSource = this.threadingTestBindingSource;
            this.listDataBindingSource.CurrentChanged += new System.EventHandler(this.ListDataBindingSource_CurrentChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "LastDate";
            this.dataGridViewTextBoxColumn1.HeaderText = "LastDate";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "LastDate";
            this.dataGridViewTextBoxColumn2.HeaderText = "LastDate";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(542, 25);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(41, 16);
            this.radioButton1.TabIndex = 36;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "HSL";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(542, 47);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(77, 16);
            this.radioButton2.TabIndex = 37;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Labnodave";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // threadingTestBindingSource
            // 
            this.threadingTestBindingSource.DataSource = typeof(WindowsFormsApp1.ThreadingTest);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 673);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadingTestBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox Txt_IP;
        private HslCommunication.Controls.UserLantern userLantern1;
        private System.Windows.Forms.TextBox txt_faddr;
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
        private System.Windows.Forms.Button btn_word;
        private System.Windows.Forms.TextBox txt_wordaddr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_startDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ThreadNumber;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_strLength;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.BindingSource threadingTestBindingSource;
        private System.Windows.Forms.BindingSource listDataBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

