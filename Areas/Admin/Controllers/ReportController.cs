using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Services.Implementation;
using PharmaStore.Modules.Services.Interface;
using System.Drawing.Printing;

namespace PharmaStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IStockRepository _stockRepository;
        private readonly IToastrHelper _toastrHelper;

        public ReportController(IStockService stockService, IStockRepository stockRepository, IToastrHelper toastrHelper)
        {
            _stockService = stockService;
            _stockRepository = stockRepository;
            _toastrHelper = toastrHelper;
        }

        public IActionResult ExpiryReport(string? search, int page = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            var stock = PaginatedList<Stock>.Create(_stockRepository.GetQueryable().Include(x => x.Medicine).Include(x => x.Supplier).OrderBy(x => x.StockExpiryDate), page, pagesize);
            if (!String.IsNullOrEmpty(search))
            {
                stock = PaginatedList<Stock>.Create(_stockRepository.GetQueryable().Include(x => x.Medicine).Include(x => x.Supplier).OrderBy(x => x.StockExpiryDate).Where(x =>
                EF.Functions.Like(x.StockBatchNumber, $"%{search}%")
                || EF.Functions.Like(x.Medicine.MedicineName, $"%{search}%")
                || EF.Functions.Like(x.Supplier.SupplierName, $"%{search}%")), page, pagesize);
            }
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            return View(stock);
        }
    }
}
