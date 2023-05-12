using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class DoctorsDataConnection
    {
        private string _connectionString;
        private List<DoctorModel> allDoctors;

        public DoctorsDataConnection(string connStr)
        {
            _connectionString = connStr;
            allDoctors = new List<DoctorModel>();
        }

        public List<DoctorModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select doctorID, doctorName, doctorSurname, specID from Doctors;";
                    allDoctors.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allDoctors.Add(new DoctorModel()
                        {
                            DoctorID = Convert.ToInt32(reader[0].ToString()),
                            DoctorName = reader[1].ToString(),
                            DoctorSurname = reader[2].ToString(),
                            SpecID = Convert.ToInt32(reader[3].ToString())
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return allDoctors;
            }
        }

        public string InsertCommand(DoctorModel doc)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into Doctors (doctorID, doctorName, doctorSurname, specID) values " +
                        "(" + doc.DoctorID + "," +
                        "'" + doc.DoctorName + "', " +
                        "'" + doc.DoctorSurname + "', " + 
                        doc.SpecID + ");";

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

        public string UpdateCommand(DoctorModel doc)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "update Doctors set " +
                        "doctorName = " + "'" + doc.DoctorName.ToString() + "'," +
                        "doctorSurname = " + "'" + doc.DoctorSurname.ToString() + "'," +
                        "specID = " + doc.SpecID.ToString() + " " +
                        "where doctorID = " + doc.DoctorID.ToString() + ";";

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

        public string DeleteCommand(DoctorModel doc)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from PatientAttendingDoctors where doctorID = " + doc.DoctorID.ToString() + ";";
                    string MyCommand2 = "delete from DoctorsInfo where doctorID =" + doc.DoctorID.ToString() + ";";
                    string MyCommand3 = "delete from DoctorsShedule where doctorID = " + doc.DoctorID.ToString() + ";";
                    string MyCommand4 = "delete from VacantionsShedule where doctorID = " + doc.DoctorID.ToString() + ";";
                    string MyCommand5 = "delete from MedicalRecords where doctorID = " + doc.DoctorID.ToString() + ";";
                    string MyCommand6 = "delete from Doctors where doctorID = " + doc.DoctorID.ToString() + ";";

                    SqlCommand command1 = new SqlCommand(MyCommand1, _connection);
                    SqlCommand command2 = new SqlCommand(MyCommand2, _connection);
                    SqlCommand command3 = new SqlCommand(MyCommand3, _connection);
                    SqlCommand command4 = new SqlCommand(MyCommand4, _connection);
                    SqlCommand command5 = new SqlCommand(MyCommand5, _connection);
                    SqlCommand command6 = new SqlCommand(MyCommand6, _connection);

                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    command4.ExecuteNonQuery();
                    command5.ExecuteNonQuery();
                    command6.ExecuteNonQuery();

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
