using System.Net;
using Dal.Api;
using Dal.Entities;
using Dal.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

public class DalManager : IDal
{
    public IUser user { get; }
    public ICategory category { get; }
    public ISubCategory subCategory { get; }
    public IPrompt prompt { get; }

    public DalManager()
    {

        ServiceCollection service = new ServiceCollection();
        service.AddSingleton<LearningPlatformContext>();
        service.AddSingleton<IPrompt, PromptService>();
        service.AddSingleton<ISubCategory, SubCategoryService>();
        service.AddSingleton<ICategory, CategoryService>();
        service.AddSingleton<IUser, UserService>();

        ServiceProvider serviceProvider = service.BuildServiceProvider();
        category = serviceProvider.GetService<ICategory>();
        subCategory = serviceProvider.GetService<ISubCategory>();
        prompt = serviceProvider.GetService<IPrompt>();
        user = serviceProvider.GetService<IUser>();

    }
}