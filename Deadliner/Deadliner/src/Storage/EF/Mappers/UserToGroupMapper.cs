using Deadliner.Api.Models.Relationships;
using Deadliner.Storage.EF.ModelsDB;

namespace Deadliner.Storage.EF.Mappers;

public class UserToGroupMapper : IMapper<IUserToGroup, UserToGroup>
{
    public IUserToGroup ReadItem(UserToGroup model)
    {
        return new Models.UserToGroup(
            model.Id,
            new UserMapper().ReadItem(model.User),
            new GroupMapper().ReadItem(model.Group)
        );
    }
    
    public UserToGroup WriteItem(IUserToGroup model)
    {
        return new UserToGroup {
            Id = model.Id,
            User = new UserMapper().WriteItem(model.User),
            Group = new GroupMapper().WriteItem(model.Group)
        };
    }
}