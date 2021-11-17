using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace App
{
    [GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [ServiceContractAttribute(ConfigurationName = "App.ICustomerCreditService")]
    public interface ICustomerCreditService
    {

        [OperationContractAttribute(Action = "http://tempuri.org/ICustomerCreditService/GetCreditLimit", ReplyAction = "http://tempuri.org/ICustomerCreditService/GetCreditLimitResponse")]
        int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);
    }

    [GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerCreditServiceChannel : ICustomerCreditService, IClientChannel
    {
    }

    [DebuggerStepThroughAttribute()]
    [GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerCreditServiceClient : ClientBase<ICustomerCreditService>, ICustomerCreditService
    {

        public CustomerCreditServiceClient()
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CustomerCreditServiceClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            return base.Channel.GetCreditLimit(firstname, surname, dateOfBirth);
        }
    }
}
