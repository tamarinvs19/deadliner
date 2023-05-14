namespace Deadliner.Api.Utils;

public interface ITimeProvider
{
     DateTime Now();
     DateTime Today();
}