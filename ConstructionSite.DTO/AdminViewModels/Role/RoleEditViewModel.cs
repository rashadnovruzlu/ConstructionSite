namespace ConstructionSite.DTO.AdminViewModels.Role
{
    public class RoleEditViewModel
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string[] IDsToAdd { get; set; }
        public string[] IDsToDelete { get; set; }
    }
}