using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
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
        private PoliticService PoliticService;

        public LoanService(IDBContext<Loan> loanContext, IDBContext<Interest> interestContext)
        {
            _loanContext = loanContext;
            PoliticService = new PoliticService(interestContext);
        }

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

        public async Task<IEnumerable<Loan>> GetLoanToAnalizeAsync()
        {
            List<ScanCondition> conditions = new List<ScanCondition>
            {
                new ScanCondition("Status", ScanOperator.Equal, LoanStatus.Processing )
            };

            return await _loanContext.GetItems(conditions);   
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

                if (!PoliticService.ValidateAgePolitic(loan.BirthDate))
                {
                    loan.Result = LoanResult.Refused;
                    loan.RefusedPolicity = "Age";
                }

                int score;

                bool scorePoliticApprove = PoliticService.ValidateScorePolitic(loan.CPF, out score);
                
                if (!scorePoliticApprove)
                {
                    loan.Result = LoanResult.Refused;
                    loan.RefusedPolicity = "Score";
                }

                int approvedTerms;

                bool commitmentPolicitApprove = PoliticService.ValidateCommitmentPolitic(loan, score, out approvedTerms);
                
                if (!commitmentPolicitApprove)
                {
                    loan.Result = LoanResult.Refused;
                    loan.RefusedPolicity = "Commitment";
                }
                else
                {
                    loan.Terms = approvedTerms;
                }

                SaveLoanAsync(loan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
