using lec11.Data;
using lec11.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace lec11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ();
            //1-List all customers' first and last names along with their email addresses.

            var customers = context.Customers.Select(c => new{c.FirstName,c.LastName,c.Email});
            // 2 - Retrieve all orders processed by a specific staff member(e.g., staff_id = 3).
            var orders = context.Orders.Where(o => o.StaffId == 3);
            // 3- Get all products that belong to a category named "Mountain Bikes".
            var products = context.Products.Where(p => p.Category.CategoryName == "Mountain Bikes");
            // 4-Count the total number of orders per store.
            var ordersPerStore = context.Orders
                .GroupBy(o => o.StoreId)
                .Select(g => new
                {
                    StoreId = g.Key,
                    OrdersCount = g.Count()
                })
                .ToList();
            //5- List all orders that have not been shipped yet (shipped_date is null).
            var ordersNotShipped = context.Orders.Where(s => s.ShippedDate == null);
            //6- Display each customer’s full name and the number of orders they have placed.
            var    customerOrdersCount = context.Customers
                .Select(c => new
                {
                    FullName = c.FirstName + " " + c.LastName,
                    OrdersCount = c.Orders.Count()
                })
                .ToList();

            //7- List all products that have never been ordered (not found in order_items).
            var productsNotShipped = context.Products.Where(p => p.OrderItems == null);

            //8- Display products that have a quantity of less than 5 in any store stock.
            var StockLeft = context.Stocks.Where(s => s.Quantity < 5).Select(s => s.Product);
            //9- Retrieve the first product from the products table.
            var firstProduct = context.Products.FirstOrDefault();
            //10- Retrieve all products from the products table with a certain model year.
            var productsByModelYear = context.Products.Where(p => p.ModelYear == 2020);
            //11- Display each product with the number of times it was ordered.
            var productOrderCounts = context.Products
                .Select(p => new
                {
                    ProductName = p.ProductName,
                    OrderCount = p.OrderItems.Count()
                })
                .ToList();
            // 12- Count the number of products in a specific category.
            var countProducts = context.Categories
                .GroupBy(o => o.CategoryName)
                .Select(r => new
                {
                    Category = r.Key,
                    products = r.Count()
                });
            // 13- Calculate the average list price of products.

            var sumProducts = context.Products.Average(p => p.ListPrice);

            //14- Retrieve a specific product from the products table by ID.
            //Console.Write("Enter Product ID: ");
            //int productId = Convert.ToInt32(Console.ReadLine());
            var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
            //15- List all products that were ordered with a quantity greater than 3 in any order.
            var productsQuantity = context.OrderItems
                .Where(o => o.Quantity > 3)
                .Select(o => o.Product)
                .ToList();
            //16- Display each staff member’s name and how many orders they processed.

            var staff = context.Staffs
               .Select(c => new
               {
                   FullName = c.FirstName + " " + c.LastName,
                   OrdersCount = c.Orders.Count()
               })
               .ToList();
            //17- List active staff members only (active = true) along with their phone numbers.
            var activeStaff = context.Staffs.Where(s => s.Active != null).Select(s => new { s.Phone });
            //18- List all products with their brand name and category name.
            var productDetails = context.Products.Select(p => new
            {
                p.ProductName,
                BrandName = p.Brand.BrandName,
                CategoryName = p.Category.CategoryName
            });
            // 19- Retrieve orders that are completed.
            var completedOrders = context.Orders.Where(o => o.OrderStatus == 1); 
            //20- List each product with the total quantity sold (sum of quantity from order_items).
            var totalQuantitySold = context.Products
                .Select(p => new
                {
                    ProductName = p.ProductName,
                    TotalQuantitySold = p.OrderItems.Sum(oi => oi.Quantity)
                })
                .ToList();
            Console.WriteLine(totalQuantitySold);

        }



    }
}
