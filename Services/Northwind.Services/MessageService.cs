using Northwind.Services.Interfaces;
using Prism.Dialogs;

namespace Northwind.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
