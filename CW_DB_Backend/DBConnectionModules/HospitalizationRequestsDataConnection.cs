using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class HospitalizationRequestsDataConnection
    {
        private string _connectionString;
        private List<HospitalizationRequestModel> allRequests;

        public HospitalizationRequestsDataConnection(string connStr)
        {
            _connectionString = connStr;
            allRequests = new List<HospitalizationRequestModel>();
        }

        public List<HospitalizationRequestModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select requestID, patientID, healthComplaints, clinicName, " +
                        "requestDate, isRequestProcessed from HospitalizationRequests;";
                    allRequests.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allRequests.Add(new HospitalizationRequestModel()
                        {
                            RequestID = Convert.ToInt32(reader[0].ToString()),
                            PatientID = Convert.ToInt32(reader[1].ToString()),
                            HealthComplaints = reader[2].ToString(),
                            ClinicName = reader[3].ToString(),
                            RequestDate = Convert.ToDateTime(reader[4].ToString()),
                            IsRequestProcessed = Convert.ToBoolean(reader[5].ToString())
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return allRequests;
            }
        }

        public string InsertCommand(HospitalizationRequestModel doc)
        { 
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into HospitalizationRequests (requestID, patientID, healthComplaints, clinicName, " +
                        "requestDate, isRequestProcessed) values " +
                        "(" + doc.RequestID + "," +
                              doc.PatientID + "," +
                        "'" + doc.HealthComplaints + "', " +
                        "'" + doc.ClinicName + "', " +
                        "'" + doc.RequestDate.ToString("yyyy-MM-dd") + "', " +
                        "'" + doc.IsRequestProcessed.ToString() + "'" + ");";

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

        public string UpdateCommand(HospitalizationRequestModel doc)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "update HospitalizationRequests set " +
                        "patientID = " + doc.PatientID.ToString() + "," +
                        "healthComplaints = " + "'" + doc.HealthComplaints.ToString() + "'," +
                        "clinicName = " + "'" + doc.ClinicName.ToString() + "'," +
                        "requestDate = " + "'" + doc.RequestDate.ToString("yyyy-MM-dd") + "'," +
                        "isRequestProcessed = " + "'" + doc.IsRequestProcessed.ToString() + "'" +
                        "where requestID = " + doc.RequestID.ToString() + ";";

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

        public string DeleteCommand(HospitalizationRequestModel doc)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from HospitalizationRequests where requestID = " + doc.RequestID.ToString() + ";";
                    
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
