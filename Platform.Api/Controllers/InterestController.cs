using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DataAccess;
using Common.Domain;
using Common.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Api.Code;
using Platform.Api.Models.Interest;

namespace Platform.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly IDBContext<Interest> _interestContext;

        public InterestController(IDBContext<Interest> interestContext)
        {
            _interestContext = interestContext;
        }

        [HttpPost]
        public object Post(InterestModel model)
        {
            try
            {
                if (!model.MaxScore.HasValue)
                {
                    model.MaxScore = 1000;
                }

                Interest interest = Mapper.FromTo<InterestModel, Interest>(model);

                interest.Id = Guid.NewGuid().ToString();

                new InterestService(_interestContext).SaveInterestAsync(interest);

                return new { Id = interest.Id };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}