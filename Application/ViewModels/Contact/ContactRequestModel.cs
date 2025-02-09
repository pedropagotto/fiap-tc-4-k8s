namespace Application.ViewModels.Contact
{
    public class ContactRequestModel
    {
        /// <summary>
        /// Nome do contato.
        /// </summary>
        /// <example>Sanders</example>
        public string Name { get; set; }
        public string Ddd { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
