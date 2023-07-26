using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MyEShop.Utilities
{
    public class UniqueEmailAttribute:ValidationAttribute
    {
        private IUserRepository userRepository;


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {


                var serviceProvider = validationContext.GetService(typeof(IServiceProvider)) as IServiceProvider;
                userRepository = serviceProvider.GetService<IUserRepository>();

                var email = value.ToString();
                bool emailExists = userRepository.IsEmailInUse(email) ;
                if (emailExists)
                    return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
