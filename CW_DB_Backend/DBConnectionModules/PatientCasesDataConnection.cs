using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class PatientCasesDataConnection
    {
        private string _connectionString;
        private List<PatientCaseModel> allCases;

        public PatientCasesDataConnection(string connStr)
        {
            _connectionString = connStr;
            allCases = new List<PatientCaseModel>();
        }

        public List<PatientCaseModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select patientID, caseID, isCaseClosed, openingDate" +
                        ", illnessDescription, conclusionNotes, remarks from PatientsCases;";
                    allCases.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allCases.Add(new PatientCaseModel()
                        {
                            PatientID = Convert.ToInt32(reader[0].ToString()),
                            CaseID = Convert.ToInt32(reader[1].ToString()),
                            IsCaseClosed = Convert.ToBoolean(reader[2].ToString()),
                            CaseOpeningDate = Convert.ToDateTime(reader[3].ToString()),
                            IllnessDescription = reader[4].ToString(),
                            ConclusionsNotes = reader[5].ToString(),
                            Remarks = reader[6].ToString()
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return allCases;
            }
        }

        public string InsertCommand(PatientCaseModel patCase)
        { 
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into PatientsCases (patientID, caseID, isCaseClosed, openingDate, " +
                        "illnessDescription, conclusionNotes, remarks) values " +
                        "(" + patCase.PatientID.ToString() + "," +
                        patCase.CaseID.ToString() + "," +
                        "'" + patCase.IsCaseClosed.ToString() + "', " +
                        "'" + patCase.CaseOpeningDate.ToString("yyyy-MM-dd") + "', " +
                        "'" + patCase.IllnessDescription.ToString() + "', " +
                        "'" + patCase.ConclusionsNotes.ToString() + "', " +
                        "'" + patCase.Remarks.ToString() + "' " + ");";

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

        public string UpdateCommand(PatientCaseModel patCase)
        { 
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
    
                    string MyCommand1 = "update PatientsCases set " +
                        "patientID = " + patCase.PatientID.ToString() + "," +
                        "isCaseClosed = " + "'" + patCase.IsCaseClosed.ToString() + "'," +
                        "openingDate = " + "'" + patCase.CaseOpeningDate.ToString("yyyy-MM-dd") + "', " +
                        "illnessDescription = " + "'" + patCase.IllnessDescription.ToString() + "', " +
                        "conclusionNotes = " + "'" + patCase.ConclusionsNotes.ToString() + "', " +
                        "remarks = " + "'" + patCase.Remarks.ToString() + "' " +
                        "where caseID = " + patCase.CaseID.ToString() + ";";

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

        public string DeleteCommand(PatientCaseModel patCase)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from PatientsCases where caseID = " + patCase.CaseID.ToString() + ";";
                   
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
