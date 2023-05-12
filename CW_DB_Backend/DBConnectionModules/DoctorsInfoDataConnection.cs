using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.Models;

namespace CW_DB_Backend.DBConnectionModules
{
    public class DoctorsInfoDataConnection
    {
        private string _connectionString;
        private List<DoctorsInfoModel> allDoctorsInfo;

        public DoctorsInfoDataConnection(string connStr)
        {
            _connectionString = connStr;
            allDoctorsInfo = new List<DoctorsInfoModel>();
        }

        public List<DoctorsInfoModel> SelectCommand()
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();
                    string MyCommand = "select doctorID, experience, graduation, salary, remarks from DoctorsInfo;";
                    allDoctorsInfo.Clear();
                    SqlCommand command = new SqlCommand(MyCommand, _connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allDoctorsInfo.Add(new DoctorsInfoModel()
                        {
                            DoctorID = Convert.ToInt32(reader[0].ToString()),
                            DoctorExperience = reader[1].ToString(),
                            DoctorGraduation = reader[2].ToString(),
                            DoctorSalary = (float)Convert.ToDouble(reader[3].ToString()),
                            DoctorRemarks = reader[4].ToString()
                        });
                    }

                    reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                return allDoctorsInfo;
            }
        }

        public string InsertCommand(DoctorsInfoModel info)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "insert into DoctorsInfo (doctorID, experience, graduation, salary, remarks) values " +
                        "(" + info.DoctorID + "," +
                        "'" + info.DoctorExperience + "', " +
                        "'" + info.DoctorGraduation + "', " +
                        info.DoctorSalary + ", " +
                        "'" + info.DoctorRemarks + "'" +");";

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

        public string UpdateCommand(DoctorsInfoModel info)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "update DoctorsInfo set " +
                        "experience = " + "'" + info.DoctorExperience.ToString() + "'," +
                        "graduation = " + "'" + info.DoctorGraduation.ToString() + "'," +
                        "salary = " + info.DoctorSalary.ToString() + ", " +
                        "remarks = " +"'" + info.DoctorRemarks.ToString() + "' " +
                        "where doctorID = " + info.DoctorID.ToString() + ";";

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

        public string DeleteCommand(DoctorsInfoModel info)
        {
            string RequestAnswer = "";

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                try
                {
                    _connection.Open();

                    string MyCommand1 = "delete from DoctorsInfo where doctorID = " + info.DoctorID.ToString() + ";";

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
