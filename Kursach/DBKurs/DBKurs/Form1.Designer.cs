namespace DBKurs
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMovie = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bSearchMovieInstance = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bSearchMovie = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.rbMTheatre = new System.Windows.Forms.RadioButton();
            this.rbMovies = new System.Windows.Forms.RadioButton();
            this.tpFoodInstance = new System.Windows.Forms.TabPage();
            this.rbTypeFoodInst = new System.Windows.Forms.RadioButton();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.tpClubs = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.rbClubs = new System.Windows.Forms.RadioButton();
            this.rbParties = new System.Windows.Forms.RadioButton();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.rbQuisine = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tpMovie.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tpFoodInstance.SuspendLayout();
            this.tpClubs.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpMovie);
            this.tabControl1.Controls.Add(this.tpFoodInstance);
            this.tabControl1.Controls.Add(this.tpClubs);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(699, 536);
            this.tabControl1.TabIndex = 0;
            // 
            // tpMovie
            // 
            this.tpMovie.Controls.Add(this.groupBox1);
            this.tpMovie.Controls.Add(this.richTextBox1);
            this.tpMovie.Controls.Add(this.rbMTheatre);
            this.tpMovie.Controls.Add(this.rbMovies);
            this.tpMovie.Location = new System.Drawing.Point(4, 22);
            this.tpMovie.Name = "tpMovie";
            this.tpMovie.Padding = new System.Windows.Forms.Padding(3);
            this.tpMovie.Size = new System.Drawing.Size(691, 510);
            this.tpMovie.TabIndex = 0;
            this.tpMovie.Text = "Кино";
            this.tpMovie.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl2);
            this.groupBox1.Location = new System.Drawing.Point(531, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 143);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(6, 19);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(142, 118);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bSearchMovieInstance);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(134, 92);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Кинотеатр";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bSearchMovieInstance
            // 
            this.bSearchMovieInstance.Location = new System.Drawing.Point(30, 41);
            this.bSearchMovieInstance.Name = "bSearchMovieInstance";
            this.bSearchMovieInstance.Size = new System.Drawing.Size(75, 23);
            this.bSearchMovieInstance.TabIndex = 1;
            this.bSearchMovieInstance.Text = "Поиск";
            this.bSearchMovieInstance.UseVisualStyleBackColor = true;
            this.bSearchMovieInstance.Click += new System.EventHandler(this.SearchMovieInstanceClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 20);
            this.textBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.bSearchMovie);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(134, 92);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Фильм";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 15);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(122, 20);
            this.textBox2.TabIndex = 1;
            // 
            // bSearchMovie
            // 
            this.bSearchMovie.Location = new System.Drawing.Point(30, 41);
            this.bSearchMovie.Name = "bSearchMovie";
            this.bSearchMovie.Size = new System.Drawing.Size(75, 23);
            this.bSearchMovie.TabIndex = 0;
            this.bSearchMovie.Text = "Поиск";
            this.bSearchMovie.UseVisualStyleBackColor = true;
            this.bSearchMovie.Click += new System.EventHandler(this.SearchMovieClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(499, 498);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // rbMTheatre
            // 
            this.rbMTheatre.AutoSize = true;
            this.rbMTheatre.Location = new System.Drawing.Point(531, 28);
            this.rbMTheatre.Name = "rbMTheatre";
            this.rbMTheatre.Size = new System.Drawing.Size(86, 17);
            this.rbMTheatre.TabIndex = 2;
            this.rbMTheatre.TabStop = true;
            this.rbMTheatre.Text = "Кинотеатры";
            this.rbMTheatre.UseVisualStyleBackColor = true;
            this.rbMTheatre.CheckedChanged += new System.EventHandler(this.MTheatre_CheckedChanged);
            // 
            // rbMovies
            // 
            this.rbMovies.AutoSize = true;
            this.rbMovies.Location = new System.Drawing.Point(531, 51);
            this.rbMovies.Name = "rbMovies";
            this.rbMovies.Size = new System.Drawing.Size(60, 17);
            this.rbMovies.TabIndex = 1;
            this.rbMovies.TabStop = true;
            this.rbMovies.Text = "Афиша";
            this.rbMovies.UseVisualStyleBackColor = true;
            this.rbMovies.CheckedChanged += new System.EventHandler(this.Movies_CheckedChanged);
            // 
            // tpFoodInstance
            // 
            this.tpFoodInstance.Controls.Add(this.rbQuisine);
            this.tpFoodInstance.Controls.Add(this.rbTypeFoodInst);
            this.tpFoodInstance.Controls.Add(this.richTextBox3);
            this.tpFoodInstance.Location = new System.Drawing.Point(4, 22);
            this.tpFoodInstance.Name = "tpFoodInstance";
            this.tpFoodInstance.Padding = new System.Windows.Forms.Padding(3);
            this.tpFoodInstance.Size = new System.Drawing.Size(691, 510);
            this.tpFoodInstance.TabIndex = 1;
            this.tpFoodInstance.Text = "Общепит";
            this.tpFoodInstance.UseVisualStyleBackColor = true;
            // 
            // rbTypeFoodInst
            // 
            this.rbTypeFoodInst.AutoSize = true;
            this.rbTypeFoodInst.Location = new System.Drawing.Point(539, 42);
            this.rbTypeFoodInst.Name = "rbTypeFoodInst";
            this.rbTypeFoodInst.Size = new System.Drawing.Size(80, 17);
            this.rbTypeFoodInst.TabIndex = 6;
            this.rbTypeFoodInst.TabStop = true;
            this.rbTypeFoodInst.Text = "Заведения";
            this.rbTypeFoodInst.UseVisualStyleBackColor = true;
            this.rbTypeFoodInst.CheckedChanged += new System.EventHandler(this.TypeFoodInst_CheckedChanged);
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(6, 6);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(499, 498);
            this.richTextBox3.TabIndex = 5;
            this.richTextBox3.Text = "";
            // 
            // tpClubs
            // 
            this.tpClubs.Controls.Add(this.groupBox2);
            this.tpClubs.Controls.Add(this.rbClubs);
            this.tpClubs.Controls.Add(this.rbParties);
            this.tpClubs.Controls.Add(this.richTextBox2);
            this.tpClubs.Location = new System.Drawing.Point(4, 22);
            this.tpClubs.Name = "tpClubs";
            this.tpClubs.Size = new System.Drawing.Size(691, 510);
            this.tpClubs.TabIndex = 2;
            this.tpClubs.Text = "Клубы";
            this.tpClubs.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl3);
            this.groupBox2.Location = new System.Drawing.Point(531, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 143);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поиск";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage3);
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Location = new System.Drawing.Point(6, 19);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(142, 118);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.textBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(134, 92);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Клуб";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(30, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Поиск";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SearchClubClick);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(6, 15);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(122, 20);
            this.textBox4.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.textBox3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(134, 92);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Вечеринка";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Поиск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SearchPartyClick);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(6, 15);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(122, 20);
            this.textBox3.TabIndex = 0;
            // 
            // rbClubs
            // 
            this.rbClubs.AutoSize = true;
            this.rbClubs.Location = new System.Drawing.Point(531, 28);
            this.rbClubs.Name = "rbClubs";
            this.rbClubs.Size = new System.Drawing.Size(57, 17);
            this.rbClubs.TabIndex = 2;
            this.rbClubs.TabStop = true;
            this.rbClubs.Text = "Клубы";
            this.rbClubs.UseVisualStyleBackColor = true;
            this.rbClubs.CheckedChanged += new System.EventHandler(this.Clubs_CheckedChanged);
            // 
            // rbParties
            // 
            this.rbParties.AutoSize = true;
            this.rbParties.Location = new System.Drawing.Point(531, 51);
            this.rbParties.Name = "rbParties";
            this.rbParties.Size = new System.Drawing.Size(79, 17);
            this.rbParties.TabIndex = 1;
            this.rbParties.TabStop = true;
            this.rbParties.Text = "Вечеринки";
            this.rbParties.UseVisualStyleBackColor = true;
            this.rbParties.CheckedChanged += new System.EventHandler(this.Parties_CheckedChanged);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(6, 6);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(499, 498);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // rbQuisine
            // 
            this.rbQuisine.AutoSize = true;
            this.rbQuisine.Location = new System.Drawing.Point(539, 65);
            this.rbQuisine.Name = "rbQuisine";
            this.rbQuisine.Size = new System.Drawing.Size(54, 17);
            this.rbQuisine.TabIndex = 7;
            this.rbQuisine.TabStop = true;
            this.rbQuisine.Text = "Кухни";
            this.rbQuisine.UseVisualStyleBackColor = true;
            this.rbQuisine.CheckedChanged += new System.EventHandler(this.Quisine_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 560);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tpMovie.ResumeLayout(false);
            this.tpMovie.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tpFoodInstance.ResumeLayout(false);
            this.tpFoodInstance.PerformLayout();
            this.tpClubs.ResumeLayout(false);
            this.tpClubs.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpMovie;
        private System.Windows.Forms.TabPage tpFoodInstance;
        private System.Windows.Forms.TabPage tpClubs;
        private System.Windows.Forms.RadioButton rbMTheatre;
        private System.Windows.Forms.RadioButton rbMovies;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button bSearchMovieInstance;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bSearchMovie;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbClubs;
        private System.Windows.Forms.RadioButton rbParties;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.RadioButton rbTypeFoodInst;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RadioButton rbQuisine;
    }
}

