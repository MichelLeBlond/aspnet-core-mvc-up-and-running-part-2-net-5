﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShoppingCart_DataAccess.Data;
using ShoppingCart_Models;
using ShoppingCart_Models.ViewModels;
using ShoppingCart_DataAccess.Repository.IRepository;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly IProductRepository _prodRepo;
        private readonly IInquiryHeaderRepository _inqHRepo;
        private readonly IInquiryDetailRepository _inqDRepo;
        private readonly IApplicationUserRepository _userRepo;



        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController( IInquiryDetailRepository inqDRepo, IInquiryHeaderRepository inqHRepo, IProductRepository prodRepo, IApplicationUserRepository userRepo, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            
            _inqHRepo = inqHRepo;
            _inqDRepo = inqDRepo;
            _prodRepo = prodRepo;
            _userRepo = userRepo;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }


        public IActionResult Index()
        {
            List<ShoppingCart_Models.ShoppingCart> shoppingCartList = new List<ShoppingCart_Models.ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //Session exists
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _prodRepo.GetAll(u => prodInCart.Contains(u.Id));
            return View(prodList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
      
            return RedirectToAction(nameof(Summary));
        }

     
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            List<ShoppingCart_Models.ShoppingCart> shoppingCartList = new List<ShoppingCart_Models.ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //Session exists
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _prodRepo.GetAll(u => prodInCart.Contains(u.Id));
            ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = _userRepo.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = prodList.ToList()
            };

            return View(ProductUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVM ProductUserVM)
        {
            var PathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                                                                 + "templates" + Path.DirectorySeparatorChar.ToString() +
                                                                 "Inquiry.html";

            var subject = "New Inquiry";
            string HtmlBody = "";
            using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
                HtmlBody = sr.ReadToEnd();
            }
            //Name: { 0}
            //Email: { 1}
            //Phone: { 2}
            //Products: {3}

            StringBuilder productListSB = new StringBuilder();
            foreach (var prod in ProductUserVM.ProductList)
            {
                productListSB.Append($" - Name: { prod.Name} <span style='font-size:14px;'> (ID: {prod.Id})</span><br />");
            }

            string messageBody = string.Format(HtmlBody,
                ProductUserVM.ApplicationUser.FullName,
                ProductUserVM.ApplicationUser.Email,
                ProductUserVM.ApplicationUser.PhoneNumber,
                productListSB.ToString());


            await _emailSender.SendEmailAsync(WC.EmailAdmin, subject, messageBody);

            return RedirectToAction(nameof(InquiryConfirmation));
        }

        public IActionResult InquiryConfirmation()
        {
            HttpContext.Session.Clear();
            return View();
        }



        public IActionResult Remove(int id)
        {
            List<ShoppingCart_Models.ShoppingCart> shoppingCartList = new List<ShoppingCart_Models.ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //Session exists
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart_Models.ShoppingCart>>(WC.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set(WC.SessionCart,shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
    }
}
