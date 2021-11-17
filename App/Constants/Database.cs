using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Constants
{
    public static class Database
    {
        public const string Db_Name = "appDatabase";

        public const string Usp_GetCompanyById = "uspGetCompanyById";
        public const string Usp_AddCustomer = "uspAddCustomer";

        public const string Col_Firstname = "@Firstname";
        public const string Col_Surname = "@Surname";
        public const string Col_DateOfBirth = "@DateOfBirth";
        public const string Col_EmailAddress = "@EmailAddress";
        public const string Col_HasCreditLimit = "@HasCreditLimit";
        public const string Col_CreditLimit = "@CreditLimit";
        public const string Col_CompanyId = "@CompanyId";

        public const string CompanyId = "CompanyId";
        public const string Name = "Name";
        public const string ClassificationId = "ClassificationId";
    }
}
