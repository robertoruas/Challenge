using Common.DataAccess;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Service
{
    public class InstalmentService
    {
        private InterestService InterestService;

        public InstalmentService(IDBContext<Interest> interestContext)
        {
            InterestService = new InterestService(interestContext);
        }

        public decimal CalculateInstallments(decimal amount, int terms, decimal commitment, int score)
        {
            double i = (double)InterestService.GetByScoreAndTerms(score, terms).Value / 100;

            decimal installment = amount * ((decimal)((Math.Pow(1 + i, terms) * i) / ((Math.Pow(1 + i, terms) - 1))));

            return installment;
        }
    }
}
