using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Api.Models.Interest
{
    public class InterestModel
    {
        public string Id { get; set; }

        public int? MaxScore { get; set; }

        public int MinScore { get; set; }

        public int Terms { get; set; }

        public decimal Value { get; set; }
    }
}
