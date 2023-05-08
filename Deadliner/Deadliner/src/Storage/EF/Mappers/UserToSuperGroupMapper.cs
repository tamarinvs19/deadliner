using Deadliner.Api.Models.Relationships;
using Deadliner.Storage.EF.ModelsDB;

namespace Deadliner.Storage.EF.Mappers;

public class UserToSuperGroupMapper : IMapper<IUserToSuperGroup, UserToSuperGroup>
{
    public IUserToSuperGroup ReadItem(UserToSuperGroup model)
    {
        return new Models.UserToSuperGroup(
            model.Id,
            new UserMapper().ReadItem(model.User),
            new SuperGroupMapper().ReadItem(model.SuperGroup)
        );
    }
    
    public UserToSuperGroup WriteItem(IUserToSuperGroup model)
    {
        return new UserToSuperGroup {
            Id = model.Id,
            UserId = model.User.Id,
            SuperGroupId = model.SuperGroup.Id
        };
    }
}