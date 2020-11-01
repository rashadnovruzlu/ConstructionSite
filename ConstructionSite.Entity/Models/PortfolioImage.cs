namespace ConstructionSite.Entity.Models
{
    public class PortfolioImage
    {
        public int Id { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public int PortfolioId { get; set; }

        public virtual Image Image { get; set; }
        public int ImageId { get; set; }
    }
}