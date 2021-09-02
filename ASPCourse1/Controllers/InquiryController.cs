using Microsoft.AspNetCore.Mvc;
using ShoppingCart_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class InquiryController : Controller
    {
        private readonly IInquiryHeaderRepository _inqHRepo;
        private readonly IInquiryDetailRepository _inqDRepo;
        
        public InquiryController(IInquiryHeaderRepository inqHRepo,IInquiryDetailRepository inqDRepo)
        {
            _inqDRepo = inqDRepo;
            _inqHRepo = inqHRepo;

        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
