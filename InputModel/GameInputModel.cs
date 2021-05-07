using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 a 100 caracteres.")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 1 a 100 caracteres.")]
        public string Producer { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser no mínimo 1 real e no máximo 1000 reais.")]
        public double Price { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "A descricao deve conter entre 10 a 255 caracteres.")]
        public string Description { get; set; }
    }
}
