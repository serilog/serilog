namespace Opi.Core
{
    interface IMessageTemplateRepository
    {
        MessageTemplate GetParsedTemplate(string messageTemplate);
    }
}