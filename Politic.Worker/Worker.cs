using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.DataAccess;
using Common.Domain;
using Common.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Politic.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private LoanService _service;

        public Worker(ILogger<Worker> logger, IDBContext<Loan> loanContext, IDBContext<Interest> interestContext)
        {
            _logger = logger;
            _service = new LoanService(loanContext, interestContext);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                try
                {
                    List<Loan> loans = _service.GetLoanToAnalizeAsync().Result.ToList();

                    foreach (Loan item in loans)
                    {
                        _service.AuthorizeLoan(item);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Worker error: {error}", ex);
                }
            }
        }
    }
}
