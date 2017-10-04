namespace DB
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbPassNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTimeSearch = new System.Windows.Forms.Label();
            this.lblStudName = new System.Windows.Forms.Label();
            this.lblLocality = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.res_3 = new System.Windows.Forms.GroupBox();
            this.lblRes3Ans = new System.Windows.Forms.Label();
            this.lblRes3Subj = new System.Windows.Forms.Label();
            this.res_2 = new System.Windows.Forms.GroupBox();
            this.lblRes2Ans = new System.Windows.Forms.Label();
            this.lblRes2Subj = new System.Windows.Forms.Label();
            this.res_1 = new System.Windows.Forms.GroupBox();
            this.lblRes1Ans = new System.Windows.Forms.Label();
            this.lblRes1Subj = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.res_3.SuspendLayout();
            this.res_2.SuspendLayout();
            this.res_1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(30, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbPassNumber
            // 
            this.tbPassNumber.Location = new System.Drawing.Point(15, 19);
            this.tbPassNumber.Name = "tbPassNumber";
            this.tbPassNumber.Size = new System.Drawing.Size(100, 20);
            this.tbPassNumber.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTimeSearch);
            this.groupBox1.Controls.Add(this.tbPassNumber);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(510, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 104);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск";
            // 
            // lblTimeSearch
            // 
            this.lblTimeSearch.AutoSize = true;
            this.lblTimeSearch.Location = new System.Drawing.Point(12, 83);
            this.lblTimeSearch.Name = "lblTimeSearch";
            this.lblTimeSearch.Size = new System.Drawing.Size(85, 13);
            this.lblTimeSearch.TabIndex = 2;
            this.lblTimeSearch.Text = "Время поиска: ";
            // 
            // lblStudName
            // 
            this.lblStudName.AutoSize = true;
            this.lblStudName.Location = new System.Drawing.Point(18, 26);
            this.lblStudName.Name = "lblStudName";
            this.lblStudName.Size = new System.Drawing.Size(37, 13);
            this.lblStudName.TabIndex = 3;
            this.lblStudName.Text = "ФИО:";
            // 
            // lblLocality
            // 
            this.lblLocality.AutoSize = true;
            this.lblLocality.Location = new System.Drawing.Point(18, 50);
            this.lblLocality.Name = "lblLocality";
            this.lblLocality.Size = new System.Drawing.Size(60, 13);
            this.lblLocality.TabIndex = 4;
            this.lblLocality.Text = "Прописка:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.lblLocality);
            this.groupBox2.Controls.Add(this.lblStudName);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 431);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Абитуриент";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.res_3);
            this.groupBox3.Controls.Add(this.res_2);
            this.groupBox3.Controls.Add(this.res_1);
            this.groupBox3.Location = new System.Drawing.Point(21, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(471, 340);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Результаты";
            // 
            // res_3
            // 
            this.res_3.Controls.Add(this.lblRes3Ans);
            this.res_3.Controls.Add(this.lblRes3Subj);
            this.res_3.Location = new System.Drawing.Point(6, 231);
            this.res_3.Name = "res_3";
            this.res_3.Size = new System.Drawing.Size(459, 100);
            this.res_3.TabIndex = 2;
            this.res_3.TabStop = false;
            this.res_3.Text = "Тест_3";
            this.res_3.Visible = false;
            // 
            // lblRes3Ans
            // 
            this.lblRes3Ans.AutoSize = true;
            this.lblRes3Ans.Location = new System.Drawing.Point(16, 60);
            this.lblRes3Ans.Name = "lblRes3Ans";
            this.lblRes3Ans.Size = new System.Drawing.Size(48, 13);
            this.lblRes3Ans.TabIndex = 1;
            this.lblRes3Ans.Text = "Ответы:";
            // 
            // lblRes3Subj
            // 
            this.lblRes3Subj.AutoSize = true;
            this.lblRes3Subj.Location = new System.Drawing.Point(16, 25);
            this.lblRes3Subj.Name = "lblRes3Subj";
            this.lblRes3Subj.Size = new System.Drawing.Size(55, 13);
            this.lblRes3Subj.TabIndex = 0;
            this.lblRes3Subj.Text = "Предмет:";
            // 
            // res_2
            // 
            this.res_2.Controls.Add(this.lblRes2Ans);
            this.res_2.Controls.Add(this.lblRes2Subj);
            this.res_2.Location = new System.Drawing.Point(6, 125);
            this.res_2.Name = "res_2";
            this.res_2.Size = new System.Drawing.Size(459, 100);
            this.res_2.TabIndex = 1;
            this.res_2.TabStop = false;
            this.res_2.Text = "Tect_2";
            this.res_2.Visible = false;
            // 
            // lblRes2Ans
            // 
            this.lblRes2Ans.AutoSize = true;
            this.lblRes2Ans.Location = new System.Drawing.Point(16, 59);
            this.lblRes2Ans.Name = "lblRes2Ans";
            this.lblRes2Ans.Size = new System.Drawing.Size(48, 13);
            this.lblRes2Ans.TabIndex = 1;
            this.lblRes2Ans.Text = "Ответы:";
            // 
            // lblRes2Subj
            // 
            this.lblRes2Subj.AutoSize = true;
            this.lblRes2Subj.Location = new System.Drawing.Point(16, 31);
            this.lblRes2Subj.Name = "lblRes2Subj";
            this.lblRes2Subj.Size = new System.Drawing.Size(55, 13);
            this.lblRes2Subj.TabIndex = 0;
            this.lblRes2Subj.Text = "Предмет:";
            // 
            // res_1
            // 
            this.res_1.Controls.Add(this.lblRes1Ans);
            this.res_1.Controls.Add(this.lblRes1Subj);
            this.res_1.Location = new System.Drawing.Point(6, 19);
            this.res_1.Name = "res_1";
            this.res_1.Size = new System.Drawing.Size(459, 100);
            this.res_1.TabIndex = 0;
            this.res_1.TabStop = false;
            this.res_1.Text = "Tect_1";
            this.res_1.Visible = false;
            // 
            // lblRes1Ans
            // 
            this.lblRes1Ans.AutoSize = true;
            this.lblRes1Ans.Location = new System.Drawing.Point(16, 58);
            this.lblRes1Ans.Name = "lblRes1Ans";
            this.lblRes1Ans.Size = new System.Drawing.Size(48, 13);
            this.lblRes1Ans.TabIndex = 1;
            this.lblRes1Ans.Text = "Ответы:";
            // 
            // lblRes1Subj
            // 
            this.lblRes1Subj.AutoSize = true;
            this.lblRes1Subj.Location = new System.Drawing.Point(16, 27);
            this.lblRes1Subj.Name = "lblRes1Subj";
            this.lblRes1Subj.Size = new System.Drawing.Size(55, 13);
            this.lblRes1Subj.TabIndex = 0;
            this.lblRes1Subj.Text = "Предмет:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 472);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(655, 446);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Поиск результата";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(655, 446);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Зарегестрировано";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(505, 420);
            this.dataGridView1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(539, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(655, 446);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Средний балл";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(566, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(14, 13);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(537, 422);
            this.dataGridView2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 494);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.res_3.ResumeLayout(false);
            this.res_3.PerformLayout();
            this.res_2.ResumeLayout(false);
            this.res_2.PerformLayout();
            this.res_1.ResumeLayout(false);
            this.res_1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbPassNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStudName;
        private System.Windows.Forms.Label lblLocality;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox res_3;
        private System.Windows.Forms.GroupBox res_2;
        private System.Windows.Forms.GroupBox res_1;
        private System.Windows.Forms.Label lblRes1Subj;
        private System.Windows.Forms.Label lblRes3Subj;
        private System.Windows.Forms.Label lblRes2Ans;
        private System.Windows.Forms.Label lblRes2Subj;
        private System.Windows.Forms.Label lblRes1Ans;
        private System.Windows.Forms.Label lblRes3Ans;
        private System.Windows.Forms.Label lblTimeSearch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}

