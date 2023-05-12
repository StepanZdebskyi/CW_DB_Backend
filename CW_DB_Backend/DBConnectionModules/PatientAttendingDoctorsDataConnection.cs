using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class PatientAttendingDoctorsDataConnection
    {
        private string _connectionString;
        private List<PatientAttendingDoctorsModel> allPatDoct;

        public PatientAttendingDoctorsDataConnection(string connStr)
        {
            _connectionString = connStr;
            allPatDoct = new List<PatientAttendingDoctorsModel>();
        }

        public List<PatientAttendingDoctorsModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select recordID, doctorID, patientID from PatientAttendingDoctors;";
                    allPatDoct.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allPatDoct.Add(new PatientAttendingDoctorsModel()
                        {
                            RecordID = Convert.ToInt32(reader[0].ToString()),
                            DoctorID = Convert.ToInt32(reader[1].ToString()),
                            PatientID = Convert.ToInt32(reader[2].ToString())
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return allPatDoct;
            }
        }

        public string InsertCommand(PatientAttendingDoctorsModel patDoct)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into PatientAttendingDoctors (recordID, doctorID, patientID) values " +
                        "(" + patDoct.RecordID + "," +
                         patDoct.DoctorID + "," +
                         patDoct.PatientID + ");";

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

        public string UpdateCommand(PatientAttendingDoctorsModel patDoct)
        { 
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "update PatientAttendingDoctors set " +
                        "doctorID = " + patDoct.DoctorID.ToString() + ", " +
                        "patientID = " + patDoct.PatientID.ToString() + " " +
                        "where recordID = " + patDoct.RecordID.ToString() + ";";

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

        public string DeleteCommand(PatientAttendingDoctorsModel doc)
        { 
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from PatientAttendingDoctors where recordID = " + doc.RecordID.ToString() + ";";

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
