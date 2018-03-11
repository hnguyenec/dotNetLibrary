using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models.Patron;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class PatronController: Controller
    {
        private readonly IPatron _patron;
        
        public PatronController(IPatron patron)
        {
            _patron = patron;
        }

        public IActionResult Index()
        {
            var allPatrons = _patron.GetAll();

            var patronModels = allPatrons.Select(p => new PatronDetailModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                Address = p.Address,
                MemberSince = p.LibraryCard.Created,
                Telephone = p.TelephoneNumber,
                HomeLibrary = p.HomeLibraryBranch.Name,
                OverdueFees = p.LibraryCard.Fees,
                AssetsCheckedOut = _patron.GetCheckouts(p.Id),
                CheckoutHistory = _patron.GetCheckoutHistories(p.Id),
                Holds = _patron.GetHolds(p.Id)
            }).ToList();

            var model = new PatronIndexModel()
            {
                Patrons = patronModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var p = _patron.Get(id);

            var model = new PatronDetailModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                Address = p.Address,
                MemberSince = p.LibraryCard.Created,
                Telephone = p.TelephoneNumber,
                HomeLibrary = p.HomeLibraryBranch.Name,
                OverdueFees = p.LibraryCard.Fees,
                AssetsCheckedOut = _patron.GetCheckouts(p.Id).ToList() ?? new List<Checkout>(),
                CheckoutHistory = _patron.GetCheckoutHistories(p.Id),
                Holds = _patron.GetHolds(p.Id)
            };
            return View(model);
        }
       
    }
}
