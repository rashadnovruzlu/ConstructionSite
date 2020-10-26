namespace ConstructionSite.DTO.FrontViewModels.Service
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public int ServiceID { get; set; }
        public string Name { get; set; }

        public string Tittle { get; set; }
        public string image { get; set; }
        //public ICollection<SubService> SubServices { get; set; }
    }
}