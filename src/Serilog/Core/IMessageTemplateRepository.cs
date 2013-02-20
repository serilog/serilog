namespace Serilog.Core
{
    interface IMessageTemplateRepository
    {
        MessageTemplate GetParsedTemplate(string messageTemplate);
    }
}