using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class PatientsDataConnetion
    {
        private string _connectionString;
        private List<PatientModel> allPatients;

        public PatientsDataConnetion(string connStr)
        {
            _connectionString = connStr;
            allPatients = new List<PatientModel>();
        }

        public List<PatientModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select patientID, patientName, patientSurname, patientAge, patientGender, patientPassportNumber from Patients;";
                    allPatients.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allPatients.Add(new PatientModel()
                        {
                            PatientID = Convert.ToInt32(reader[0].ToString()),
                            PatientName = reader[1].ToString(),
                            PatientSurname = reader[2].ToString(),
                            PatientAge = Convert.ToInt32(reader[3].ToString()),
                            PatientGender = Convert.ToInt32(reader[4].ToString()),
                            PatientPassportNumber = Convert.ToInt64(reader[5].ToString())
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return allPatients;
            }
        }

        public string InsertCommand(PatientModel pat)
        {
            PatientModel newPatient = pat;
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into Patients (patientID, patientName, patientSurname, patientAge, patientGender, patientPassportNumber) values " +
                        "(" + pat.PatientID + "," +
                        "'" + pat.PatientName + "', " +
                        "'" + pat.PatientSurname + "', " +
                        pat.PatientAge + ", " + 
                        pat.PatientGender + ", " + 
                        pat.PatientPassportNumber + ");";

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

        public string UpdateCommand(PatientModel pat)
        {
            PatientModel editedPatient = pat;
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "update Patients set " +
                        "patientName = " + "'" + pat.PatientName.ToString() + "'," +
                        "patientSurname = " + "'" + pat.PatientSurname.ToString() + "'," +
                        "patientAge = " + pat.PatientAge.ToString() + "," + 
                        "patientGender = " + pat.PatientGender.ToString() + "," + 
                        "patientPassportNumber = " + pat.PatientPassportNumber.ToString() + " " +
                        "where patientID = " + pat.PatientID.ToString() + ";";

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

        public string DeleteCommand(PatientModel pat)
        {
            PatientModel editedPatient = pat;
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from PatientAttendingDoctors where patientID = " + pat.PatientID.ToString() + ";";
                    string MyCommand2 = "delete from MedicalRecords where patientID =" + pat.PatientID.ToString() + ";";
                    string MyCommand3 = "delete from HospitalizationRequests where patientID = " + pat.PatientID.ToString() + ";";
                    string MyCommand4 = "delete from PatientsCases where patientID = " + pat.PatientID.ToString() + ";";
                    string MyCommand5 = "delete from Patients where patientID = " + pat.PatientID.ToString() + ";";

                    SqlCommand command1 = new SqlCommand(MyCommand1, _connection);
                    SqlCommand command2 = new SqlCommand(MyCommand2, _connection);
                    SqlCommand command3 = new SqlCommand(MyCommand3, _connection);
                    SqlCommand command4 = new SqlCommand(MyCommand4, _connection);
                    SqlCommand command5 = new SqlCommand(MyCommand5, _connection);

                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    command4.ExecuteNonQuery();
                    command5.ExecuteNonQuery();

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
