using Amazon.DynamoDBv2.DataModel;
using System;

namespace Common.Domain
{
    public class Loan 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CPF { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal Amount { get; set; }

        public int Terms { get; set; }

        public decimal Income { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("Index1")]
        public LoanStatus Status { get; set; }

        public LoanResult Result { get; set; }

        public string RefusedPolicity { get; set; }
    }
}
