using Newtonsoft.Json;
using Platform.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Api.Models
{
    public class LoanModel
    {
        [Required(ErrorMessage = "Informe o Nome.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o CPF."), CPFValidator]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe a Data de Nascimento.")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Informe a Quantia solicitada.")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "Informe que Quantidade de Parcelas.")]
        public int? Terms { get; set; }

        [Required(ErrorMessage = "Informe o Valor de Renda Mensal.")]
        public decimal? Income { get; set; }
    }
}