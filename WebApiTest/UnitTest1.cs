using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi;
using WebApi.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Bill sampleBill = new Bill()
            {
                BillNumber = "5",
                PhoneNumber = "9474005270",
                ShopName = "82938334-EC45-4E4A-AF24-A6311D27148F",
                Total = 1160,
                TotalItems = 2,
                TotalQuantity = 2,
                TransactionTime = DateTime.Now,
                ItemList = new List<ItemDetail>()
                {
                    new ItemDetail{Name = "Reebok Pant", Amount=660},
                    new ItemDetail{Name = "Spykar Jeans", Amount=500},
                }
            };

            var serializedBill = JsonConvert.SerializeObject(sampleBill);
        }

        [TestMethod]
        public void ShopTesting()
        {
            Shop sample = new Shop()
            {
                ShopDetails = new ShopDetail
                {
                    Name = "ShoppersStop",
                    Location = "InorbitMall",
                    PhoneNumber = "9090909090",
                    GSTIN = "56325632563"
                }
            };

            var serializedBill = JsonConvert.SerializeObject(sample);
        }

        [TestMethod]
        public void AddShop()
        {
            var shop = new Shop()
            {
                ShopDetails = new ShopDetail()
                {
                    Name = "ShoppersStop",
                    Location = "InorbitMall",
                    PhoneNumber = "9090909090",
                    GSTIN = "56325632563"
                }
            };

            var shopController = new ShopsController();
            var id = shopController.PostShop(shop);


        }
    }
}
