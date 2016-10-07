using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Zervo.ViewModels
{
    public class CustomerViewModel
    {
        [BindNever]
        public int Id { get; set; }
        [BindNever]
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}
