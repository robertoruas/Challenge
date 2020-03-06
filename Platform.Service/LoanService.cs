using Common.DataAccess;
using Common.Domain;
using System;
using System.Threading.Tasks;

namespace Platform.Service
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


    }
}
