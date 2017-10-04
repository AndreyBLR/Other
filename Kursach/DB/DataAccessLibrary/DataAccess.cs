using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Linq;
using System.Data.Common;
using Entities;


namespace DataAccessLibrary
{
    public enum ReadStudMode{Full, Partial, PartialWithSubject}

    public enum DataType{InstType, Subject, LocatyType}
    public class DataAccess
    {
        private SqlConnection _connection;
        String connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Documents and Settings\Andrew\Мои документы\Visual Studio 2010\Projects\DB\DataAccessLibrary\DataBase.mdf;Integrated Security=True;User Instance=True";
            
        public DataAccess()
        {
            _connection = new SqlConnection(connectionString);   
        }
        //Создание подключения для текущего объекта
        public void Open()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        //Закрытие открытого подключения для текущего объекта 
        public void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
       
        public List<String> ReadOtherData(DataType type)
        {
            List<String> result = new List<string>();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                switch (type)
                {
                    case DataType.InstType:
                        cmd.CommandText = "SELECT Name_type FROM Institution_Types";
                        break;
                    case DataType.LocatyType:
                        cmd.CommandText = "SELECT Name_type FROM Locality_Types";
                        break;
                    case DataType.Subject:
                        cmd.CommandText = "SELECT Name_subject FROM Subjects";
                        break;
                }
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            return result;
        }
        //Получает данные об всех студентах в зависимость от режима
        public List<Student> ReadAllStudents(ReadStudMode mode)
        {
            List<Student> result = new List<Student>();
            List<int> passNumbers = new List<int>();

            using(SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = "SELECT Passport_number FROM PassportData";
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        passNumbers.Add(reader.GetInt32(0));
                    }
                }
                reader.Close();

                if (passNumbers.Count > 0)
                {
                    foreach (int number in passNumbers)
                    {
                        result.Add(ReadStudent(number, mode));
                    }
                }
            }
            return result;
        }
        //По номеру пасспорта получает все данные по студенту
        public Student ReadStudent(int passNumber, ReadStudMode mode)
        {
            Student result = new Student();
            int idInstitutionType = -1;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@passNumber", passNumber));
                cmd.CommandText = "SELECT Phone, FK_institution_type FROM Student WHERE FK_passport_number = @passNumber";
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.PhoneNumber = reader.GetString(0);
                        idInstitutionType = reader.GetInt32(1);
                    }
                }
                reader.Close();

                if (idInstitutionType != -1)
                {
                    cmd.Parameters.Add(new SqlParameter("@idInstitutionType", idInstitutionType));
                    cmd.CommandText = "SELECT Name_type FROM Institution_Types WHERE ID_institution_type = @idInstitutionType";
                    result.InstitutionType = cmd.ExecuteScalar().ToString();
                    result.Passport = ReadPassportData(passNumber);
                    switch (mode)
                    {
                        case ReadStudMode.Full:
                            result.Result = ReadTestingResult(passNumber, true);
                            break;
                        case ReadStudMode.Partial:
                            result.Result = null;
                            break;
                        case ReadStudMode.PartialWithSubject:
                            result.Result = ReadTestingResult(passNumber, false);
                            break;
                    }
                }
            }
            return result;
        }
        //По номеру пасспорта получает пасспортные данные
        public PassportData ReadPassportData(int passNumber)
        {
            PassportData result = new PassportData();
            Registration reg = new Registration();
            int idRegistration = -1;
            int idArea=-1;
            int idLocalityType = -1;
            int idLocalityName = -1;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@passNumber", passNumber));
                cmd.CommandText = "SELECT FK_registration, FIO FROM PassportData WHERE Passport_number = @passNumber";
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.FIO = reader.GetString(1);
                        idRegistration = reader.GetInt32(0);
                    }
                }
                reader.Close();

                if (idRegistration != -1)
                {
                    cmd.Parameters.Add(new SqlParameter("@idReg", idRegistration));
                    cmd.CommandText = "SELECT FK_area, FK_locality_type, FK_locality_name FROM Registrations WHERE ID_registration = @idReg";
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idArea = reader.GetInt32(0);
                            idLocalityType = reader.GetInt32(1);
                            idLocalityName = reader.GetInt32(2);
                        }
                    }
                    reader.Close();

                    if (idArea != -1 && idLocalityName != -1 && idLocalityType != -1)
                    {
                        cmd.Parameters.Add(new SqlParameter("@idArea", idArea));
                        cmd.Parameters.Add(new SqlParameter("@idLocalityType", idLocalityType));
                        cmd.Parameters.Add(new SqlParameter("@idLocalityName", idLocalityName));
                        cmd.CommandText = "SELECT Name_area FROM Areas WHERE ID_area = @idArea";
                        reg.Area = cmd.ExecuteScalar().ToString();
                        cmd.CommandText = "SELECT Name_type FROM Locality_Types WHERE ID_type = @idLocalityType";
                        reg.TypeLocality = cmd.ExecuteScalar().ToString();
                        cmd.CommandText = "SELECT Name_locality FROM Locality_Names WHERE ID_locality_name = @idLocalityName";
                        reg.LocalityName = cmd.ExecuteScalar().ToString();
                        result.Reg = reg;
                    }
                }
            }
            return result;
        }
        //По номеру пасспорта получает результаты тестов по предметам
        public List<TestingResult> ReadTestingResult(int passNumber, Boolean mode)
        {
            List<TestingResult> result = new List<TestingResult>();
            List<int> idTestVersions = new List<int>();
            int idSubj = -1;
            TestingResult temp;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@passNumber", passNumber));
                cmd.CommandText = "SELECT FK_version FROM Tests WHERE FK_student = @passNumber";
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idTestVersions.Add(reader.GetInt32(0));
                    }
                    
                }
                reader.Close();

                if (idTestVersions.Count > 0)
                {
                    
                    for (int i = 0; i < idTestVersions.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        temp = new TestingResult();
                        cmd.Parameters.Add(new SqlParameter("@IdVersion", SqlDbType.Int));
                        cmd.Parameters["@IdVersion"].Value = idTestVersions[i];
                        cmd.CommandText = "SELECT Version, FK_subject FROM Tests_Versions WHERE ID_version = @IdVersion";
                        
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                temp.NumberVersion = reader.GetInt32(0);
                                idSubj = reader.GetInt32(1);
                            }
                        }
                        reader.Close();
                        cmd.Parameters.Add(new SqlParameter("@IdSubject", idSubj));
                        cmd.CommandText = "SELECT Name_subject FROM Subjects WHERE ID_subject = @IdSubject";
                        temp.Subject = (String) cmd.ExecuteScalar();
                        if (mode)
                        {
                            temp.Question = ReadQuestions(idTestVersions[i]);
                        }
                        result.Add(temp);
                    }
                }
            }
            return result;
        }
        //По ID варианта теста получает его вопросы с ответами
        public List<Question> ReadQuestions(Int32 idVersion)
        {
            List<Question> result = new List<Question>();
            Question temp = new Question();
            List<int> questionId = new List<int>();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IdVersion", idVersion));
                //Часть А
                cmd.CommandText = "SELECT ID_question, Text_question FROM Questions_Part_A WHERE FK_version = @IdVersion";
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        temp = new Question();
                        temp.Part = "A";
                        temp.Text = reader.GetString(1);
                        questionId.Add(reader.GetInt32(0));
                        result.Add(temp);
                    }
                    
                }
                reader.Close();
                if (questionId.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        result[i].Answers = ReadAnswers('A', questionId[i]);
                        result[i].StudAnswers = ReadStudAnswers('A', questionId[i]);
                    }
                }
                //Часть B
                cmd.CommandText = "SELECT ID_question, Text_question FROM Question_Part_B WHERE FK_version = @IdVersion";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        temp = new Question();
                        temp.Part = "B";
                        temp.Text = reader.GetString(1);
                        questionId.Add(reader.GetInt32(0));
                        result.Add(temp);
                    }
                }
                reader.Close();

                if (questionId.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (result[i].Part == "B")
                        {
                            result[i].Answers = ReadAnswers('B', questionId[i]);
                            result[i].StudAnswers = ReadStudAnswers('B', questionId[i]);
                        }
                    }
                }
            }
            return result;
        }
        //По ID вопроса находит ответы студента
        public List<Answer> ReadStudAnswers(Char part, Int32 idQuestion)
        {
            List<Answer> result = new List<Answer>();
            Answer temp = new Answer();

            using (SqlCommand cmd = new SqlCommand())
            {
                List<int> id = new List<int>();
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IdQuestion", idQuestion));
                if (part == 'A')
                {
                    cmd.CommandText = "SELECT FK_answer FROM Answers_Part_A WHERE FK_Question = @idQuestion";
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id.Add(reader.GetInt32(0));
                        }
                    }
                    reader.Close();
                    if (id.Count > 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                        foreach (var item in id)
                        {
                            cmd.Parameters["@ID"].Value = (Int32) item;
                            cmd.CommandText = "SELECT Text_Answer, IsTrue FROM Version_Answer_A WHERE ID_answer = @ID";
                            reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    temp = new Answer();
                                    temp.Text = (String) reader["Text_Answer"];
                                    temp.IsTrue = reader.GetBoolean(1);
                                    result.Add(temp);
                                }
                                reader.Close();
                            }
                        }
                    }
                }
                else if (part == 'B')
                {
                    cmd.CommandText = "SELECT Answer FROM Answer_Part_B WHERE FK_Question = @idQuestion";
                    temp.Text = (String) cmd.ExecuteScalar();
                    result.Add(temp);
                }
            }
            return result;
        }
        //По ID вопроса находит варианты ответов
        public List<Answer> ReadAnswers(Char part, Int32 idQuestion)
        {
            List<Answer> result = new List<Answer>();
            Answer temp = new Answer();
            
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@IdQuestion", idQuestion));
                if (part =='A')
                {
                    cmd.CommandText = "SELECT Text_Answer, IsTrue FROM Version_Answer_A WHERE FK_question = @IdQuestion";
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            temp = new Answer();
                            temp.Text = (String) reader["Text_Answer"];
                            temp.IsTrue = reader.GetBoolean(1);
                            result.Add(temp);
                        }
                    }
                    reader.Close();
                }
                else if(part =='B')
                {
                    cmd.CommandText = "SELECT Text_Answer FROM Correct_Answer_B WHERE FK_question = @IdQuestion";
                    temp.Text = (String)cmd.ExecuteScalar();
                    temp.IsTrue = true;
                    result.Add(temp);
                }
            }
            return result;
        }
    }
}
