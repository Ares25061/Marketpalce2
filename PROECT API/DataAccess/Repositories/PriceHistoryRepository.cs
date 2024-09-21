using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PriceHistoryRepository : RepositoryBase<PriceHistory>, IPriceHistoryRepository
    {
        public  PriceHistoryRepository(MarketpalceContext repositoryContext)
            : base(repositoryContext)
        {
        }

    }
}
