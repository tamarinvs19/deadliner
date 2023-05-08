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
        LocalAction? baseParent = null;
        if (model.Parent is ILocalTask task)
        {
            baseParent = new LocalAction
            {
                Id = task.Id,
                Description = task.Description,
                DgroupNavigation = new GroupMapper().WriteItem(task.Group),
                Parent = task.Parent?.Id,
                Title = task.Title,
                State = StateIdTransformer.GetStateId(task.State)
            };
            var parent = new LocalTaskMapper().WriteItem(task);
            parent.IdNavigation = baseParent;
        }
        var localAction = new LocalAction
        {
            Id = model.Id,
            State = StateIdTransformer.GetStateId(model.State),
            ParentNavigation = baseParent,
            DgroupNavigation = new GroupMapper().WriteItem(model.Group),
            Title = model.Title,
            Description = model.Description,
            Type = 1
        };
        var localTask = new LocalTask()
        {
            Id = model.Id,
            Deadline = model.Deadline,
            CreationDateTime = model.CreationDateTime,
            IdNavigation = localAction
        };
        return localTask;
    }
}