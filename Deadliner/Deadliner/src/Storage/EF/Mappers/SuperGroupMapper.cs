using Deadliner.Api.Models;
using Deadliner.Storage.EF.ModelsDB;

namespace Deadliner.Storage.EF.Mappers;

public class SuperGroupMapper : IMapper<ISuperGroup, SuperGroup>
{
    public ISuperGroup ReadItem(SuperGroup model)
    {
        return new Models.SuperGroup(
            model.Id,
            model.Title,
            model.Description,
            model.AccessKey,
            new UserMapper().ReadItem(model.OwnerNavigation)
        );
    }
    
    public SuperGroup WriteItem(ISuperGroup model)
    {
        return new SuperGroup {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            AccessKey = model.AccessKey,
            OwnerNavigation = new UserMapper().WriteItem(model.Owner)
        };
    }
}