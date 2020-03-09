using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Common.DataAccess;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public class InterestService
    {
        private IDBContext<Interest> _interestContext;

        public InterestService(IDBContext<Interest> interestContext)
        {
            _interestContext = interestContext;
        }

        public async void SaveInterestAsync(Interest interest)
        {
            try
            {
                await _interestContext.SaveAsync(interest);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível registrar a Taxa de Juros. Error: {ex}");
            }
        }

        public Interest GetByScoreAndTerms(int score, int terms)
        {

            var opConfig = new DynamoDBOperationConfig();
            opConfig.QueryFilter = new List<ScanCondition>();

            List<ScanCondition> conditions = new List<ScanCondition>
            {
                new ScanCondition("Terms", ScanOperator.Equal, terms )
            };

            var interest = _interestContext.GetItems(conditions).Result;

            return interest.FirstOrDefault(x => score <= x.MaxScore && score >= x.MinScore);
        }
    }
}
