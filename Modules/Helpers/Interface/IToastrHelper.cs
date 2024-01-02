using Microsoft.AspNetCore.Mvc;
using PharmaStore.Modules.Models.Toastr;

namespace PharmaStore.Modules.Helpers.Interface
{
    public interface IToastrHelper
    {
        void SendMessage(Controller controller, string title, string message, MessageType messageType);
        void AddMessage(string title, string message, MessageType messageType);
        void Send(Controller controller);
    }
}
