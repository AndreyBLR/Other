using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Models
{
    public enum InstType {Film, Food, Club}

    public enum DataReadEnum{Quisines, FoodInstanceTypes}

    public class Repository
    {
        private String _connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Documents and Settings\Andrew\мои документы\visual studio 2010\Projects\DBKurs\DBKurs\Database.mdf;Integrated Security=True;User Instance=True";

        private SqlConnection _connection;

        public Repository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public void Open()
        {
            if(_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        //Чтение ID типа заведения
        private Int32 ReadIdInstanceType(InstType type)
        {
            Int32 result = -1;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Add(new SqlParameter("@NameType", type.ToString()));
                cmd.Connection = _connection;
                cmd.CommandText = "SELECT IDType FROM Types WHERE NameType = @NameType";
                result = (Int32)cmd.ExecuteScalar();
            }
            return result;
        }
        //Чтение по ID адреса заведения
        private Adress ReadAdressInstance(Int32 idAdress)
        {
            Adress result = new Adress();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IDAdress", idAdress));
                cmd.CommandText = "SELECT Name From Streets WHERE IDStreet = (SELECT IDStreet FROM Adress WHERE IDAdress = @IDAdress)";
                result.Street = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "SELECT Home FROM Adress WHERE IDAdress = @IDAdress";
                result.Home = (Int32)cmd.ExecuteScalar();
            }
            return result;
        }
        //Чтение по ID рейтинга заведения
        private Rating ReadRatingInstance(Int32 idRating)
        {
            Rating result = new Rating();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IDRating", idRating));
                cmd.CommandText = "SELECT PRating, CRating FROM Ratings WHERE IDRating = @IDRating";
                var dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        result.Personal = dataReader.GetInt32(0);
                        result.Comfort = dataReader.GetInt32(1);
                    }
                }
                dataReader.Close();
            }
            return result;
        }
        //Чтение всех заведений данного типа
        public List<Instance> ReadInstances(InstType type)
        {
            List<Instance> result = new List<Instance>();
            List<Int32> idInstances = new List<Int32>();
            Instance instance = new Instance();
            Int32 idType = ReadIdInstanceType(type);
            Int32 idAdress = -1;
            Int32 idRating = -1;
            Int32 idPayment = -1;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IDType", idType));
                cmd.CommandText = "SELECT IDInstance FROM InstanceTypes WHERE IDType = @IDType";
                var dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        idInstances.Add(dataReader.GetInt32(0));
                    }
                }
                dataReader.Close();

                for (int i = 0; i < idInstances.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@IDInstance", idInstances[i]));
                    cmd.CommandText = "SELECT * FROM Instances WHERE IDInstance = @IDInstance";
                    dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            instance.Phone = dataReader.GetString(0);
                            instance.Description = dataReader.GetString(1);
                            instance.WorkingDay = dataReader.GetString(2);
                            instance.Name = dataReader.GetString(7);
                            idAdress = dataReader.GetInt32(4);
                            idRating = dataReader.GetInt32(6);
                            idPayment = dataReader.GetInt32(5);
                        }
                    }
                    dataReader.Close();
                    instance.Adress = ReadAdressInstance(idAdress);
                    instance.Rating = ReadRatingInstance(idRating);
                    cmd.Parameters.Add(new SqlParameter("@IDPayment", idPayment));
                    cmd.CommandText = "SELECT Name FROM PaymentTypes WHERE IDPaymentType = @IDPayment";
                    instance.Payment = cmd.ExecuteScalar().ToString();
                    cmd.CommandText = "SELECT DiscontCart FROM PaymentTypes WHERE IDPaymentType = @IDPayment";
                    instance.DiscontCart = (Boolean)cmd.ExecuteScalar();
                    instance.Votes = ReadVotes(idInstances[i]);
                    switch (type)
                    {
                        case InstType.Film:
                            instance.Type = ReadMoviesInstance(idInstances[i]);
                            break;
                        case InstType.Club:
                            instance.Type = ReadPartiesInstance(idInstances[i]);
                            break;
                        case InstType.Food:
                            instance.Type = ReadFoodInstance(idInstances[i]);
                            break;
                    }

                    result.Add(instance);
                    instance = new Instance();
                }
            }
            return result;
        }
        //Чтение всех фильмов в прокате
        public List<Film> ReadMovies()
        {
            List<Film> result = new List<Film>();
            List<Int32> idMovies = new List<Int32>();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = "SELECT IDMovie FROM OnScreenMovies";
                var dataReader = cmd.ExecuteReader();
                if(dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        idMovies.Add(dataReader.GetInt32(0));
                    }
                }
                dataReader.Close();
                for(int i = 0; i < idMovies.Count; i++)
                {
                    result.Add(ReadMovie(idMovies[i]));
                }
            }
            return result;
        }
        //Чтение данных фильма по названию
        public Film ReadMovie(String name)
        {
            Film result = new Film();
            Int32 idMovie = -1;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.CommandText = "SELECT IDMovie FROM OnScreenMovies WHERE Name = @Name";
                idMovie = (Int32) cmd.ExecuteScalar();
                result = ReadMovie(idMovie);
            }
            return result;
        }
        //Чтение данных заведения по имени и типа
        public Instance ReadInstance (String name, InstType type)
        {
            Instance result = new Instance();
            Int32 idInstance = -1;
            Int32 idAdress = -1;
            Int32 idRating = -1;
            Int32 idPayment = -1;
            using(SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.CommandText = "SELECT IDInstance FROM Instances WHERE Name = @Name";
                idInstance = (Int32)cmd.ExecuteScalar();

                cmd.Parameters.Add(new SqlParameter("@IDInstance", idInstance));
                cmd.CommandText = "SELECT * FROM Instances WHERE IDInstance = @IDInstance";
                var dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        result.Phone = dataReader.GetString(0);
                        result.Description = dataReader.GetString(1);
                        result.WorkingDay = dataReader.GetString(2);
                        result.Name = dataReader.GetString(7);
                        idAdress = dataReader.GetInt32(4);
                        idRating = dataReader.GetInt32(6);
                        idPayment = dataReader.GetInt32(5);
                    }
                }
                dataReader.Close();
                result.Adress = ReadAdressInstance(idAdress);
                result.Rating = ReadRatingInstance(idRating);
                cmd.Parameters.Add(new SqlParameter("@IDPayment", idPayment));
                cmd.CommandText = "SELECT Name FROM PaymentTypes WHERE IDPaymentType = @IDPayment";
                result.Payment = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "SELECT DiscontCart FROM PaymentTypes WHERE IDPaymentType = @IDPayment";
                result.DiscontCart = (Boolean)cmd.ExecuteScalar();
                result.Votes = ReadVotes(idInstance);
                switch (type)
                {
                    case InstType.Film:
                        result.Type = ReadMoviesInstance(idInstance);
                        break;
                    case InstType.Club:
                        result.Type = ReadPartiesInstance(idInstance);
                        break;
                    case InstType.Food:
                        result.Type = ReadFoodInstance(idInstance);
                        break;
                }
            }
            return result;
        }
        //Чтение данных фильма по ID
        private Film ReadMovie(Int32 idMovie)
        {
            Film result = new Film();
            List<int> idMovies = new List<int>();
            Int32 idGenre = -1;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IDMovie", idMovie));
                cmd.CommandText = "SELECT Name, Description, IDGenre FROM OnScreenMovies WHERE IDMovie = @IDMovie";
                var dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        result.Name = dataReader.GetString(0);
                        result.Description = dataReader.GetString(1);
                        idGenre = dataReader.GetInt32(2);
                    }
                }
                if (idGenre != -1)
                {
                    dataReader.Close();
                    cmd.Parameters.Add(new SqlParameter("@IDGenre", idGenre));
                    cmd.CommandText = "SELECT Name FROM Genres WHERE IDGenre = @IDGenre";
                    result.Genre = cmd.ExecuteScalar().ToString();
                }
            }
            return result;
        }
        //Чтение всех фильмов кинотеатра по ID
        private Movie ReadMoviesInstance(Int32 idInstance)
        {
            List<Int32> idFilms = new List<int>();
            List<String> date = new List<string>();
            Movie result = new Movie();
            

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IDInstance", idInstance));
                cmd.CommandText = "SELECT IDMovie, Date FROM Movies WHERE IDInstance = @IDInstance";
                var dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        idFilms.Add(dataReader.GetInt32(0));
                        date.Add(dataReader.GetString(1));
                    }
                }
                dataReader.Close();

                for (int i = 0; i < idFilms.Count; i++)
                {
                    var film = ReadMovie(idFilms[i]);
                    film.Date = date[i];
                    result.Movies.Add(film);
                }
            }
            return result;
        }
        //Чтение данных всех вечеринок
        public List<Party> ReadParties()
        {
            List<Party> result = new List<Party>();
            List<String> NameParties = new List<String>();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = "SELECT Name FROM Parties";
                var dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        NameParties.Add(dataReader.GetString(0));
                    }
                }
                dataReader.Close();
                for (int i = 0; i < NameParties.Count; i++)
                {
                    result.Add(ReadParty(NameParties[i]));
                }
            }
            return result;
        }
        //Чтение вечеринки по имени
        public Party ReadParty(String partyName)
        {
            Party result = new Party();
            String Dj = "";
            Int32 idStyle = -1;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@PartyName", partyName));
                cmd.CommandText = "SELECT * FROM Parties WHERE Name = @PartyName";
                var dataReader = cmd.ExecuteReader();
                if(dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        result.Name = partyName;
                        result.Description = dataReader.GetString(5);
                        result.Date = dataReader.GetString(1);
                        idStyle = dataReader.GetInt32(3);
                        Dj = dataReader.GetString(4);
                    }
                }
                dataReader.Close();
                cmd.Parameters.Add(new SqlParameter("@IDStyle", idStyle));
                cmd.CommandText = "SELECT Name From Styles WHERE IDStyle = @IDStyle";
                result.Style = cmd.ExecuteScalar().ToString();
                cmd.Parameters.Add(new SqlParameter("@Dj", Dj));
                cmd.CommandText = "SELECT * FROM Dj WHERE DJName = @Dj";
                dataReader = cmd.ExecuteReader();
                if(dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        result.Dj.Name = Dj;
                        result.Dj.Description = dataReader.GetString(2);
                        result.Dj.Rating = dataReader.GetInt32(3); 
                        idStyle = dataReader.GetInt32(1);
                    }
                }
                dataReader.Close();
                cmd.Parameters.Add(new SqlParameter("@DjStyle", idStyle));
                cmd.CommandText = "SELECT Name FROM Styles WHERE IDStyle = @DjStyle";
                result.Dj.Style = cmd.ExecuteScalar().ToString();
            }
            return result;
        }
        //Чтение всех вечеринок заведения
        private Club ReadPartiesInstance(Int32 idInstance)
        {
            List<String> nameParties = new List<String>();
            Club result = new Club();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IDInstance", idInstance));
                cmd.CommandText = "SELECT Name FROM Parties WHERE IDInstance = @IDInstance";
                var dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        nameParties.Add(dataReader.GetString(0));
                    }
                }
                dataReader.Close();

                for (int i = 0; i < nameParties.Count; i++)
                {
                    result.Parties.Add(ReadParty(nameParties[i]));
                }
            }
            return result;
        }
        //Чтение данных об заведении общепита
        private FoodInstanceType ReadFoodInstance(Int32 idInstance)
        {
            FoodInstanceType result = new FoodInstanceType();
            Int32 idFoodInstType = -1;
            Int32 idQuisine = -1;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IDInstance", idInstance));
                cmd.CommandText = "SELECT IDFoodInstanceType, IDQuisine, SeatAmount FROM FoodInstances WHERE IDInstance = @IDInstance";
                var dataReader = cmd.ExecuteReader();
                if(dataReader.HasRows)
                {
                    while(dataReader.Read())
                    {
                        idFoodInstType = dataReader.GetInt32(0);
                        idQuisine = dataReader.GetInt32(1);
                        result.SeatAmount = dataReader.GetInt32(2);
                    }
                }
                dataReader.Close();

                cmd.Parameters.Add(new SqlParameter("@idFoodInstType", idFoodInstType));
                cmd.CommandText = "SELECT Name, Category FROM FoodInstanceTypes WHERE IDFoodInstanceType = @idFoodInstType";
                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        result.Type = dataReader.GetString(0);
                        result.Category = dataReader.GetInt32(1);
                    }
                }
                dataReader.Close();

                cmd.Parameters.Add(new SqlParameter("@idQuisine", idQuisine));
                cmd.CommandText = "SELECT Name FROM Quisines WHERE IDQuisine = @idQuisine";
                result.Quisine = cmd.ExecuteScalar().ToString();
            }
            return result;
        }
        
        //Чтение отзывов по ID заведения
        private List<Vote> ReadVotes(Int32 idInstance)
        {
            List<Vote> result = new List<Vote>();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IDInstance", idInstance));
                cmd.CommandText = "SELECT Name, Vote FROM Votes WHERE IDInstance = @IDInstance";
                var dataReader = cmd.ExecuteReader();
                if(dataReader.HasRows)
                {
                    while(dataReader.Read())
                    {
                        Vote vote = new Vote();
                        vote.Name = dataReader.GetString(0);
                        vote.TextVote = dataReader.GetString(1);
                        result.Add(vote);
                    }
                }
                dataReader.Close();
            }
            return result;
        }

        public List<String> ReadOtherData(DataReadEnum type)
        {
            List<String> temp = new List<String>();
            List<String> result = new List<String>();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                switch (type)
                {
                    case DataReadEnum.FoodInstanceTypes:
                        cmd.CommandText = "SELECT Name FROM FoodInstanceTypes";
                        break;
                    case DataReadEnum.Quisines:
                        cmd.CommandText = "SELECT Name FROM Quisines";
                        break;
                }
                var dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        temp.Add(dataReader.GetString(0));
                    }
                }
                dataReader.Close();
            }
            var list = temp.Distinct();
            foreach (var item in list)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
