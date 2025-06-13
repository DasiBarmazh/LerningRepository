using System.Net;
using BL.Api;
using BL.Services;
using Dal;
using Dal.Api;
using Dal.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BL;

public class BLManager : IBL
{
    public IBLUser BLUser { get; }
    public IBLCategory BLCategory { get; }
    public IBLSubCategory BLSubCategory { get; }
    public IOpenAI OpenAI { get; }
    public IBLPrompt prompt { get; }
    

    public BLManager(IOpenAI openAI)
    {

        ServiceCollection services = new ServiceCollection();

        services.AddSingleton<IDal, DalManager>();
        services.AddSingleton<IBLUser, BLUserService>();
        services.AddSingleton<IBLCategory, BLCategoryService>();
        services.AddSingleton<IBLSubCategory, BLSubCategoryService>();
        services.AddSingleton<IBLPrompt, BLPromptService>();


        ServiceProvider serviceProvider = services.BuildServiceProvider();

        BLUser = serviceProvider.GetService<IBLUser>();
        BLCategory = serviceProvider.GetService<IBLCategory>();
        BLSubCategory = serviceProvider.GetService<IBLSubCategory>();
        OpenAI = openAI;
        prompt = serviceProvider.GetService<IBLPrompt>();
    }

}
