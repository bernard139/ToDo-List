//using System;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;

//public class UsernameOrEmailAttribute : ValidationAttribute
//{
//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//        var userManager = validationContext.GetService<UserManager<IdentityUser>>();

//        var usernameOrEmail = value?.ToString();
//        if (!string.IsNullOrEmpty(usernameOrEmail))
//        {
//            var user = userManager.FindByEmailAsync(usernameOrEmail).Result
//                       ?? userManager.FindByNameAsync(usernameOrEmail).Result;

//            if (user == null)
//            {
//                return new ValidationResult("Invalid username or email.");
//            }
//        }
//        else
//        {
//            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
//            var requiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute), true)
//                as RequiredAttribute[];

//            if (requiredAttribute?.Length > 0)
//            {
//                return new ValidationResult(requiredAttribute[0].ErrorMessage);
//            }
//        }

//        return ValidationResult.Success;
//    }
//}
