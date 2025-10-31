using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFServices.Models;
using static WCFServices.Models.CustomQueries;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomer" in both code and config file together.
    [ServiceContract]
    public interface ICustomer
    {

        [OperationContract]
        [WebGet(UriTemplate = "GetCustomersByCountry?country={country}", ResponseFormat = WebMessageFormat.Json)]
        List<CustomerList> GetCustomersByCountry(string country);

        [OperationContract]
        [WebGet(UriTemplate = "GetCustomerOrdersInfo?CustomerID={CustomerID}", ResponseFormat = WebMessageFormat.Json)]
        List<CustomerOrderInfoList> GetCustomerOrdersInfo(string CustomerID);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
