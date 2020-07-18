﻿using ConstructionSite.Entity.Data;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Implementations;
using ConstructionSite.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Concreate
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAboutImageRepository         _aboutImageRepository;
        private IAboutRepository              _AboutRepository;
        private IContactRepository            _ContactRepository;
        private ICustomerFeedbackRepository   _customerFeedbackRepository;
        private IHomePageRepository _HomePageRepository;
        private IImageRepository _imageRepository;
        private IMessageRepository _messageRepository;
        private INewsImageRepository _newsImageRepository;
        private INewsRepository _newsRepository;
        private IPortfolioRepository _portfolioRepository;
        private IProjectImageRepository _projectImageRepository;
        private IProjectRepository _projectRepository;
        private IServiceRepository _serviceRepository;
        private IStaticFieldRepository _staticFieldRepository;
        private ISubServiceImageRepository _subServiceImageRepository;
        private ISubServiceRepository _SubServiceRepository;
       
        private readonly ConstructionDbContext _context;
        public UnitOfWork(ConstructionDbContext context)
        {
            _context=context;
        }
        public IAboutImageRepository AboutImageRepository
        {
            get
            {
                return _aboutImageRepository??(_aboutImageRepository=new AboutImageRepository(_context));
            }
        }

        public IAboutRepository AboutRepository
        {
            get
            {
                return _AboutRepository??(_AboutRepository=new AboutRepository(_context));
            }
        }

        public IContactRepository ContactRepository
        {
            get
            {
                return _ContactRepository??(_ContactRepository=new ContactRepository(_context));
            }
        }

        public ICustomerFeedbackRepository customerFeedbackRepository
        {
            get
            {
                return _customerFeedbackRepository??(_customerFeedbackRepository=new CustomerFeedbackRepository(_context));
            }
        }

        public IHomePageRepository HomePageRepository
        {
            get
            {
                return _HomePageRepository??(_HomePageRepository=new HomePageRepository(_context));
            }
        }

        public IImageRepository imageRepository
        {
            get
            {
                return _imageRepository??(_imageRepository=new ImageRepository(_context));
            }
        }

        public IMessageRepository messageRepository
        {
            get
            {
                return _messageRepository ?? (_messageRepository = new MessageRepository(_context));
            }
        }

        public INewsImageRepository newsImageRepository
        {
            get
            {
                return _newsImageRepository??(_newsImageRepository=new NewsImageRepository(_context));
            }
        }

        public INewsRepository newsRepository
        {
            get
            {
                return _newsRepository??(_newsRepository=new NewsRepository(_context));
            }
        }

        public IPortfolioRepository portfolioRepository
        {
            get
            {
                return _portfolioRepository??(_portfolioRepository=new PortfolioRepository(_context));
            }
        }

        public IProjectImageRepository projectImageRepository
        {
            get
            {
                return _projectImageRepository??(_projectImageRepository=new ProjectImageRepository(_context));
            }
        }

        public IProjectRepository projectRepository
        {
            get
            {
                return _projectRepository??(_projectRepository=new ProjectRepository(_context));
            }
        }

        public IServiceRepository ServiceRepository
        {
            get
            {
                return _serviceRepository??(_serviceRepository=new ServiceRepository(_context));
            }
        }

        public IStaticFieldRepository staticFieldRepository
        {
            get
            {
                return _staticFieldRepository??(_staticFieldRepository=new StaticFieldRepository(_context));
            }
        }

        public ISubServiceImageRepository SubServiceImageRepository
        {
            get
            {
              return  _subServiceImageRepository??(_subServiceImageRepository=new SubServiceImageRepository(_context));
            }
        }

        public ISubServiceRepository SubServiceRepository
        {
            get
            {
                return _SubServiceRepository??(_SubServiceRepository=new SubServiceRepository(_context));
            }
        }

       

        public  int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
           _context.Dispose();
        }

        public async Task<int> CommitAsync()
        {
           return  await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}