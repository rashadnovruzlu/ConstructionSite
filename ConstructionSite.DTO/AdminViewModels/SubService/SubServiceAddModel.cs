namespace ConstructionSite.DTO.AdminViewModels.SubService
{
    public class SubServiceAddModel
    {
        public int Id { get; set; }
        public string NameAz { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }
        public int ServiceId { get; set; }
    }
}