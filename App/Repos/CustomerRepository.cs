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
    public static class CustomerRepository
    {
        public static void AddCustomer(Customer customer)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[Database.Db_Name].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = Database.Usp_AddCustomer
                };

                var firstNameParameter = new SqlParameter(Database.Col_Firstname, SqlDbType.VarChar, 50) { Value = customer.Firstname };
                command.Parameters.Add(firstNameParameter);
                var surnameParameter = new SqlParameter(Database.Col_Surname, SqlDbType.VarChar, 50) { Value = customer.Surname };
                command.Parameters.Add(surnameParameter);
                var dateOfBirthParameter = new SqlParameter(Database.Col_DateOfBirth, SqlDbType.DateTime) { Value = customer.DateOfBirth };
                command.Parameters.Add(dateOfBirthParameter);
                var emailAddressParameter = new SqlParameter(Database.Col_EmailAddress, SqlDbType.VarChar, 50) { Value = customer.EmailAddress };
                command.Parameters.Add(emailAddressParameter);
                var hasCreditLimitParameter = new SqlParameter(Database.Col_HasCreditLimit, SqlDbType.Bit) { Value = customer.HasCreditLimit };
                command.Parameters.Add(hasCreditLimitParameter);
                var creditLimitParameter = new SqlParameter(Database.Col_CreditLimit, SqlDbType.Int) { Value = customer.CreditLimit };
                command.Parameters.Add(creditLimitParameter);
                var companyIdParameter = new SqlParameter(Database.Col_CompanyId, SqlDbType.Int) { Value = customer.Company.Id };
                command.Parameters.Add(companyIdParameter);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
