using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FileRepository : RepositoryBase<Models.File>, IFileRepository
    {
        public FileRepository(MarketpalceContext repositoryContext)
            : base(repositoryContext)
        {
        }

    }
}
