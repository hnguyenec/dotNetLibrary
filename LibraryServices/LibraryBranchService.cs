using System.Collections.Generic;
using System.Linq;
using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices
{
    public class LibraryBranchService : ILibraryBranch
    {
        private readonly LibraryContext _context;

        public LibraryBranchService(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranches
                .Include(b => b.Patrons)
                .Include(b => b.LibraryAssets);
        }

        public IEnumerable<Patron> GetPatrons(int branchId)
        {
            return _context.LibraryBranches
                .Include(b => b.Patrons)
                .FirstOrDefault(b => b.Id == branchId)
                .Patrons;
        }

        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            return _context.LibraryBranches
                .Include(b => b.LibraryAssets)
                .FirstOrDefault(b => b.Id == branchId)
                .LibraryAssets;
        }

        public LibraryBranch Get(int branchId)
        {
            return GetAll()
                .FirstOrDefault(b => b.Id == branchId);
        }

        public void Add(LibraryBranch newBranch)
        {
            _context.Add(newBranch);
            _context.SaveChanges();
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var hours = _context.BranchHours
                .Include(bh => bh.Branch)
                .Where(bh => bh.Branch.Id == branchId);

            var displayHours = DateHelpers.HumanizeBusinessHours(hours);

            return displayHours;
        }

        //TODO: Implement
        public bool IsBranchOpen(int branchId)
        {
            return true;
        }

        public int GetAssetCount(int branchId)
        {
            return Get(branchId)?.LibraryAssets?.Count() ?? 0;
        }

        public int GetPatronCount(int branchId)
        {
            return Get(branchId)?.Patrons?.Count() ?? 0;
        }

        public decimal GetAssetsValue(int id)
        {
            var assetValue = GetAssets(id).Select(a => a.Cost);
            return assetValue.Sum();
        }
    }
}