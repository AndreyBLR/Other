using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Models;

namespace DBKurs
{
    public partial class Form1 : Form
    {
        private Repository _r;
        public Form1()
        {
            Font font = new Font("Arial", 8, FontStyle.Italic);
            InitializeComponent();
            richTextBox1.Font = font;
            richTextBox2.Font = font;
            _r = new Repository();
        }

        private void MTheatre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMTheatre.Checked)
            {
                _r.Open();
                var instances = _r.ReadInstances(InstType.Film);
                _r.Close();
                ShowLinkInstances(ref richTextBox1, instances);
            }
            
        }

        private void Movies_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMovies.Checked)
            {
                _r.Open();
                var instances = _r.ReadInstances(InstType.Film);
                var movies = _r.ReadMovies();
                _r.Close();
                ShowLinkMovies(ref richTextBox1, movies);
            }
        }

        private void SearchMovieInstanceClick(object sender, EventArgs e)
        {
            RadioButtonReset();
            _r.Open();
            List<Instance> list = new List<Instance>();
            list.Add(_r.ReadInstance(textBox1.Text.Trim(), InstType.Film));
            _r.Close();
            ShowInstance(ref richTextBox1, list[0]);
        }

        private void SearchMovieClick(object sender, EventArgs e)
        {
            RadioButtonReset();
            _r.Open();
            Film film = _r.ReadMovie(textBox2.Text);
            List<Instance> list = _r.ReadInstances(InstType.Film);
            _r.Close();
            if (film.Name != "")
            {
                ShowMovie(ref richTextBox1, film, list);
            }
        }
        //Отображение линков фильмов в прокате
        private void ShowLinkMovies(ref RichTextBox r, List<Film> list)
        {
            r.Clear();
            r.Controls.Clear();
            Point p = new Point();
            p.X = 10;
            p.Y = 10;
            foreach (var film in list)
            {
                LinkLabel label = new LinkLabel();
                label.LinkClicked += LinkMovieClicked;
                label.Text = film.Name.Trim();
                label.Location = p;
                r.Controls.Add(label);
                p.Y = p.Y + 30;
            }
        }
        //Обработчик события нажатия на линк фильма
        private void LinkMovieClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RadioButtonReset();
            LinkLabel l = (LinkLabel)sender;
            _r.Open();
            ShowMovie(ref richTextBox1, _r.ReadMovie(l.Text), _r.ReadInstances(InstType.Film));
            _r.Close();
        }
        //Отображение данных об фильме
        private void ShowMovie(ref RichTextBox richTextBox, Film film, List<Instance> listInstance)
        {
            richTextBox.Clear();
            richTextBox.Controls.Clear();

            richTextBox.Text += "                                        " + film.Name.ToString().Trim() + "\n";
            richTextBox.Text += "                            Жанр:" + film.Genre.Trim() + "\n\n";
            richTextBox.Text += "Описание: " + film.Description.Trim() + "\n\n";
            richTextBox.Text += "Кинотеатры: ";
            foreach (var instance in listInstance)
            {
                Movie movie = (Movie)instance.Type;
                if ((from f in movie.Movies where f.Name.Trim() == film.Name.Trim() select f).Count() > 0)
                {
                    richTextBox.Text += instance.Name.Trim() + " ";
                }
            }
            richTextBox.Text += "\n--------------------------------------------------------------";
            richTextBox.Text += "\n\n\n";
        }
        //Отображение всех фильмов в кинотеатре
        private void ShowMoviesInstance(ref RichTextBox richTextBox, Instance instance)
        {
            richTextBox.Text += "Расписание: \n\n";
            Movie movie = (Movie) instance.Type;
            foreach (var film in movie.Movies)
            {
                richTextBox.Text += film.Name.ToString().Trim();
                richTextBox.Text += "           Жанр:" + film.Genre.Trim();
                richTextBox.Text += "           " + film.Date.Trim();
                richTextBox.Text += "\n------------------------------------------------------------------------";
                richTextBox.Text += "\n";
            }
        }


        //Отображение линков заведений
        private void ShowLinkInstances(ref RichTextBox r, List<Instance> list)
        {
            r.Clear();
            r.Controls.Clear();
            Point p = new Point();
            p.X = 10;
            p.Y = 10;
            foreach (var instance in list)
            {
                LinkLabel label = new LinkLabel();
                label.LinkClicked += LinkInstanceClicked;
                label.Text = instance.Name.Trim();
                label.Location = p;
                r.Controls.Add(label);
                p.Y = p.Y + 30;
            }
        }
        //Обработчик события нажатия на линк заведения
        private void LinkInstanceClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RadioButtonReset();
            Instance instance = new Instance();
            LinkLabel l = (LinkLabel)sender;
            _r.Open();
            if (l.Parent.Name == "richTextBox1")
            {
                instance = _r.ReadInstance(l.Text, InstType.Film);
                ShowInstance(ref richTextBox1, instance);
                ShowMoviesInstance(ref richTextBox1, instance);
                ShowVotes(ref richTextBox1, instance);
            }
            else if (l.Parent.Name == "richTextBox2")
            {
                instance = _r.ReadInstance(l.Text, InstType.Club);
                ShowInstance(ref richTextBox2, instance);
                ShowPartiesInstance(ref richTextBox2, instance);
                ShowVotes(ref richTextBox2, instance);
            }
            _r.Close();

        }
        //Отображение данных об заведении
        private void ShowInstance(ref RichTextBox richTextBox, Instance item)
        {
            richTextBox.Clear();
            richTextBox.Controls.Clear();
            richTextBox.Text += "                                        " + item.Name.ToString().Trim() + "\n";
            richTextBox.Text += "                                       (П-" + item.Rating.Personal.ToString() + " K-" + item.Rating.Comfort + " O-" + item.Rating.General + ")" + "\n\n"; 
            richTextBox.Text += "Описание: " + item.Description.Trim() + "\n\n";
            richTextBox.Text += "Адрес: " + item.Adress.Street.Trim() + ", " + item.Adress.Home + "\n\n";
            richTextBox.Text += "Время работы: " + item.WorkingDay.Trim() + "\n\n";
            richTextBox.Text += "Телефон: " + item.Phone.Trim() + "\n\n";
            richTextBox.Text += "------------------------------------------------------------------------";
            richTextBox.Text += "\n\n\n\n\n\n";
        }
        //Отображение отзывов об заведении
        private void ShowVotes (ref RichTextBox richTextBox, Instance item)
        {
            richTextBox.Text += "\n\nОтзывы:\n";
            foreach (Vote vote in item.Votes)
            {
                richTextBox.Text += "        " + vote.Name + "\n\n";
                richTextBox.Text += vote.TextVote + "\n------------\n";
            }
        }


        private void Clubs_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClubs.Checked)
            {
                _r.Open();
                List<Instance> list = _r.ReadInstances(InstType.Club);
                _r.Close();
                ShowLinkInstances(ref richTextBox2, list);
            }
        }

        private void Parties_CheckedChanged(object sender, EventArgs e)
        {
            if (rbParties.Checked)
            {
                _r.Open();
                List<Party> listParties = _r.ReadParties();
                _r.Close();
                ShowLinkParties(ref richTextBox2, listParties);
            }
        }
        //Отображение линков вечеринок
        private void ShowLinkParties(ref RichTextBox r, List<Party> list)
        {
            r.Clear();
            r.Controls.Clear();
            Point p = new Point();
            p.X = 10;
            p.Y = 10;
            foreach (var party in list)
            {
                LinkLabel label = new LinkLabel();
                label.LinkClicked += LinkPartyClicked;
                label.Text = party.Name.Trim();
                label.Location = p;
                r.Controls.Add(label);
                p.Y = p.Y + 30;
            }
        }
        //Обработчик события нажатия на линк вечеринки
        private void LinkPartyClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RadioButtonReset();
            LinkLabel l = (LinkLabel)sender;
            _r.Open();
            ShowParty(ref richTextBox2, _r.ReadParty(l.Text), _r.ReadInstances(InstType.Club));
            _r.Close();
        }
        //Отображение данных об вечеринке
        private void ShowParty(ref RichTextBox richTextBox, Party party, List<Instance> listInstance)
        {
            LinkLabel lblDj = new LinkLabel();
            richTextBox.Clear();
            richTextBox.Controls.Clear();

            richTextBox.Text += "                                        " + party.Name.Trim() + "\n";
            richTextBox.Text += "Dj " + party.Dj.Name + "\n";
            richTextBox.Text += "Описание: " + party.Description.Trim() + "\n\n";
            richTextBox.Text += "Дата проведения: " + party.Date + "\n\n";
            richTextBox.Text += "Место проведения: ";
            foreach (var instance in listInstance)
            {
                Club club = (Club)instance.Type;
                if ((from f in club.Parties where f.Name.Trim() == party.Name.Trim() select f).Count() > 0)
                {
                    richTextBox.Text += instance.Name.Trim() + " ";
                }
            }
            richTextBox.Text += "\n--------------------------------------------------------------";
            richTextBox.Text += "\n\n\n";
        }

        private void ShowPartiesInstance(ref RichTextBox richTextBox, Instance instance)
        {
            richTextBox.Text += "Расписание: \n\n";
            Club club = (Club)instance.Type;
            foreach (var party in club.Parties)
            {
                richTextBox.Text += party.Name.ToString().Trim();
                richTextBox.Text += "           Стиль:" + party.Style.Trim();
                richTextBox.Text += "              Дата:" + party.Date.Trim(); 
                richTextBox.Text += "\n------------------------------------------------------------------------";
                richTextBox.Text += "\n";
            }
        }

        private void SearchPartyClick(object sender, EventArgs e)
        {
            RadioButtonReset();
            _r.Open();
            Party party = _r.ReadParty(textBox3.Text);
            List<Instance> list = _r.ReadInstances(InstType.Club);
            _r.Close();
            if (party.Name != "")
            {
                ShowParty(ref richTextBox2, party, list);
            }
        }

        private void SearchClubClick(object sender, EventArgs e)
        {
            RadioButtonReset();
            _r.Open();
            List<Instance> list = new List<Instance>();
            list.Add(_r.ReadInstance(textBox1.Text, InstType.Club));
            _r.Close();
            ShowInstance(ref richTextBox2, list[0]);
        }




        private void TypeFoodInst_CheckedChanged(object sender, EventArgs e)
        {
            if(rbTypeFoodInst.Checked)
            {
                _r.Open();
                ShowLinkFoodInstanceTypes(ref richTextBox3, _r.ReadOtherData(DataReadEnum.FoodInstanceTypes));
                _r.Close();
            }
        }
        //Отображение линков типов заведений общепита
        private void ShowLinkFoodInstanceTypes(ref RichTextBox r, List<String> list)
        {
            r.Clear();
            r.Controls.Clear();
            Point p = new Point();
            p.X = 10;
            p.Y = 10;
            foreach (var type in list)
            {
                LinkLabel label = new LinkLabel();
                label.LinkClicked += LinkFoodInstanceClicked;
                label.Text = type.Trim();
                label.Location = p;
                r.Controls.Add(label);
                p.Y = p.Y + 30;
            }
        }

        private void ShowLinkQuisines(ref RichTextBox r, List<String> list)
        {
            r.Clear();
            r.Controls.Clear();
            Point p = new Point();
            p.X = 10;
            p.Y = 10;
            foreach (var type in list)
            {
                LinkLabel label = new LinkLabel();
                label.LinkClicked += LinkQuisineClicked;
                label.Text = type.Trim();
                label.Location = p;
                r.Controls.Add(label);
                p.Y = p.Y + 30;
            }
        }
        //Обработчик нажатия на линк типа заведения общепита
        private void LinkFoodInstanceClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RadioButtonReset();
            LinkLabel lbl = (LinkLabel) sender;
            _r.Open();
            List<Instance> list = _r.ReadInstances(InstType.Food);
            _r.Close();

            foreach (var instance in list)
            {
                FoodInstanceType item = (FoodInstanceType) instance.Type;
                if (item.Type.Trim() == lbl.Text.Trim())
                {
                    ShowInstance(ref richTextBox3, instance);
                }
            }
        }

        private void Quisine_CheckedChanged(object sender, EventArgs e)
        {
            if(rbQuisine.Checked)
            {
                _r.Open();
                List<String> list = _r.ReadOtherData(DataReadEnum.Quisines);
                _r.Close();
                ShowLinkQuisines(ref richTextBox3, list);
            }
        }

        private void LinkQuisineClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RadioButtonReset();
            Boolean flag = false;
            LinkLabel lbl = (LinkLabel)sender;
            _r.Open();
            List<Instance> list = _r.ReadInstances(InstType.Food);
            _r.Close();

            foreach (var instance in list)
            {
                FoodInstanceType item = (FoodInstanceType)instance.Type;
                if (item.Quisine.Trim() == lbl.Text.Trim())
                {
                    flag = true;
                    ShowInstance(ref richTextBox3, instance);
                }
            }
            if(!flag)
            {
                richTextBox3.Controls.Clear();
                richTextBox3.Text = "Ничего не найдено";
            }
        }

        private void RadioButtonReset()
        {
            rbMTheatre.Checked = false;
            rbMovies.Checked = false;
            rbParties.Checked = false;
            rbClubs.Checked = false;
            rbQuisine.Checked = false;
            rbTypeFoodInst.Checked = false;
        }
    }
}
