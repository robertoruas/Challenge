using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain
{
    public class Interest
    {
        public string Id { get; set; }

        public int MaxScore { get; set; }

        public int MinScore { get; set; }

        public int Terms { get; set; }

        public decimal Value { get; set; }
    }
}
