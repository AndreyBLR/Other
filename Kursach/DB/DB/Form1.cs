using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccessLibrary;
using Entities;
using System.Diagnostics;

namespace DB
{
    public partial class Form1 : Form
    {
        private DataAccess _da;
        private List<GroupBox> listGB;
        public Form1()
        {
            InitializeComponent();
            _da = new DataAccess();
            listGB = new List<GroupBox>();
            listGB.Add(res_1);
            listGB.Add(res_2);
            listGB.Add(res_3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbPassNumber.Text != "")
            {
                Stopwatch sw = new Stopwatch();

                res_1.Visible = false;
                res_2.Visible = false;
                res_3.Visible = false;
                lblStudName.Text = "ФИО:";
                lblLocality.Text = "Прописка:";
                lblTimeSearch.Text = "";
                int passNumber = int.Parse(tbPassNumber.Text);
                sw.Start();
                _da.Open();
                Student student = _da.ReadStudent(passNumber, ReadStudMode.Full);
                _da.Close();
                sw.Stop();
                if (student.Result != null && student.Result.Count > 0)
                {
                    lblStudName.Text += " " + student.Passport.FIO;
                    lblLocality.Text += " " + student.Passport.Reg.Area.Trim() + ", " +
                                        student.Passport.Reg.LocalityName.Trim();
                    for (int i = 0; i < student.Result.Count; i++)
                    {
                        listGB[i] = ShowResult(listGB[i], student.Result[i]);
                    }
                }
                else
                {
                    MessageBox.Show("Таких не найдено");
                }
                lblTimeSearch.Text += sw.ElapsedMilliseconds.ToString();
            }
          
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            
            switch (e.TabPageIndex)
            {
                case 1:
                    dataGridView1.DataSource = GetDataRegStatistics();
                    break;
                case 2:
                    dataGridView2.DataSource = GetDataAverageBallet();
                    break;
            }
        }

        public DataTable GetSchemaGridView1()
        {
            _da.Open();
            List<String> listLocType = _da.ReadOtherData(DataType.LocatyType);
            _da.Close();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Предмет"));
            foreach (String type in listLocType)
            {
                dt.Columns.Add(new DataColumn(type));
            }
            dt.Columns.Add(new DataColumn("Всего"));
            return dt;
        }

        public DataTable GetSchemaGridView2()
        {
            _da.Open();
            List<String> listLocType = _da.ReadOtherData(DataType.LocatyType);
            _da.Close();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Предмет"));
            dt.Columns.Add(new DataColumn("Средний балл"));
            return dt;
        }

        public double AverageResult (String subject, IEnumerable<Student> students)
        {
            double result = -1;
            foreach (var student in students)
            {
                result +=  Math.Round(Convert.ToDouble(student[subject].Question.Count(item => item.IsTrue == true)) / Convert.ToDouble(student[subject].Question.Count)*100);
            }
            return result/students.Count();
        }

        private GroupBox ShowResult(GroupBox groupBox, TestingResult test)
        {
            var partA = from q in test.Question where q.Part == "A" select q;
            var partB = from q in test.Question where q.Part == "B" select q;
            var collection = groupBox.Controls;
            int istrue = 0;
            groupBox.Visible = true;
            collection[1].Text = "Предмет:";
            collection[1].Text += " " + test.Subject.Trim();
            collection[0].Text = "Результат:     A: ";
            foreach (var question in partA)
            {
                if (question.IsTrue)
                {
                    collection[0].Text += " " + "+";
                    istrue++;
                }
                else
                {
                    collection[0].Text += " " + "-";
                }
            }
            collection[0].Text += "    B: ";
            foreach (var question in partB)
            {
                if (question.IsTrue)
                {
                    collection[0].Text += " " + "+";
                    istrue++;
                }
                else
                {
                    collection[0].Text += " " + "-";
                }
            }
            collection[0].Text += "     " + Math.Round(Convert.ToDouble(istrue) / Convert.ToDouble(test.Question.Count) * 100).ToString() + "%";
            return groupBox;
        }

        private DataTable GetDataRegStatistics()
        {
            DataTable dt = GetSchemaGridView1();
            _da.Open();
            List<String> listSubjects = _da.ReadOtherData(DataType.Subject);
            List<Student> listStudents = _da.ReadAllStudents(ReadStudMode.PartialWithSubject);
            List<String> listLocType = _da.ReadOtherData(DataType.LocatyType);
            _da.Close();

            foreach (var subject in listSubjects)
            {
                DataRow dr = dt.NewRow();
                dr["Предмет"] = subject.Trim();
                foreach (String type in listLocType)
                {
                    dr[type] = listStudents.Count(item => item.Passport.Reg.TypeLocality.Trim() == type.Trim() && item.Result.Any(subg => subg.Subject == subject));
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private DataTable GetDataAverageBallet()
        {
            DataTable dt = GetSchemaGridView2();
            _da.Open();
            List<Student> listStudent = _da.ReadAllStudents(ReadStudMode.Full);
            List<String> listSubjects = _da.ReadOtherData(DataType.Subject);
            _da.Close();

            dt = GetSchemaGridView2();
            foreach (var subject in listSubjects)
            {
                DataRow dr = dt.NewRow();
                dr["Предмет"] = subject.Trim();
                var listSubjStud = from st in listStudent
                                   where st.Result.Any(item => item.Subject.Trim() == subject.Trim())
                                   select st;
                dr["Средний балл"] = AverageResult(subject.Trim(), listSubjStud);
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
