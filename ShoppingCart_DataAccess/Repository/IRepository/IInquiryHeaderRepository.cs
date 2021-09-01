using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCart_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_DataAccess.Repository.IRepository
{
   public interface IInquiryHeaderRepository: IRepository<InquiryHeader>
    {
        void Update(InquiryHeader obj);
     
    }
 
}
