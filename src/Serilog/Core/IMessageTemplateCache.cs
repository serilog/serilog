namespace Serilog.Core
{
    public interface IMessageTemplateCache
    {
        MessageTemplate GetParsedTemplate(string messageTemplate);
    }
}