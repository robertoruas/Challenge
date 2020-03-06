using Common.DataAccess;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politic.Worker.Service
{
    public class PoliticService
    {
        private IDBContext<Politics> _politicsContext;

        public PoliticService(IDBContext<Politics> politicsContext)
        {
            _politicsContext = politicsContext;
        }

        public async Task<Politics> GetPoliticsAsync(string id)
        {
            try
            {
                return await _politicsContext.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar obter os dados do id {id}! Error: {ex}");
            }
        }

        public async void SaveLoanAsync(Politics politics)
        {
            try
            {
                await _politicsContext.SaveAsync(politics);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar gravar os dados. Error: {ex}");
            }
        }

    }
}
