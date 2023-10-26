using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Registration.Models
{
    public class CharactersANameAttribute: ValidationAttribute, IClientModelValidator
    {

        protected override ValidationResult IsValid(object? v, ValidationContext ctx)
        {
            if (v is string)
            {
                string checkUsername = (string)v;
                if(!checkUsername.EndsWith("Patrick"))
                {
                    return ValidationResult.Success!;
                }
            }



            string msg = base.ErrorMessage ??
                    $"{ctx.DisplayName} please, please do not end username with our name";
            return new ValidationResult(msg);

        }

        public void AddValidation(ClientModelValidationContext c)
        {
            if(!c.Attributes.ContainsKey("data-val"))
            {
                c.Attributes.Add("data-val", "true");
                c.Attributes.Add("data-val-charactersaname", 
                    GetMsg(c.ModelMetadata.DisplayName) ?? c.ModelMetadata.Name ?? "Value");
            }
        }

        private string GetMsg(string? name) =>
            base.ErrorMessage ?? name + $" please, please do not end username with our name";
    }
}
