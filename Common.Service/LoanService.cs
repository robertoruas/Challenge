using Common.DataAccess;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Service
{
    public class LoanService
    {
        private IDBContext<Loan> _loanContext;

        public LoanService(IDBContext<Loan> loanContext)
        {
            _loanContext = loanContext;
        }

        public async Task<Loan> GetLoanAsync(string id)
        {
            try
            {
                return await _loanContext.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar obter os dados do id {id}! Error: {ex}");
            }
        }

        public async Task<List<Loan>> GetLoanToAnalizeAsync()
        {
            
        }

        public async void SaveLoanAsync(Loan loan)
        {
            try
            {
                await _loanContext.SaveAsync(loan);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar gravar os dados. Error: {ex}");
            }
        }

        public void AuthorizeLoan(Loan loan)
        {
            try
            {
                loan.Status = LoanStatus.Completed;

                if (((DateTime.Now.Date - loan.BirthDate.Date).TotalDays / 365) < 18)
                {
                    loan.Result = LoanResult.Refused;
                    loan.RefusedPolicity = "Age";

                    return;
                }

                int score = NoverdeService.GetScore(loan.CPF);

                if (score < 600)
                {
                    loan.Result = LoanResult.Refused;
                    loan.RefusedPolicity = "Score";

                    return;
                }

                decimal commitment = NoverdeService.GetCommitment(loan.CPF);

                decimal installment = CalculateInstallments(ref loan, commitment, score);

                while (loan.Terms <= 12)
                {
                    if (installment > GetAvailableValue(loan.Income, commitment))
                    {
                        loan.Terms += 3;
                        loan.Result = LoanResult.Refused;
                        loan.RefusedPolicity = "Commitment";
                    }
                }

                if (loan.Result != LoanResult.Refused)
                {
                    loan.Result = LoanResult.Approved;
                }

                SaveLoanAsync(loan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal CalculateInstallments(ref Loan loan, decimal commitment, int score)
        {
            decimal availableValue = GetAvailableValue(loan.Income, commitment);

            double i = (double)GetInterestTax(score, loan.Terms);

            decimal installment = loan.Amount * (decimal)((Math.Pow(1 - i, loan.Terms) * i) / ((Math.Pow(1 - i, loan.Terms) - 1)));

            return installment;
        }

        private static decimal GetAvailableValue(decimal income, decimal commitment)
        {
            return income * commitment;
        }

        private double GetInterestTax(int score, int terms)
        {
            double interest = 0;

            if (score >= 900)
            {
                switch (terms)
                {
                    case 6:
                        interest = 0.039f;
                        break;
                    case 9:
                        interest = 0.042f;
                        break;
                    case 12:
                        interest = 0.045f;
                        break;
                    default:
                        break;
                }
            }
            else if (score <= 899 && score >= 800)
            {
                switch (terms)
                {
                    case 6:
                        interest = 0.047f;
                        break;
                    case 9:
                        interest = 0.050f;
                        break;
                    case 12:
                        interest = 0.053f;
                        break;
                    default:
                        break;
                }
            }
            else if (score <= 799 && score >= 700)
            {
                switch (terms)
                {
                    case 6:
                        interest = 0.055f;
                        break;
                    case 9:
                        interest = 0.058f;
                        break;
                    case 12:
                        interest = 0.061f;
                        break;
                    default:
                        break;
                }
            }
            else if (score <= 699 && score >= 600)
            {
                switch (terms)
                {
                    case 6:
                        interest = 0.064f;
                        break;
                    case 9:
                        interest = 0.067f;
                        break;
                    case 12:
                        interest = 0.069f;
                        break;
                    default:
                        break;
                }
            }

            return interest;
        }
    }
}
