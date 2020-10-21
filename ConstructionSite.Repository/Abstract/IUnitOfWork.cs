﻿using ConstructionSite.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IAboutImageRepository AboutImageRepository { get; }

        IAboutRepository AboutRepository { get; }
        IContactRepository ContactRepository { get; }
        ICustomerFeedbackRepository customerFeedbackRepository { get; }
        IHomePageRepository HomePageRepository { get; }
        IImageRepository imageRepository { get; }
        IMessageRepository messageRepository { get; }
        INewsImageRepository newsImageRepository { get; }
        INewsRepository newsRepository { get; }
        IPortfolioRepository portfolioRepository { get; }
        IProjectImageRepository projectImageRepository { get; }
        IProjectRepository projectRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IStaticFieldRepository staticFieldRepository { get; }
        ISubServiceImageRepository SubServiceImageRepository { get; }
        ISubServiceRepository SubServiceRepository { get; }
        IGaleryFileRepstory GaleryFileRepstory { get; }
        IGaleryRepstory GaleryRepstory { get; }
        IPortfolioImageRepostory PortfolioImageRepostory { get; }
        Task<int> CommitAsync();

        int Commit();

        void Rollback();
    }
}