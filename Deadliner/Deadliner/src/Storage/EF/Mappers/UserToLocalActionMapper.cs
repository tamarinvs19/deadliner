using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Storage.EF.ModelsDB;
using Deadliner.Utils;

namespace Deadliner.Storage.EF.Mappers;

public class UserToLocalActionMapper : IMapper<IUserToLocalAction, UserToLocalAction>
{
    public IUserToLocalAction ReadItem(UserToLocalAction model)
    {
        ILocalAction localAction;
        if (model.LocalAction == null)
        {
            var _context = MainContainer.Context();
            try
            {
                localAction = _context.LocalEvents.Get(model.LocalActionId);
            }
            catch (Exception e)
            {
                localAction = _context.LocalTasks.Get(model.LocalActionId);
            }
        }
        else
        {
            localAction = _createAction(model.LocalAction);
        }
        return new Models.UserToLocalAction(
            model.Id,
            new UserMapper().ReadItem(model.User),
            localAction,
            StateIdTransformer.GetState(model.StateId)
        );
    }

    private static ILocalAction _createAction(LocalAction action)
    {
        if (action.LocalEvent is not null)
        {
            return new LocalEventMapper().ReadItem(action.LocalEvent);
        }
        return new LocalTaskMapper().ReadItem(action.LocalTask!);
    }
    
    public UserToLocalAction WriteItem(IUserToLocalAction model)
    {
        return new UserToLocalAction {
            Id = model.Id,
            UserId = model.User.Id,
            StateId = StateIdTransformer.GetStateId(model.State),
            LocalActionId = model.LocalAction.Id
        };
    }
}