using App.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace App
{
    public interface ICompanyRepository
    {
        Company GetById(int id);
    }

    public class CompanyRepository : ICompanyRepository
    {
        public Company GetById(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Database.Db_Name].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = Database.Usp_GetCompanyById
                };

                var parameter = new SqlParameter(Database.Col_CompanyId, SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();

                var company = new Company
                {
                    Id = int.Parse(reader[Database.CompanyId].ToString()),
                    Name = reader[Database.Name].ToString(),
                    Classification = (Classification)int.Parse(reader[Database.ClassificationId].ToString())
                };

                connection.Dispose();
                return company;
            }
        }
    }
}
