using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Extentions
{
    public class CharacterDigitPasswordValidator: PasswordValidator
    {
        public bool RequireCharacter { get; set; }

        public bool IsCharacterPresent(string s)
        {
            return s.Any(c => Char.IsLetter(c));
        }

        public override async Task<IdentityResult> ValidateAsync(string item)
        {
            IdentityResult result = await base.ValidateAsync(item);

            var errors = result.Errors.ToList();

            if (RequireCharacter)
            {
                if (string.IsNullOrEmpty(item) || !IsCharacterPresent(item))
                {
                    errors.Add(string.Format("Password should contain atleast one character"));
                }
            }


            return await Task.FromResult(!errors.Any()
             ? IdentityResult.Success
             : IdentityResult.Failed(errors.ToArray()));
        }
    }
}