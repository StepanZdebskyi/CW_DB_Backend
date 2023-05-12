using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class SpecialitiesDataConnection
    {
        private string _connectionString;
        private List<SpecialityModel> allSpecs;

        public SpecialitiesDataConnection(string connStr)
        {
            _connectionString = connStr;
            allSpecs = new List<SpecialityModel>();
        }

        public List<SpecialityModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select specID, specName from Specialities;";
                    allSpecs.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allSpecs.Add(new SpecialityModel()
                        {
                            SpecID = Convert.ToInt32(reader[0].ToString()),
                            SpecName = reader[1].ToString()
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return allSpecs;
            }
        }

        public string InsertCommand(SpecialityModel spec)
        {
            SpecialityModel newSpec = spec;
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into Specialities (specID, specName) values " +
                        "(" + spec.SpecID + "," +
                        "'" + spec.SpecName +"'" + ");";

                    SqlCommand command1 = new SqlCommand(MyCommand1, _connection);

                    command1.ExecuteNonQuery();

                    _connection.Close();
                    RequestAnswer = "New record added successfully!";
                }
                catch (Exception ex)
                {
                    RequestAnswer = ex.Message;
                }
                return RequestAnswer;
            }
        }

        public string UpdateCommand(SpecialityModel spec)
        {
            SpecialityModel editedSpec = spec;
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "update Specialities set " +
                        "specName = " + "'" + spec.SpecName.ToString() + "' " +
                        "where specID = " + spec.SpecID.ToString() + ";";

                    SqlCommand command1 = new SqlCommand(MyCommand1, _connection);

                    command1.ExecuteNonQuery();

                    _connection.Close();
                    RequestAnswer = "Record updated successfully!";
                }
                catch (Exception ex)
                {
                    RequestAnswer = ex.Message;
                }
            }
            return RequestAnswer;
        }

        public string DeleteCommand(SpecialityModel spec)
        {
            SpecialityModel editedSpec = spec;
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand0 = "select doctorID from Doctors where specID = " + spec.SpecID.ToString() + ";";

                    //Спочатку потрібно видалити всіх лікарів які пов'язані із цією спеціальністю 
                    SqlCommand command = new SqlCommand(MyCommand0, _connection);
                    List<int> doctorsIDs = new List<int>();

                    SqlDataReader reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        doctorsIDs.Add(Convert.ToInt32(reader1[0].ToString()));
                    }
                    reader1.Close();

                    //Викликаємо модуль зв'язку із таблицею Doctors для видалення всіх елементів 
                    DoctorsDataConnection doctorConn = new DoctorsDataConnection(_connectionString);
                    foreach (var doc in doctorsIDs)
                    {
                        doctorConn.DeleteCommand(new DoctorModel() { DoctorID = doc}); 
                    }

                    string MyCommand1 = "delete from Specialities where specID = " + spec.SpecID.ToString() + ";";

                    SqlCommand command1 = new SqlCommand(MyCommand1, _connection);

                    command1.ExecuteNonQuery();

                    _connection.Close();
                    RequestAnswer = "Record and related data deleted successfully!";
                }
                catch (Exception ex)
                {
                    RequestAnswer = ex.Message;
                }
            }
            return RequestAnswer;
        }
    }
}
