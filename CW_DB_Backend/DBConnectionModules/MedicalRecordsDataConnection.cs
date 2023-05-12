using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class MedicalRecordsDataConnection
    {
        private string _connectionString;
        private List<MedicalRecordModel> allRecords;

        public MedicalRecordsDataConnection(string connStr)
        {
            _connectionString = connStr;
            allRecords = new List<MedicalRecordModel>();
        }

        public List<MedicalRecordModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select recordID, doctorID, patientID, recordDate, recordTime, recordHeader, recordBody from MedicalRecords;";
                    allRecords.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allRecords.Add(new MedicalRecordModel()
                        {
                            RecordID = Convert.ToInt32(reader[0].ToString()),
                            DoctorID = Convert.ToInt32(reader[1].ToString()),
                            PatientID = Convert.ToInt32(reader[2].ToString()),
                            RecordDate = Convert.ToDateTime(reader[3].ToString()), 
                            RecordTime = Convert.ToDateTime(reader[4].ToString()),
                            RecordHeader = reader[5].ToString(), 
                            RecordBody = reader[6].ToString()
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return allRecords;
            }
        }

        public string InsertCommand(MedicalRecordModel rec)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into MedicalRecords (recordID, doctorID, patientID, recordDate," +
                        "recordTime, recordHeader, recordBody) values " +
                        "(" + rec.RecordID + "," +
                        rec.DoctorID + "," +
                        rec.PatientID + "," +
                         "'" + rec.RecordDate.ToString("yyyy-MM-dd") + "', " +
                        "'" + rec.RecordTime.ToString("HH:mm:ss") + "', " +
                        "'" + rec.RecordHeader + "', " +
                         "'" + rec.RecordBody + "'" + ");";

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

        public string UpdateCommand(MedicalRecordModel rec)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    
                    string MyCommand1 = "update MedicalRecords set " +
                        "doctorID = " + rec.DoctorID.ToString() + ", " +
                        "patientID = " + rec.PatientID.ToString() + ", " +
                        "recordDate = '" + rec.RecordDate.ToString("yyyy-MM-dd") + "', " +
                         "recordTime = '" + rec.RecordTime.ToString("HH:mm:ss") + "', " +
                        "recordHeader = " + "'" + rec.RecordHeader.ToString() + "' ," +
                        "recordBody = " + "'" + rec.RecordBody.ToString() + "' " +
                        "where recordID = " + rec.RecordID.ToString() + ";";

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

        public string DeleteCommand(MedicalRecordModel rec)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from MedicalRecords " + "where recordID = " + rec.RecordID.ToString() + ";";

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
