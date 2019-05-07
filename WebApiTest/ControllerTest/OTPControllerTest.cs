using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest.ControllerTest
{
    //Please change the testing database
    [TestClass]
    public class OTPControllerTest
    {
        [TestMethod]
        public void TestIsOTPVerified()
        {
            var context = new ApplicationDbContext();
            string phoneNumber = "9474015270";

            var isOtpVerified = OtpController.IsOtpVerified(context, phoneNumber);

            Assert.IsTrue(isOtpVerified);
        }
    }
}
