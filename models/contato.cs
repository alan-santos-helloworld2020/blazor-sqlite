using System.ComponentModel.DataAnnotations;
namespace BlazorApp.Models
{
    public class Contato
    {

        
        public int id { get; set; } = 0;
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        public string telefone { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string email { get; set; } = string.Empty;

    }

}