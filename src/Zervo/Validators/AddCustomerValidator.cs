using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zervo.Features.Customers;
using Zervo.ViewModels;

namespace Zervo.Validators
{
    public class AddCustomerValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerValidator()
        {
            RuleFor(command => command.CustomerViewModel.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.CustomerViewModel.FirstName).NotEmpty();
            RuleFor(command => command.CustomerViewModel.LastName).NotEmpty();
        }
    }
}
