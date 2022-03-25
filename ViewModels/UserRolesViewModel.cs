namespace LoginRegisterUser.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId {get; set;}
        public string UserName {get; set;}
        public string LastName {get; set;}
        public List<RoleViewModel> Roles {get; set;}
    }
}