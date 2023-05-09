using Deadliner.Api.Models;

namespace Deadliner.Storage.EF.Mappers;

public interface IMapper<T, TU> where T : IObject
{
    T ReadItem(TU model);
    TU WriteItem(T model);
}