using MoboFix.Data;
using MoboFix.Utilities.Program.Status;
using System.ComponentModel.DataAnnotations.Schema;



namespace MoboFix.Models
{

    public class ApplicationUserSeller : ApplicationUser
    {

        [InverseProperty("Seller")]
        public List<Advertisment>? Advertisments { get; set; }



        /*        public int AddProduct(string ProductName="N/A", int ProductPrice = 0,string ProductCodeNumber = "N/A")
                {
                    Product newProduct = new Product
                    {
                        Name = ProductName,
                        Price = ProductPrice,
                        CodeNumber = ProductCodeNumber

                    };

                    var Resault = _context.Products.Add(newProduct);
                    Console.WriteLine(Resault);
                    return _context.SaveChanges();
                }
                public int AddProductAdvertisement(int ProductId)
                {
                    Advertisment newAdvertisment = new Advertisment {
                        ProductId = ProductId,
                        SellerId = this.Id
                    };
                    var Resault = _context.Advertisments.Add(newAdvertisment);
                    Console.WriteLine(Resault);
                    return _context.SaveChanges();

                }
                public int DeleteProductAdvertisement(int ProductId)
                {
                    Advertisment delAdvertisment = _context.Advertisments.SingleOrDefault(a => a.ProductId == ProductId);
                    if (delAdvertisment != null)
                    {

                    var Resault = _context.Advertisments.Remove(delAdvertisment);
                    Console.WriteLine(Resault);
                    }
                    return _context.SaveChanges();

                }
                public int EnableAdvertise(Advertisment advertisment)
                {
                    var AdResault = _context.Advertisments.SingleOrDefault(c => c.Id == advertisment.Id);
                    if(AdResault == null)
                    {
                        Console.WriteLine("Advertisment id:{0} do not exist", advertisment.Id);
                        return 0;
                    }
                    else
                    {
                        if(AdResault.SellerId != advertisment.SellerId)
                        {
                            Console.WriteLine("Seller id:{0} cant access to advertisment id:{1}", this.Id, advertisment.Id);
                            return 0;
                        }
                        AdResault.SetStatus(ProgramStatusCodes.Active);
                    }

                    return _context.SaveChanges();
                }
                public int DisableAdvertise(Advertisment advertisment)
                {
                    var AdResault = _context.Advertisments.SingleOrDefault(c => c.Id == advertisment.Id);
                    if (AdResault == null)
                    {
                        Console.WriteLine("Advertisment id:{0} do not exist", advertisment.Id);
                        return 0;
                    }
                    else
                    {
                        if (AdResault.SellerId != advertisment.SellerId)
                        {
                            Console.WriteLine("Seller id:{0} cant access to advertisment id:{1}",this.Id ,advertisment.Id);
                            return 0;
                        }
                        AdResault.SetStatus(ProgramStatusCodes.Disable);
                    }

                    return _context.SaveChanges();
                }
            }*/
    }
}
