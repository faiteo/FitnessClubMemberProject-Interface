using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace FitnessClubMemberProject_Interface.DBLayer
{
    public class MemberDB : IMember
    {

        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\users\faithnielsen\documents\visual studio 2013\Projects\FitnessClubMemberProject-Interface\FitnessClubMemberProject-Interface\FitnessClubMemberDB.mdf;Integrated Security=True";
           
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        
        //Insert a new Member into the Database
        public int AddNewMember(string fn, string ln, string adr, string city, string phnr, string mail)
        {
            int returnAffectedRow;
            
            SqlConnection con = GetConnection();
            string queryStatement = "INSERT INTO Member(firstname, lastname, address, city, phone, email) VALUES (@fn, @ln, @adr, @city, @phnr, @mail)";
            SqlCommand sqlCmd = new SqlCommand(queryStatement, con);
            sqlCmd.Parameters.AddWithValue("@fn", fn);
            sqlCmd.Parameters.AddWithValue("@ln", ln);
            sqlCmd.Parameters.AddWithValue("@adr", adr);
            sqlCmd.Parameters.AddWithValue("@city", city);
            sqlCmd.Parameters.AddWithValue("@phnr", phnr);
            sqlCmd.Parameters.AddWithValue("@mail", mail);

            try
            {
               con.Open();
               returnAffectedRow = sqlCmd.ExecuteNonQuery();
    
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
            return returnAffectedRow;
        }

        //Get All Members fromm Database
        public List<Member> GetAllMembers()
        {
            List<Member> memberListToReturn = new List<Member>();
            SqlConnection conn = GetConnection();
            string queryStatment = "SELECT * FROM Member";
            SqlCommand sqlCmd = new SqlCommand(queryStatment, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    Member memberObj = new Member();
                    memberObj.MemberID = Convert.ToInt32(reader[0].ToString());
                    memberObj.FirstName = reader[1].ToString();
                    memberObj.LastName = reader[2].ToString();
                    memberObj.Address = reader[3].ToString();
                    memberObj.City = reader[4].ToString();
                    memberObj.Phone = reader[5].ToString();
                    memberObj.Email = reader[6].ToString();

                    memberListToReturn.Add(memberObj);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return memberListToReturn;
        }

        //Find a Member by a given ID
        public Member FindMemberByID(int id)
        { 
            SqlConnection conn = GetConnection();
            string queryStatment = "SELECT firstname,lastname,address, city, phone, email FROM Member WHERE Id = @id";
            Member member = new Member();
            SqlCommand sqlCmd = new SqlCommand(queryStatment, conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = id;
            sqlCmd.Parameters.Add(param);
            try
            {
                conn.Open();
                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    member.FirstName = reader[0].ToString();
                    member.LastName = reader[1].ToString();
                    member.Address = reader[2].ToString();
                    member.City = reader[3].ToString();
                    member.Phone = reader[4].ToString();
                    member.Email = reader[5].ToString();
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            finally
            {
                conn.Close();
            }
            return member;
        }

        //Delete a Member with a given ID. TODO
        public int DeleteMember(int id)
        {
            throw new NotImplementedException();
        }


    }
}
