using Deadliner.Api.Controller;
using Deadliner.Models;

namespace Deadliner.Controller;

public class BaseController
{
    private IContext _context;

    BaseController()
    {
        _context = new BaseContext();
    }
}