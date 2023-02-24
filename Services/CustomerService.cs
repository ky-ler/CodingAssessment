using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssessment.Services
{
    internal class CustomerService
    {
        public List<Model.Customer> GetAllCustomers()
        {
            var customers = DataAccess.CustomerDataAccess.GetCustomerData();
            return customers;            
        }

        public List<Model.Customer> GetActiveCustomers()
        {
            List<Model.Customer> customers = new List<Model.Customer>();

            foreach (var customer in DataAccess.CustomerDataAccess.GetCustomerData())
            {
                if (customer.Disabled != true)
                {
                    customers.Add(customer);
                }
            }
            return customers;
        }
        
    }
}
