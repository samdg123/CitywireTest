using App;
using App.Constants;
using Moq;
using System;
using Xunit;

namespace AppTests
{
    public class CustomerServiceTests : Base
    {   
        [Fact]
        public void AddCustomer_Positive_ReturnsTrue()
        {
            //Arrange
            var mockCompanyRepo = MockCompanyRepo;
            mockCompanyRepo.Setup(s => s.GetById(It.IsAny<int>())).Returns(Company_VeryImportant);

            var mockCustomerService = new Mock<CustomerService>(mockCompanyRepo.Object, MockCustomerCreditService.Object);
            mockCustomerService.Setup(s => s.AddCustomerToDatabase(AnyCustomer));

            //Act
            var result = mockCustomerService.Object.AddCustomer(Firstname_Valid, Surname_Valid, Email_Valid, Dob_22YearsOld, 1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddCustomer_Positive_AddCustomerToDatabaseInvoked()
        {
            //Arrange
            var mockCompanyRepo = MockCompanyRepo;
            mockCompanyRepo.Setup(s => s.GetById(It.IsAny<int>())).Returns(Company_VeryImportant);

            var mockCustomerService = new Mock<CustomerService>(mockCompanyRepo.Object, MockCustomerCreditService.Object);
            mockCustomerService.Setup(s => s.AddCustomerToDatabase(It.IsAny<Customer>())).Verifiable();

            //Act
            mockCustomerService.Object.AddCustomer(Firstname_Valid, Surname_Valid, Email_Valid, Dob_22YearsOld, 1);

            //Assert
            mockCustomerService.Verify(s => s.AddCustomerToDatabase(It.IsAny<Customer>()));
        }

        [Fact]
        public void AddCustomer_Negative_Under21()
        {
            //Arrange
            var mockCompanyRepo = MockCompanyRepo;

            var mockCustomerService = new Mock<CustomerService>(mockCompanyRepo.Object, MockCustomerCreditService.Object);
            mockCustomerService.Setup(s => s.AddCustomerToDatabase(It.IsAny<Customer>()));

            //Act
            var result = mockCustomerService.Object.AddCustomer(Firstname_Valid, Surname_Valid, Email_Valid, Dob_20YearsOld, 1);

            //Assert
            Assert.False(result);
        }

        /// <summary>
        /// Checking the ImportantClient gets double the limit
        /// </summary>
        [Fact]
        public void CalculateCreditLimit_ImportantClient_GetsDouble()
        {
            //Arrange
            var maxCreditLimit = Settings.MaxCreditLimit;
            var mockCustomerCreditService = MockCustomerCreditService;
            mockCustomerCreditService.Setup(s => s.GetCreditLimit(AnyString, AnyString, AnyDateTime)).Returns(maxCreditLimit);

            var mockCustomerService = new Mock<CustomerService>(MockCompanyRepo.Object, mockCustomerCreditService.Object);

            //Act
            var result = mockCustomerService.Object.CalculateCreditLimit(Clients.ImportantClient, Firstname_Valid, Surname_Valid, Dob_22YearsOld);

            //Assert
            Assert.True(result == maxCreditLimit * 2);
        }
    }
}
