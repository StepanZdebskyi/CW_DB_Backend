using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class DoctorsSheduleDataConnection
    {
        private string _connectionString;
        private List<DoctorsSheduleModel> shedule;

        public DoctorsSheduleDataConnection(string connStr)
        {
            _connectionString = connStr;
            shedule = new List<DoctorsSheduleModel>();
        }

        public List<DoctorsSheduleModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select sheduleItemID, doctorID, workDate, startTime, finishTime from DoctorsShedule;";
                    shedule.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        shedule.Add(new DoctorsSheduleModel()
                        {
                            SheduleItemID = Convert.ToInt32(reader[0].ToString()),
                            DoctorID = Convert.ToInt32(reader[1].ToString()),
                            WorkDate = Convert.ToDateTime(reader[2].ToString()),
                            StartWorkTime = Convert.ToDateTime(reader[3].ToString()),
                            FinishWorkTime = Convert.ToDateTime(reader[4].ToString()),
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return shedule;
            }
        }

        public string InsertCommand(DoctorsSheduleModel record)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into DoctorsShedule (sheduleItemID, doctorID, workDate, startTime, finishTime) values " +
                        "(" + record.SheduleItemID+ "," +
                        record.DoctorID + "," +
                        "'" + record.WorkDate.ToString("yyyy-MM-dd") + "', " +
                        "'" + record.StartWorkTime.ToString("HH:mm:ss") + "', " +
                         "'" + record.FinishWorkTime.ToString("HH:mm:ss") + "'" + ");";

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

        public string UpdateCommand(DoctorsSheduleModel record)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                 
                    string MyCommand1 = "update DoctorsShedule set " +
                        "doctorID =" + record.DoctorID.ToString() + ", " +
                        "workDate = " + "'" + record.WorkDate.ToString("yyyy-MM-dd") + "', " +
                        "startTime = " + "'" + record.StartWorkTime.ToString("HH:mm:ss") + "', " +
                        "finishTime = " + "'" + record.FinishWorkTime.ToString("HH:mm:ss") + "' " + 
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

        public string DeleteCommand(DoctorsSheduleModel record)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from DoctorsShedule " +
                       "where sheduleItemID = " + record.SheduleItemID.ToString() + ";";

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
