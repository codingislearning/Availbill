using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Utility;

namespace WebApiTest.UtilityTest
{
    [TestClass]
    public class HelperTest
    {
        [TestMethod]
        public void TestIsValidPhoneNumber()
        {
            string phoneNumber = "9474005270";
            Assert.IsTrue(Helper.IsValidPhoneNumber(phoneNumber));

            phoneNumber = "947405270";
            Assert.IsFalse(Helper.IsValidPhoneNumber(phoneNumber));

            phoneNumber = "947400a270";
            Assert.IsFalse(Helper.IsValidPhoneNumber(phoneNumber));
        }
    }
}
