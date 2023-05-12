using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class VacantionsSheduleDataConnection
    {
        private string _connectionString;
        private List<VacantionSheduleModel> allRecords;

        public VacantionsSheduleDataConnection(string connStr)
        {
            _connectionString = connStr;
            allRecords = new List<VacantionSheduleModel>();
        }

        public List<VacantionSheduleModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select sheduleItemID, doctorID, vacantionStartDate, vacantionEndDate from VacantionsShedule;";
                    allRecords.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allRecords.Add(new VacantionSheduleModel()
                        {
                            SheduleItemID = Convert.ToInt32(reader[0].ToString()),
                            DoctorID = Convert.ToInt32(reader[1].ToString()),
                            VacantionStartDate = Convert.ToDateTime(reader[2].ToString()),
                            VacantionEndDate = Convert.ToDateTime(reader[3].ToString())
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

        public string InsertCommand(VacantionSheduleModel doc)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into VacantionsShedule (sheduleItemID, doctorID, vacantionStartDate, vacantionEndDate) values " +
                        "(" + doc.SheduleItemID + "," +
                        doc.DoctorID + "," +
                        "'" + doc.VacantionStartDate.ToString("yyyy-MM-dd") + "', " +
                        "'" + doc.VacantionEndDate.ToString("yyyy-MM-dd") + "'" + ");";

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

        public string UpdateCommand(VacantionSheduleModel record)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    
                    string MyCommand1 = "update VacantionsShedule set " +
                        "doctorID = " + record.DoctorID.ToString() + ", " +  
                        "vacantionEndDate = " + "'" + record.VacantionEndDate.ToString("yyyy-MM-dd") + "', " +
                        "vacantionStartDate = " + "'" + record.VacantionStartDate.ToString("yyyy-MM-dd") + "' " +
                        "where sheduleItemID = " + record.SheduleItemID.ToString() + ";";

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

        public string DeleteCommand(VacantionSheduleModel doc)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from VacantionsShedule " +
                        "where sheduleItemID = " + doc.SheduleItemID.ToString() + ";";

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
