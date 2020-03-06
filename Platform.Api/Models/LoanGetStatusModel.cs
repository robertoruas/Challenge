using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Api.Models
{
    public class LoanGetStatusModel
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public string Result { get; set; }

        public string Refused_Policity { get; set; }

        public decimal? Amout { get; set; }

        public int? Terms { get; set; }
    }
}
