namespace Bookify.Application;

public interface IEmailService {
    Task SendAsync(string recipient, string subject, string body);
}