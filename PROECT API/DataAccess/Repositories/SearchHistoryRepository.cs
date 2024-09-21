using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SearchHistoryRepository : RepositoryBase<SearchHistory>, ISearchHistoryRepository
    {
        public SearchHistoryRepository(MarketpalceContext repositoryContext)
            : base(repositoryContext)
        {
        }

    }
}
