using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaStore.Areas.Admin.Models.ViewModel;
using PharmaStore.Data.Repositories.Interface;
using System.Data;

namespace PharmaStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IStockRepository _stockRepository;
        public readonly IMedicineRepository _medicineRepository;
        public readonly IMedicineCategoryRepository _medicinecategoryRepository;

        public DashboardController(IOrderRepository orderRepository, IStockRepository stockRepository, IMedicineRepository medicineRepository, IMedicineCategoryRepository medicinecategoryRepository)
        {
            _orderRepository = orderRepository;
            _stockRepository = stockRepository;
            _medicineRepository = medicineRepository;
            _medicinecategoryRepository = medicinecategoryRepository;
        }

        public IActionResult Index()
        {
            DashboardViewModel dvm = new DashboardViewModel();
            dvm.NumberOfMedicine = _medicineRepository.Count();
            dvm.NumberOfMedicineCategory = _medicinecategoryRepository.Count();
            dvm.NumberOfOrder = _orderRepository.Count();
            dvm.NumberOfStock = _stockRepository.Count();
            ViewBag.RecentOrder = _orderRepository.GetQueryable().Include(x => x.Stock).Include(x => x.Stock.Medicine).Include(x => x.ApplicationUser).Where(x => x.OrderStatus== Data.Entities.OrderStatus.Pending).OrderBy(x=>x.OrderDate).Take(5).ToList();
            return View(dvm);
        }
        public IActionResult Test()
        {
            return View();
        }
    }
}
