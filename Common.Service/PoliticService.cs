using Common.DataAccess;
using Common.Domain;
using Common.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Service
{
    public class PoliticService
    {
        private InstalmentService InstalmentService;

        public PoliticService(IDBContext<Interest> interestContext)
        {
            InstalmentService = new InstalmentService(interestContext);
        }

        public bool ValidateCommitmentPolitic(Loan loan, int score, out int approvedTerms)
        {
            bool isApproved = false;

            approvedTerms = loan.Terms;

            decimal commitment = NoverdeService.GetCommitment(loan.CPF);

            decimal installment = InstalmentService.CalculateInstallments(loan.Amount, loan.Terms, commitment, score);

            while (loan.Terms <= 12)
            {
                if (installment < GetAvailableValue(loan.Income, commitment))
                {
                    loan.Terms += 3;
                    
                    if (loan.Terms < 12)
                    {
                        installment = InstalmentService.CalculateInstallments(loan.Amount, loan.Terms, commitment, score);
                    }
                }
                else
                {
                    isApproved = true;
                    approvedTerms = loan.Terms;
                }
            }

            return isApproved;
        }

        private decimal GetAvailableValue(decimal income, decimal commitment)
        {
            return income * commitment;
        }

        internal bool ValidateScorePolitic(string cpf, out int score)
        {
            score = NoverdeService.GetScore(cpf);

            return score > CommonHelper.MinScoreAccepted;
        }

        internal bool ValidateAgePolitic(DateTime birthDate)
        {
            return ((DateTime.Now.Date - birthDate.Date).TotalDays / 365) > CommonHelper.MinAgeAccepted;
        }
    }
}
