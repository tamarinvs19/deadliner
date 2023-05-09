using Deadliner.Api.Models;
using Deadliner.Storage.EF.ModelsDB;
using Deadliner.Utils;

namespace Deadliner.Storage.EF.Mappers;

public class LocalEventMapper : IMapper<ILocalEvent, LocalEvent>
{
    public ILocalEvent ReadItem(LocalEvent model)
    {
        ILocalAction? parent = null;
        if (model.IdNavigation.ParentNavigation?.LocalTask is not null)
        {
            parent = new LocalTaskMapper().ReadItem(model.IdNavigation.ParentNavigation?.LocalTask!);
        }

        return new Models.LocalEvent(
            model.Id,
            model.IdNavigation.Title,
            model.IdNavigation.Description,
            model.DateTime,
            new GroupMapper().ReadItem(model.IdNavigation.DgroupNavigation),
            StateIdTransformer.GetState(model.IdNavigation.State),
            parent
        );
    }
    
    public LocalEvent WriteItem(ILocalEvent model)
    {
        var localAction = new LocalAction
        {
            Id = model.Id,
            State = StateIdTransformer.GetStateId(model.State),
            Parent = model.Parent?.Id,
            Dgroup = model.Group.Id,
            Title = model.Title,
            Description = model.Description,
            Type = 1
        };
        var localEvent = new LocalEvent
        {
            Id = model.Id,
            DateTime = model.DateTime,
            IdNavigation = localAction
        };
        return localEvent;
    }
}