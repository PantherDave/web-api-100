namespace Shows.Api.Shows;

public interface IProvideUniqueIds
{
    Guid GetGuid();
}

public class SystemUniqueIdProvider : IProvideUniqueIds
{
    Guid IProvideUniqueIds.GetGuid()
    {
        return Guid.NewGuid();
    }
}
