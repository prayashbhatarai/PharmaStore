namespace PharmaStore.Modules.Models.Toastr
{
    public class Toastr
    {
        public Toastr(string title, string message, MessageType messageType)
        {
            this.title = title;
            this.message = message;
            this.messageType = messageType;
        }

        public string title { get; set; } = String.Empty;
        public string message { get; set; } = String.Empty;
        public MessageType messageType { get; set; }
    }
}