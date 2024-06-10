namespace MVC_FinalProject.ViewModels.Contacts
{
    public class ContactTableVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SendDate { get; set; }
    }
}
