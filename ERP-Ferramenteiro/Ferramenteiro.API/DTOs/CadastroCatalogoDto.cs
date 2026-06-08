using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_Ferramenteiro.Application.DTOs
{
    public class CadastroCatalogoDto
    {
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O nome do modelo é obrigatório.")]
        public string NomeModelo { get; set; }

        public decimal? PrecoHora { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço por dia deve ser maior que zero.")]
        public decimal PrecoDia { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "A lista de patrimônios exige registro unitário e não pode ser vazia.")]
        public List<string> Patrimonios { get; set; }
    }
}