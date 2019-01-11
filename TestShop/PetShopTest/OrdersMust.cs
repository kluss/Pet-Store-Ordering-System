using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetShop.Models;
using PetShop.Services;
using System.Collections.Generic;

namespace PetShopTest
{
    [TestClass]
    public class OrdersMust
    {
        [TestMethod]
        public void GivenOrder_Return_TotalCost()
        {

            //Arrange
            Order myOrder = new Order()
            {

                OrderId = 1,
                CustomerId = "AAA",
                Items = new List<Item>()
                {
                    new Item  {  ItemId = 1, ProductId=101, Quantity = 1,
                        Product  = new Product()
                        {
                            Id= 101, Name = "product 1", Price = 6.99m

                        }
                    }
                }
            };


            //Act
            //DataServices calc = new DataServices();
            //var orderTotal = calc.GetOrderTotal(myOrder);
            ////Assert
            //Assert.AreEqual(6.99m, orderTotal);

        }

    }

}
