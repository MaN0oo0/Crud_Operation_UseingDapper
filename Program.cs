using Crud_Operation_UseingDapper;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;

var database = new Database("Server=.;Database=EmployeeDB;Trusted_Connection=True;MultipleActiveResultSets=true;");

var IsOk = "y";
string[] Quests = new string[6];
Quests[0] = "Add New Product ";
Quests[1] = "Get All Product ";
Quests[2] = "Get Product By Id ";
Quests[3] = "Update Product By Id ";
Quests[4] = "Delete Product By Id ";
Quests[5] = "Exit From Application !";
var context = new DefaultHttpContext();
do
{
    var products = database.GetAllProducts();
    context.Items["products"] = products;
    Console.Write("\t \n welcome to product Sys :) \n \n");
    Console.Write("\tPlese Chose Option: \n");
    for (int i = 0; i < Quests.Length; i++)
    {

        Console.WriteLine($"\n\t \t {i+1}- {Quests[i]}\n");
    }
    
    Console.Write("\t  Your Option: ");
  
    var Opt=Console.ReadLine();
    switch (Opt)
    {
        case "1":
            AddNewProduct();
            break;

        case "2":
            DisplayAllProducts(); 
        break;
        case "3":
            Console.Clear();
            Console.Write("Enter Product Id: ");
            var Id = int.Parse(Console.ReadLine());
            GetProductById(Id);
        break;
        case "4":
            Console.Clear();
            Console.Write("Enter Product Id: ");
            int ProductId = int.Parse(Console.ReadLine());
            UpdateProduct(ProductId);
        break;
        case "5":
            Console.Clear();
            Console.Write("Enter Product Id: ");
            int ProductToDeleteId = int.Parse(Console.ReadLine());
            DeleteProduct(ProductToDeleteId);
            break;
        default:
            break;


    }
    Console.Write("Do You Need Anothter Operation(Y/N) ? : ");
    IsOk=Console.ReadLine();
    Console.Clear();

} while (IsOk=="y"||IsOk=="Y");

Console.WriteLine("\t Happy Hacking :) ");


#region Crud Functions
void AddNewProduct()
{



    try
    {
        Console.Clear();
        Console.Write("Enter Product Name: ");
        string Name = Console.ReadLine();
        Console.Write("Enter Product Price: ");
        decimal Price = decimal.Parse(Console.ReadLine());
        Console.Write("Enter Product Count In Stock: ");
        int Stock = int.Parse(Console.ReadLine());
        // Create a new product
        var newProduct = new Product
        {
            Name = Name,
            Price = Price,
            Stock = Stock
        };

        database.AddProduct(newProduct);
        Console.WriteLine("Product added.");
      
    }
    catch (Exception)
    {
        throw;
   
    }
  

   
}

void DisplayAllProducts()
{
    Console.Clear();
    //// Retrieve all products
    var products = database.GetAllProducts();
    context.Items["products"] = products;
    foreach (var product in products)
    {
        Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
    }
}
void GetProductById(int Id)
{
    Console.Clear();
   var ProductList= context.Items["products"] as List<Product>;
   var Product = ProductList.Where(x=>x.Id==Id).FirstOrDefault();

    if (Product is not null)
    {
        Console.WriteLine($"Id: {Product.Id},\n Name: {Product.Name},\n Price: {Product.Price},\n Stock: {Product.Stock}");
    }
    else
    {
        Console.WriteLine($"No Product With This Id :${Id}");
    }
      
}
void  UpdateProduct(int Id)
{
    Console.Clear( );
    try
    {
        var ProductList = context.Items["products"] as List<Product>;
        var Product = ProductList.Where(x => x.Id == Id).FirstOrDefault();
        var arrChange = new List<string>() { "Change Name", "Change Price", "Change Stock" };
        if (Product is not null)
        {
            Console.WriteLine($" Id: {Product.Id},\n Name: {Product.Name},\n Price: {Product.Price},\n Stock: {Product.Stock}\n\n");
            Console.Write("\t\tPlese Chose Number To Applay Change: \n\n");
            for (int i = 0; i < arrChange.Count; i++)
            {
                Console.WriteLine($"\t\t{i + 1}- {arrChange[i]}");
            }
            Console.Write("Your Chose: ");
            var Chose = int.Parse(Console.ReadLine());
            switch (Chose)
            {
                case 1:
                    Console.Write("Enter New Name: ");
                    var Name = Console.ReadLine();
                    Product.Name = Name.Trim();
                    database.UpdateProduct(Product);
                    break;
                case 2:
                    Console.Write("Enter New Price: ");
                    var Price = decimal.Parse(Console.ReadLine());
                    Product.Price = Price;
                    database.UpdateProduct(Product);
                    break;
                case 3:
                    Console.Write("Enter New Stock: ");
                    var Stock = int.Parse(Console.ReadLine());
                    Product.Stock = Stock;
                    database.UpdateProduct(Product);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine($"No Product With This Id :${Id}");
        }
    }
    catch (Exception ex)
    {

        throw;
    }


}
void DeleteProduct(int Id)
{
    Console.Clear();
    try
    {
        var ProductList = context.Items["products"] as List<Product>;
        var Product = ProductList.Where(x => x.Id == Id).FirstOrDefault();
        if (Product != null)
        {
            Console.WriteLine($" Id: {Product.Id},\n Name: {Product.Name},\n Price: {Product.Price},\n Stock: {Product.Stock}\n\n");
            Console.WriteLine("___________________________________________");
            Console.Write("Are You Sure To Delete This Product? (Y/N): ");
            var Confirm = Console.ReadLine();
            if (Confirm.ToLower() == "y")
            {
                database.DeleteProduct(Product.Id);
                Console.WriteLine("Product deleted.");
            }
        }
        else
        {
            Console.WriteLine($"No Product With This Id :${Id}");
        }

    }
    catch (Exception)
    {

        throw;
    }
}


#endregion