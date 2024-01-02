using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Models.Toastr;

namespace PharmaStore.Modules.Helpers.Implementation
{
    public class ToastrHelper : IToastrHelper
    {
        private const MessageType DEFAULT_MESSAGE_TYPE = MessageType.Info;

        private List<Toastr> toastrmessages = new List<Toastr>();

        public void SendMessage(Controller controller, string title, string message, MessageType messageType = DEFAULT_MESSAGE_TYPE)
        {
            try
            {
                Toastr toastr = new Toastr(title, message, messageType);
                List<Toastr> toastrmessage = new List<Toastr>
                {
                    toastr
                };
                controller.TempData["messages"] = JsonConvert.SerializeObject(toastrmessage);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error Sending Message: {ex.Message}");
            }
        }

        public void AddMessage(string title, string message, MessageType messageType = DEFAULT_MESSAGE_TYPE)
        {
            try
            {
                Toastr toastr = new Toastr(title, message, messageType);
                toastrmessages.Add(toastr);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error Adding Message: {ex.Message}");
            }
        }

        public void Send(Controller controller)
        {
            try
            {
                controller.TempData["messages"] = JsonConvert.SerializeObject(toastrmessages);
                toastrmessages.Clear();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error Sending Message: {ex.Message}");
            }
        }
    }
}
