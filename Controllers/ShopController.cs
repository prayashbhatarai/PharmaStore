using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Identity;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Models.Toastr;
using PharmaStore.Modules.Services.Implementation;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ShopController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        public readonly IStockRepository _stockRepository;
        public readonly IStockService _stockService;
        public readonly IMedicineCategoryService _medicineCategoryService;
        public readonly IOrderService _orderService;
        public readonly IOrderRepository _orderRepository;
        public readonly IFileHelper _fileHelper;
        public readonly IToastrHelper _toastrHelper;
        private readonly string uploadpath = "uploads\\prescription";

        public ShopController(UserManager<ApplicationUser> userManager, IStockRepository stockRepository, IStockService stockService, IMedicineCategoryService medicineCategoryService, IOrderService orderService, IOrderRepository orderRepository, IFileHelper fileHelper, IToastrHelper toastrHelper)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _stockService = stockService;
            _medicineCategoryService = medicineCategoryService;
            _orderService = orderService;
            _orderRepository = orderRepository;
            _fileHelper = fileHelper;
            _toastrHelper = toastrHelper;
        }

        public IActionResult Index(string search, Guid category)
        {
            ViewBag.Search = search;
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            var medicines = _stockRepository.GetQueryable().Include(x => x.Medicine).ToList();
            if (!String.IsNullOrEmpty(search) && category != Guid.Empty)
            {
                medicines = _stockRepository.GetQueryable()
                    .Include(x => x.Medicine)
                    .Include(x => x.Medicine.MedicineCategory)
                    .Where(x => EF.Functions.Like(x.Medicine.MedicineName, $"%{search}%") && x.Medicine.MedicineCategory.MedicineCategoryId == category)
                    .ToList();
            }
            if (!String.IsNullOrEmpty(search))
            {
                medicines = _stockRepository.GetQueryable()
                    .Include(x => x.Medicine)
                    .Where(x => EF.Functions.Like(x.Medicine.MedicineName, $"%{search}%"))
                    .ToList();
            }
            if (category != Guid.Empty)
            {
                medicines = _stockRepository.GetQueryable()
                    .Include(x => x.Medicine)
                    .Include(x => x.Medicine.MedicineCategory)
                    .Where(x => x.Medicine.MedicineCategory.MedicineCategoryId == category)
                    .ToList();
            }
            return View(medicines);
        }
        [HttpGet]
        public IActionResult Detail(Guid id)
        {
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            ViewBag.StockDetails = _stockRepository.GetQueryable()
                .Include(x => x.Medicine)
                .Include(x => x.Medicine.MedicineCategory)
                .Where(x => x.StockId == id)
                .Single();
            var category = _stockRepository.GetQueryable().Include(x => x.Medicine).Include(x => x.Medicine.MedicineCategory).Where(x=>x.StockId == id).Select(x => x.Medicine.MedicineCategory.MedicineCategoryId).Single();
            ViewBag.OtherMedicine = _stockRepository.GetQueryable()
                    .Include(x => x.Medicine)
                    .Include(x => x.Medicine.MedicineCategory)
                    .Where(x => x.Medicine.MedicineCategory.MedicineCategoryId == category && x.StockId != id)
                    .ToList();
            return View();
        }
       
        [HttpPost]

        public IActionResult Detail(OrderDto orderDto, IFormFile prescription)
        {
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            ViewBag.StockDetails = _stockRepository.GetQueryable()
                .Include(x => x.Medicine)
                .Include(x => x.Medicine.MedicineCategory)
                .Where(x => x.StockId == orderDto.StockId)
                .Single();
            var category = _stockRepository.GetQueryable().Include(x => x.Medicine).Include(x => x.Medicine.MedicineCategory).Where(x => x.StockId == orderDto.StockId).Select(x => x.Medicine.MedicineCategory.MedicineCategoryId).Single();
            ViewBag.OtherMedicine = _stockRepository.GetQueryable()
                    .Include(x => x.Medicine)
                    .Include(x => x.Medicine.MedicineCategory)
                    .Where(x => x.Medicine.MedicineCategory.MedicineCategoryId == category && x.StockId != orderDto.StockId)
                    .ToList();
            try
            {
                if (ModelState.IsValid && prescription != null)
                {
                    var userid = _userManager.GetUserId(HttpContext.User);
                    orderDto.UserId = userid;
                    orderDto.OrderDate = DateTime.Now;
                    var stock = _stockService.GetStockById(orderDto.StockId);
                    orderDto.Amount = orderDto.OrderQuantity * stock.StockRate;

                    orderDto.Prescription = _fileHelper.SaveFile(uploadpath,prescription);
                    if (_orderService.AddOrder(orderDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Medicine Ordered Successfully", MessageType.Success);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Update Medicine");
                        return View(orderDto);
                    }
                }
                else
                {
                    if(prescription == null)
                    {
                        ModelState.AddModelError("", "Please Upload Prescription");
                    }
                    return View(orderDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                return View(orderDto);
            }
        }
        [HttpGet]
        public IActionResult MyOrder()
        {
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            var userid = _userManager.GetUserId(HttpContext.User);
            var order = _orderRepository.GetQueryable().Include(x => x.Stock).Include(x => x.Stock.Medicine).Include(x => x.ApplicationUser).OrderBy(x => x.OrderDate).Where(x=>x.UserId == userid).ToList();
            return View(order);
        }
    }
}
