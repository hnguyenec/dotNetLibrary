using System.Collections.Generic;
using System.Linq;
using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices
{
    public class PatronService : IPatron
    {
        private readonly LibraryContext _context;

        public PatronService(LibraryContext context)
        {
            _context = context;
        }

        public Patron Get(int id)
        {
            return GetAll()
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Patron> GetAll()
        {
            return _context.Patrons
                .Include(p => p.LibraryCard)
                .Include(p => p.HomeLibraryBranch);
        }

        public void Add(Patron newPatron)
        {
            _context.Add(newPatron);
            _context.SaveChanges();
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistories(int patronId)
        {
            var cardId = GetPatronLibraryCardId(patronId);

            return _context.CheckoutHistories
                .Include(h => h.LibraryCard)
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryCard.Id == cardId)
                .OrderByDescending(h => h.CheckedOut);
        }

        public IEnumerable<Hold> GetHolds(int patronId)
        {
            var cardId = GetPatronLibraryCardId(patronId);
            return _context.Holds
                .Include(hold => hold.LibraryCard)
                .Include(hold => hold.LibraryAsset)
                .Where(hold => hold.LibraryCard.Id == cardId)
                .OrderByDescending(hold => hold.HoldPlaced);
        }

        public IEnumerable<Checkout> GetCheckouts(int patronId)
        {
            var cardId = GetPatronLibraryCardId(patronId);
            return _context.Checkouts
                .Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .Where(co => co.LibraryCard.Id == cardId);
        }

        private int GetPatronLibraryCardId(int patronId)
        {
            return _context.Patrons
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.Id == patronId)
                .LibraryCard.Id;
        }
    }
}