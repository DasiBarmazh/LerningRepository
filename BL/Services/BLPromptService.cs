using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Api;
using BL.Models;
using Dal.Api;
using Microsoft.Identity.Client;

namespace BL.Services;

public class BLPromptService : IBLPrompt
{
    IPrompt dalPrompt;
    public BLPromptService (IDal dal)
    {
        dalPrompt = dal.prompt;
    }
    public async Task CreatPrompt(LessonRequest lesson)
    {

    }
}
