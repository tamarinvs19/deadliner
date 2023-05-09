using System.Diagnostics;
using Deadliner.Api.Models;
using Deadliner.Storage.EF.ModelsDB;
using Deadliner.Utils;

namespace Deadliner.Storage.EF.Mappers;

public class LocalTaskMapper : IMapper<ILocalTask, LocalTask>
{
    public ILocalTask ReadItem(LocalTask model)
    {
        ILocalAction? parent = null;
        if (model.IdNavigation.ParentNavigation?.LocalTask is not null)
        {
            parent = new LocalTaskMapper().ReadItem(model.IdNavigation.ParentNavigation?.LocalTask!);
        }
        return new Models.LocalTask(
            model.Id,
            model.IdNavigation.Title,
            model.IdNavigation.Description,
            model.CreationDateTime,
            model.Deadline,
            new GroupMapper().ReadItem(model.IdNavigation.DgroupNavigation),
            StateIdTransformer.GetState(model.IdNavigation.State),
            parent
        );
    }
    
    public LocalTask WriteItem(ILocalTask model)
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
        var localTask = new LocalTask
        {
            Id = model.Id,
            Deadline = model.Deadline,
            CreationDateTime = model.CreationDateTime,
            IdNavigation = localAction
        };
        return localTask;
    }
}