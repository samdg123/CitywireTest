using App;
using App.Constants;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTests
{
    public class Base
    {
        public static Company Company_VeryImportant { get { return new Company() { Name = Clients.VeryImportantClient }; } }

        public string Firstname_Valid { get { return "Sami"; } }
        public string Surname_Valid { get { return "Aljumily"; } }
        public string Email_Valid { get { return "sami@email.com"; } }
        public string Email_Invalid { get { return "sami.com"; } }

        public DateTime Dob_22YearsOld { get { return DateTime.Now.AddYears(-22); } }
        public DateTime Dob_20YearsOld { get { return DateTime.Now.AddYears(-20); } }

        public Mock<ICompanyRepository> MockCompanyRepo { get { return new Mock<ICompanyRepository>(); } }
        public Mock<ICustomerCreditService> MockCustomerCreditService { get { return new Mock<ICustomerCreditService>(); } }

        public string AnyString { get { return It.IsAny<string>(); } }
        public DateTime AnyDateTime { get { return It.IsAny<DateTime>(); } }
        public Customer AnyCustomer { get { return It.IsAny<Customer>(); } }
    }
}
