namespace Bookify.Application;


// Benefits of this interface:
// 1. It allows us to use a mock for testing
public interface IDateTimeProvider{
    DateTime UtcNow { get; }
}
