using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Models
{
    public class Utenti
    {
        public int IdUtente { get; set; }
        [DisplayName("Usename")]
        [Required(ErrorMessage = "Lo Username è obbligatorio")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Il Nome deve essere compreso tra 3 e 20 caratteri")]
        public string Usename { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "La Password è obbligatoria")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Il Nome deve essere compreso tra 3 e 20 caratteri")]
        public string Password { get; set; }
        public string Ruolo { get; set; }
    }
}