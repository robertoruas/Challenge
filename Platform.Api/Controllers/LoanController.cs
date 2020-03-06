using System;
using Common.DataAccess;
using Common.Domain;
using Common.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Api.Code;
using Platform.Api.Models;

namespace Platform.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly IDBContext<Loan> _loanContext;

        public LoanController(IDBContext<Loan> loanContext)
        {
            _loanContext = loanContext;
        }

        [HttpPost]
        public object Post(LoanModel model)
        {
            try
            {
                Loan loan = Mapper.FromTo<LoanModel, Loan>(model);

                loan.Id = Guid.NewGuid().ToString();
                loan.Status = LoanStatus.Processing;

                new LoanService(_loanContext).SaveLoanAsync(loan);

                return new { Id = loan.Id };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public object Get([FromQuery]string id)
        {
            try
            {
                Loan loan = new LoanService(_loanContext).GetLoanAsync(id).Result;

                LoanGetStatusModel model = new LoanGetStatusModel
                {
                    Id = loan.Id,
                    Status = loan.Status.ToString(),
                    Result = loan.Status == LoanStatus.Processing ? null : loan.Result.ToString(),
                    Refused_Policity = loan.Status == LoanStatus.Processing ? null : loan.RefusedPolicity.ToString(),
                    Amout = loan.Status == LoanStatus.Processing ? null : (decimal?)loan.Amount,
                    Terms = loan.Status == LoanStatus.Processing ? null : (int?)loan.Terms
                };

                return JsonConvert.SerializeObject(model);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}