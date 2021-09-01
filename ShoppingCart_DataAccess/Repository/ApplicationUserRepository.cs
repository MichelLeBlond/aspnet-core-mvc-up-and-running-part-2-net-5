using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart_DataAccess.Data;
using ShoppingCart_DataAccess.Repository.IRepository;
using ShoppingCart_Models;

namespace ShoppingCart_DataAccess.Repository
{
   public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
    
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
      
    }
}
