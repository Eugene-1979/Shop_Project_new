using Bogus;
using Shop_Project.Models;

namespace Shop_Project.Db
    {
    public class DbObjects
        {
        static Faker faker = new Faker();



        public static void Initial(AppDbContent content,
        int category = 10,
        int product = 100,
        int empl = 10,
        int customer = 10,
        int order = 400)
            {
            AppDbContent appDbContent = content;

            /*Category*/
            var tcategory = new Faker<Category>()
               .RuleFor(q => q.Name, q => q.Lorem.Word()).
               RuleFor(q=>q.Salary,w=>0).
               Generate(category);

            appDbContent.Categorys.AddRange(tcategory);

            /*Product*/
            var tproducts = tcategory.SelectMany(categ =>
             new Faker<Product>().
             RuleFor(q => q.Name, fak => fak.Random.Words()).
             RuleFor(q=>q.About,f=>f.Lorem.Text()).
             RuleFor(q=>q.Reviews,f=>f.Lorem.Text()).
             RuleFor(q => q.Sale, fak => fak.Random.Int(1, 10000)).
             RuleFor(q => q.Category, fak => categ).
             Generate(product)).ToList();

            


            /*Employee*/
            var temployees = new Faker<Employee>()
               .RuleFor(q => q.Name, q => q.Name.FullName()).
                RuleFor(q => q.Email, w => w.Internet.Email()).
                RuleFor(q=>q.Salary,w=>w.Random.Int(1,10000)).
               Generate(empl);

            appDbContent.Employees.AddRange(temployees);

            /*Customer*/

            var tcustomers = new Faker<Customer>()
               .RuleFor(q => q.Name, q => q.Name.FullName()).
               RuleFor(q=>q.Email,w=>w.Internet.Email()).
               Generate(customer);

            appDbContent.Customers.AddRange(tcustomers);

            /*Order*/

            var torders = new Faker<Order>().
              RuleFor(q => q.Customer, w => tcustomers[w.Random.Int(0, customer-1)]).
              RuleFor(q => q.Employee, w => temployees[w.Random.Int(0, empl-1)]).
              RuleFor(q => q.MyDate, w => DateTime.Now).

              RuleFor(q => q.Products, (w, t) => Enumerable.Range(0, (int)(product / 4)).Select(q => tproducts[q]).ToList()).
              Generate(order);
                
               


                /*Entrollments*/

                foreach(var item in torders)
                    {
                    List<Product> temp = new List<Product>();
                    for(int i = 0; i < product / 4; i++)
                        {
                        Product product1 = tproducts[faker.Random.Int(0, product-1)];
                        if(!temp.Contains(product1))
                            {
                            temp.Add(product1);
                            item.Enrollments.Add(new Enrollment() { Product = product1, Count = faker.Random.Int(1, 10000) });
                            }
                        }



                    }

            appDbContent.Products.AddRange(tproducts);
            appDbContent.Orders.AddRange(torders);

            appDbContent.SaveChanges();

                

           
            }
        
        }
    }
