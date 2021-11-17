using App.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class CustomerService
    {
        private ICompanyRepository CompanyRepository { get; set; }
        private ICustomerCreditService CustomerCreditService { get; set; }

        public CustomerService(ICompanyRepository companyRepository, ICustomerCreditService customerCreditService)
        {
            CompanyRepository = companyRepository;
            CustomerCreditService = customerCreditService;
        }

        public bool AddCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            // First stage validation
            if (!(CheckNameValid(firstname, surname) &&
                  CheckEmailValid(email) &&
                  CheckOfAge(dateOfBirth)))
                return false;

            // Get company from repo
            var company = CompanyRepository.GetById(companyId);

            //Set Credit Limit
            var hasCreditLimit = HasCreditLimit(company.Name);
            var creditLimit = 0;

            if (hasCreditLimit)
                creditLimit = CalculateCreditLimit(company.Name, firstname, surname, dateOfBirth);

            // Final stage validation
            if (!CheckBelowCreditMaxLimit(hasCreditLimit, creditLimit))
                return false;

            // Validation passed. Add to database
            var customer = new Customer
            {
                Company = company,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstname,
                Surname = surname,
                HasCreditLimit = hasCreditLimit,
                CreditLimit = creditLimit
            };

            AddCustomerToDatabase(customer);
            return true;
        }

        public virtual void AddCustomerToDatabase(Customer customer)
        {
            CustomerRepository.AddCustomer(customer);
        }

        #region Calculate Credit Limit
        public bool HasCreditLimit(string companyName)
        {
            switch (companyName)
            {
                case Clients.VeryImportantClient:
                    return false;

                case Clients.ImportantClient:
                default:
                    return true;
            }
        }

        public int CalculateCreditLimit(string companyName, string firstname, string surname, DateTime dateOfBirth)
        {
            int creditLimit;

            creditLimit = CustomerCreditService.GetCreditLimit(firstname, surname, dateOfBirth);

            if (companyName == Clients.ImportantClient)
                creditLimit *= 2; // If important customer then double credit limit

            return creditLimit;
        }
        #endregion

        #region Validation
        public bool CheckNameValid(string firstname, string surname)
        {
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }
            return true;
        }

        public bool CheckEmailValid(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            return true;
        }

        public bool CheckOfAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < Settings.MinimumAge)
            {
                return false;
            }
            return true;
        }

        public bool CheckBelowCreditMaxLimit(bool hasCreditLimit, int creditLimit)
        {
            if (hasCreditLimit && creditLimit < Settings.MaxCreditLimit)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
