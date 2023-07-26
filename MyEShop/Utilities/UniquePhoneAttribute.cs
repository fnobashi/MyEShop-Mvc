﻿using MyEShop.Contracts.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MyEShop.Utilities
{
    public class UniquePhoneAttribute:ValidationAttribute
    {

        private IUserRepository userRepository;


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {


                var serviceProvider = validationContext.GetService(typeof(IServiceProvider)) as IServiceProvider;
                userRepository = serviceProvider.GetService<IUserRepository>();

                var phone = value.ToString();
                bool phoneExists = userRepository.IsPhoneNumberInUse(phone);
                if (phoneExists)
                    return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
