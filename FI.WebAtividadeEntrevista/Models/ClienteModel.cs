using FI.WebAtividadeEntrevista.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    public class ClienteModel
    {
        public long Id { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Cidade { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required]
        [MaxLength(2)]
        public string Estado { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Nacionalidade { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        public string Telefone { get; set; }

        [Required]
        public string CPF { get; set; }

        public List<BeneficiarioModel> Beneficiarios { get; set; }
    }
}