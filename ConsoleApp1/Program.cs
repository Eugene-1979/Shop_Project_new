// See https://aka.ms/new-console-template for more information
using Shop_Project.Models;

Console.WriteLine("Hello, World!");
List<Product> lst=new List<Product>() { 
new Product(){Name="q" },
new Product(){Name="w" },
new Product(){Name="e" }

};
var t = typeof(Product);
var tt=t.GetProperty("Name");
var rrrr=lst.OrderBy(q => tt.Name).ToList();
var rrrr1=lst.OrderBy(q => tt.PropertyType).ToList();
var rrrr12=lst.OrderBy(q => tt.DeclaringType).ToList();

Console.ReadLine();
