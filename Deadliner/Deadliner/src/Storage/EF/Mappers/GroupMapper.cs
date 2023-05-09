using Deadliner.Api.Models;
using Deadliner.Storage.EF.ModelsDB;

namespace Deadliner.Storage.EF.Mappers;

public class GroupMapper : IMapper<IGroup, Group>
{
    public IGroup ReadItem(Group model)
    {
        return new Models.Group(
            model.Id,
            model.Title,
            model.Description,
            model.AccessKey,
            new UserMapper().ReadItem(model.OwnerNavigation),
            new SuperGroupMapper().ReadItem(model.SuperGroupNavigation)
        );
    }
    
    public Group WriteItem(IGroup model)
    {
        return new Group {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            AccessKey = model.AccessKey,
            Owner = model.Owner.Id,
            SuperGroup = model.SuperGroup.Id
        };
    }
}