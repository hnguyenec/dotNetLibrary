using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models.Branch;
using LibraryData;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BranchController : Controller
    {
        private ILibraryBranch _branchSvc;
        public BranchController(ILibraryBranch branchSvc)
        {
            _branchSvc = branchSvc;
        }

        public IActionResult Index()
        {
            var branchModels = _branchSvc.GetAll().Select(br => new BranchDetailModel
            {
                Id = br.Id,
                BranchName = br.Name,
                NumberOfAssets = _branchSvc.GetAssetCount(br.Id),
                NumberOfPatrons = _branchSvc.GetPatronCount(br.Id),
                IsOpen = _branchSvc.IsBranchOpen(br.Id)
            }).ToList();

            var model = new BranchIndexModel()
            {
                Branches = branchModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var branch = _branchSvc.Get(id);
            var model = new BranchDetailModel
            {
                BranchName = branch.Name,
                Description = branch.Description,
                Address = branch.Address,
                Telephone = branch.Telephone,
                BranchOpenedDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfPatrons = _branchSvc.GetPatronCount(id),
                NumberOfAssets = _branchSvc.GetAssetCount(id),
                TotalAssetValue = _branchSvc.GetAssetsValue(id),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _branchSvc.GetBranchHours(id)
            };

            return View(model);
        }

    }
}
