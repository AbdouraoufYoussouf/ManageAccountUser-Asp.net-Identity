using System.ComponentModel.DataAnnotations;

namespace LoginRegisterUser.ViewModels
{
    public class RoleAddViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
