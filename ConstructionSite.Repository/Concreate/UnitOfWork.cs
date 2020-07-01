using ConstructionSite.Entity.Data;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Helpers.Exceptions;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Implementations;
using ConstructionSite.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public INewsImageRepository newsImageRepository => throw new NotImplementedException();

        public INewsRepository newsRepository => throw new NotImplementedException();

        public IPortfolioRepository portfolioRepository => throw new NotImplementedException();

        public IProjectImageRepository projectImageRepository => throw new NotImplementedException();

        public IProjectRepository projectRepository => throw new NotImplementedException();

        public IServiceRepository ServiceRepository => throw new NotImplementedException();

        public IStaticFieldRepository staticFieldRepository => throw new NotImplementedException();

        public ISubServiceImageRepository SubServiceImageRepository => throw new NotImplementedException();

        public ISubServiceRepository SubServiceRepository => throw new NotImplementedException();

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

    }
}