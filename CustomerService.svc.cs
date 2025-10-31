using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using WCFServices.Models;
using static WCFServices.Models.CustomQueries;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CustomerService.svc or CustomerService.svc.cs at the Solution Explorer and start debugging.
    public class CustomerService : ICustomer
    {
        private readonly NorthwindEntities _context;

        public CustomerService(NorthwindEntities context)
        {
            _context = context;
        }

        // Constructor por defecto requerido por WCF
        public CustomerService()
        {
            _context = new NorthwindEntities();
        }

        public List<CustomerList> GetCustomersByCountry(string country)
        {
            try
            {
                //using (var db = new NorthwindEntities())
                //{
                    // Consulta LINQ EF
                    var customers = (from c in _context.Customers
                                     where c.Country == country
                                     orderby c.ContactName
                                     select new CustomerList
                                     {
                                         CustomerID = c.CustomerID,
                                         CompanyName = c.CompanyName,
                                         ContactName = c.ContactName,
                                         Phone = c.Phone,
                                         Fax = c.Fax
                                     }).ToList();

                    if (customers.Count > 0)
                    {
                        // webTrack log
                        LogAction();
                        return customers;
                    }


                // Log.save(this, "GetCustomersByCountry :: not found", "logPath");

                return null; // o una lista vacía si prefieres
                //}
            }
            catch (Exception ex)
            {
                // Log.save(this, $"GetCustomersByCountry :: error {ex.Message}", "logPath");
                return null;
            }
        }

        public List<CustomerOrderInfoList> GetCustomerOrdersInfo(string CustomerID)
        {
            try
            {
                //using (var db = new NorthwindEntities())
                //{
                    // Consulta LINQ EF
                    var orders = (from o in _context.Orders
                                     where o.CustomerID == CustomerID
                                     orderby o.ShippedDate
                                     select new CustomerOrderInfoList
                                     {
                                         OrderID = o.OrderID,
                                         CustomerID = o.CustomerID,
                                         OrderDate = (DateTime)o.OrderDate,
                                         ShippedDate = (DateTime)o.ShippedDate
                                     }).ToList();

                    if (orders.Count > 0)
                    {
                        // webTrack log
                        LogAction();
                        return orders;
                    }

                    // webTrack log
                    // Log.save(this, "GetCustomersByCountry :: not found", "logPath");

                    return null; // o una lista vacía si prefieres
                //}
            }
            catch (Exception ex)
            {
                // Log.save(this, $"GetCustomersByCountry :: error {ex.Message}", "logPath");
                return null;
            }   
        }

        private void LogAction()
        {
            try
            {
                // Get complete URL, actual request
                string URL = WebOperationContext.Current?.IncomingRequest?.UriTemplateMatch?.RequestUri?.ToString();
                // Get Client IP address (if HTTP context exist)
                string IP = GetIPAddress();

                var log = new webTracker
                    {
                        URLRequest = URL,
                        SourceIp = IP,
                        TimeOfAction = DateTime.Now
                    };

                    _context.webTrackers.Add(log);
                    _context.SaveChanges();
            }
            catch
            {
                // En entorno real, podrías registrar el error en un archivo o sistema de logs

            }
        }

        private string GetIPAddress()
        {
            try
            {
                string address = string.Empty;

                var context = OperationContext.Current;
                if (context == null)
                    return "Unknown";

                var properties = context.IncomingMessageProperties;

                // 1️⃣ Si hay cabecera HTTP (caso con Load Balancer o proxy)
                if (properties.ContainsKey(HttpRequestMessageProperty.Name))
                {
                    var httpRequest = properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                    if (httpRequest != null)
                    {
                        string forwardedFor = httpRequest.Headers["X-Forwarded-For"];
                        if (!string.IsNullOrWhiteSpace(forwardedFor))
                        {
                            // Puede traer varias IP separadas por coma → tomamos la primera
                            address = forwardedFor.Split(',')[0].Trim();
                        }
                    }
                }

                // 2️⃣ Si no vino por cabecera, obtener IP directa del endpoint TCP
                if (string.IsNullOrWhiteSpace(address) && properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    var endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    if (endpoint != null)
                        address = endpoint.Address;
                }

                // 3️⃣ Convertir IPv6 local a IPv4
                if (address == "::1" || address == "0:0:0:0:0:0:0:1")
                    address = "127.0.0.1";

                return string.IsNullOrWhiteSpace(address) ? "Unknown" : address;
            }
            catch
            {
                return "Unknown";
            }
        }


    }
}
