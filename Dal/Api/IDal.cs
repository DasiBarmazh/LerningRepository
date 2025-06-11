using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api;

public interface IDal
{
    public IUser user { get; }
    public ICategory category { get; }
    public ISubCategory subCategory { get; }
    public IPrompt prompt { get; }
}
