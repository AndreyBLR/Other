using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MvcApp.Models.DataAccess
{
    public class DataAccessManager
    {
        private SqlConnection _connection;
        public DataAccessManager(String connectionStr)
        {
            _connection = new SqlConnection(connectionStr);
        }

        public List<Employee> ReadEmployees()
        {
            List<Employee> result = null;
            _connection.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = "SELECT * FROM Employees";
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    result = new List<Employee>();
                    while (reader.Read())
                    {
                        result.Add(new Employee()
                        {
                            Id = (Guid)reader["Id"],
                            Name = (string)reader["Name"],
                            Birthday = (string)reader["Birthday"],
                            DayOfEmployment = (string)reader["DayOfEmployment"]
                        });
                    }
                }
                reader.Close();
            }
            _connection.Close();
            return result;
        }
        public void AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _connection.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@Id", employee.Id));
                cmd.Parameters.Add(new SqlParameter("@Name", employee.Name));
                cmd.Parameters.Add(new SqlParameter("@Birthday", employee.Birthday));
                cmd.Parameters.Add(new SqlParameter("@DayOfEmployment", employee.DayOfEmployment));
                cmd.CommandText = "INSERT INTO Employees (Id, Name, Birthday, DayOfEmployment) VALUES( @Id, @Name, @Birthday, @DayOfEmployment)";
                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }
        public void UpdateEmployee(Employee emp)
        {
            _connection.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@Id", emp.Id));
                cmd.Parameters.Add(new SqlParameter("@Name", emp.Name));
                cmd.Parameters.Add(new SqlParameter("@Birthday", emp.Birthday));
                cmd.Parameters.Add(new SqlParameter("@DayOfEmployment", emp.DayOfEmployment));
                cmd.CommandText = "UPDATE employees SET Name=@Name, Birthday=@Birthday, DayOfEmployment=@DayOfEmployment WHERE Id = @Id";
                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void DeleteEmployee(Guid id)
        {
            _connection.Open();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = _connection;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.CommandText = "DELETE FROM employees WHERE Id = @Id";
                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}