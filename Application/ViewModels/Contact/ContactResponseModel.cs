namespace Application.ViewModels.Contact
{
    public class ContactResponseModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Ddd { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
