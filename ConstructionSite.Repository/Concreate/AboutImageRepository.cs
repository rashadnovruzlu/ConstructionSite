using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;

namespace ConstructionSite.Repository.Concreate
{
  public  class AboutImageRepository:GenericRepository<AboutImage>,IAboutImageRepository
    {
        public AboutImageRepository(ConstructionDbContext context):base(context)
        {

        }
     
       
    }
  public class AboutRepository : GenericRepository<About>, IAboutRepository
    {
        public AboutRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class HomePageRepository : GenericRepository<HomePage>, IHomePageRepository
    {
        public HomePageRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class CustomerFeedbackRepository : GenericRepository<CustomerFeedback>, ICustomerFeedbackRepository
    {
        public CustomerFeedbackRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class NewsImageRepository : GenericRepository<NewsImage>, INewsImageRepository
    {
        public NewsImageRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class ProjectImageRepository : GenericRepository<ProjectImage>, IProjectImageRepository
    {
        public ProjectImageRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class StaticFieldRepository : GenericRepository<StaticField>, IStaticFieldRepository
    {
        public StaticFieldRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class SubServiceImageRepository : GenericRepository<SubServiceImage>, ISubServiceImageRepository
    {
        public SubServiceImageRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  public class SubServiceRepository : GenericRepository<SubService>, ISubServiceRepository
    {
        public SubServiceRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
  

}
