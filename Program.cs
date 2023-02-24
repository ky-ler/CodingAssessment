using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodingAssessment
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /************************************************************************************************
             Step 1 : Fix all build errors in the project
             Step 2:  Find all Todo: comments in this console project and code to achieve desired effect.
            *************************************************************************************************/

            Console.WriteLine("Press any key to return all of the customer names (Last, First)");
            Console.ReadKey();

            // Create object from CustomerService
            Services.CustomerService customerService = new Services.CustomerService();

            //// get all customer names 
            foreach (var item in customerService.GetAllCustomers())
                Console.WriteLine($"{item.LastName}, {item.FirstName}");

            Console.WriteLine("Press any key to list every other customer in the list");
            Console.ReadKey();

            // Order customers by Last Name, First Name
            var orderedCustomers = from customer in customerService.GetAllCustomers() 
                                    orderby customer.LastName, customer.FirstName
                                    select customer;
            
            // Output every other customer
            foreach (var customer in orderedCustomers.Where((x, i) => i % 2 == 0))
            {
                Console.WriteLine($"{customer.LastName}, {customer.FirstName}");
            }

            Console.WriteLine("Enter a name below to filter by name (last or first)");
            string nameFilterCriteria = Console.ReadLine().ToLower();
            Console.WriteLine("Below are the results that match your search criteria");

            // Output all customer names where the last name or first name matches any part of the user entered search criteria
            var matchingCustomer = from customer in customerService.GetAllCustomers()
                                   where customer.FirstName.ToLower().Contains(nameFilterCriteria) || customer.LastName.ToLower().Contains(nameFilterCriteria)
                                   select customer;

            foreach (var customer in matchingCustomer)
            {
                Console.WriteLine($"{customer.LastName}, {customer.FirstName}");
            }

                        
            Console.WriteLine("Press any key to list all the Active customers (do not list customers where the disabled flag is set)");
            Console.ReadKey();

            // Output active customers
            foreach(var customer in customerService.GetActiveCustomers())
            {
                Console.WriteLine($"{customer.LastName}, {customer.FirstName}");
            }

            Console.WriteLine("Press any key to list all the Active customers in the following format (LastName, FirstName, Age in Years)");
            Console.ReadKey();

            // Output all active customers in the format above using the existing BirthDate property to calculate the customers age in years  
            foreach (var customer in customerService.GetActiveCustomers())
            {
                Console.WriteLine($"{customer.LastName}, {customer.FirstName}, {DateTime.Now.Year - customer.BirthDate?.Year}");
            }

            Console.WriteLine("Press any key to list all the Active customers in the following format (LastName, FirstName, Age in Years, Probable Gender)");
            Console.ReadKey();

            // Output all active customers in the format above, using the https://api.genderize.io API to guess the customer's gender based on their first name.
            Services.Genderize genderize = new Services.Genderize();
            foreach (var customer in customerService.GetActiveCustomers())
            {
                var gender = await genderize.GetGender(customer.FirstName);
                Console.WriteLine($"{customer.LastName}, {customer.FirstName}, {DateTime.Now.Year - customer.BirthDate?.Year}, {gender}");
            }

            Console.WriteLine("Press any key to end");
            Console.ReadLine();
        }
    }
}
