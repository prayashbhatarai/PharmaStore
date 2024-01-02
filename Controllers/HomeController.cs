using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Controllers
{
    [Authorize(Roles = "Customer")]
    public class HomeController : Controller
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMedicineCategoryService _medicineCategoryService;

        public HomeController(IStockRepository stockRepository, IMedicineCategoryService medicineCategoryService)
        {
            _stockRepository = stockRepository;
            _medicineCategoryService = medicineCategoryService;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            var stock = _stockRepository.GetQueryable().Include(x => x.Medicine).Take(6).ToList();
            return View(stock);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}