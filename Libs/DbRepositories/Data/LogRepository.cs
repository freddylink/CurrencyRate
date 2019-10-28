using DbRepositories.Data.Object;
using System;

namespace DbRepositories.Data
{
    public class LogRepository : Repository<Log>
    {
        public LogRepository(CurrencyRateDbContext context)
            : base(context)
        { }

        public void AddException(string message)
        {
            Add(new Log() {
                Message = message,
                Date = DateTime.Today
            });
        }
    }
}
