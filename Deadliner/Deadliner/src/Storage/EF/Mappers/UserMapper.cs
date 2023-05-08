using Deadliner.Api.Models;
using Deadliner.Storage.EF.ModelsDB;

namespace Deadliner.Storage.EF.Mappers;

public class UserMapper : IMapper<IUser, User>
{
    public IUser ReadItem(User model)
    {
        return new Models.User { Id = model.Id, Password = model.Password, Username = model.Username };
    }
    
    public User WriteItem(IUser model)
    {
        return new User { Id = model.Id, Password = model.Password, Username = model.Username };
    }
}