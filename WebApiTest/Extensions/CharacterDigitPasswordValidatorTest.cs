using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Extentions;

namespace WebApiTest.Extensions
{
    [TestClass]
    public class CharacterDigitPasswordValidatorTest
    {
        [TestMethod]
        public async Task TestPasswordValidatorAsync()
        {
            var validator = new CharacterDigitPasswordValidator()
            {
                RequireCharacter = true,
                RequireDigit = true,
                RequiredLength = 8
            };

            string password = "9474005270";
            var isValid = await validator.ValidateAsync(password);
            Assert.IsFalse(isValid.Succeeded);

            password = "amgu";
            isValid = await validator.ValidateAsync(password);
            Assert.IsFalse(isValid.Succeeded);

            password = "amangupta";
            isValid = await validator.ValidateAsync(password);
            Assert.IsFalse(isValid.Succeeded);


            password = "amangupta04";
            isValid = await validator.ValidateAsync(password);
            Assert.IsTrue(isValid.Succeeded);
        }
    }
}
