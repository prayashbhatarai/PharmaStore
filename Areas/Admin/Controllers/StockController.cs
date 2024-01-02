using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Mappers.Implementation;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Models.Toastr;
using PharmaStore.Modules.Services.Implementation;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IMedicineService _medicineService;
        private readonly ISupplierService _supplierService;
        private readonly IStockMapper _stockMapper;
        private readonly IPaginationRedirectHelper _paginationRedirectHelper;
        private readonly IToastrHelper _toastrHelper;

        public StockController(IStockService stockService, IMedicineService medicineService, ISupplierService supplierService, IStockMapper stockMapper, IPaginationRedirectHelper paginationRedirectHelper, IToastrHelper toastrHelper)
        {
            _stockService = stockService;
            _medicineService = medicineService;
            _supplierService = supplierService;
            _stockMapper = stockMapper;
            _paginationRedirectHelper = paginationRedirectHelper;
            _toastrHelper = toastrHelper;
        }

        [HttpGet]
        public IActionResult Index(string? search, int page = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            var suppliers = _stockService.GetStocksWithPagination(page, pagesize);
            if (!String.IsNullOrEmpty(search))
            {
                suppliers = _stockService.SearchStocksWithPagination(search, page, pagesize);
            }
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            return View(suppliers);
        }
        [HttpGet]
        public IActionResult Add(string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            ViewBag.Medicines = _medicineService.GetAllMedicines();
            ViewBag.Suppliers = _supplierService.GetAllSuppliers();
            return View();
        }

        [HttpPost]
        public IActionResult Add(StockDto stockDto, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            ViewBag.Medicines = _medicineService.GetAllMedicines();
            ViewBag.Suppliers = _supplierService.GetAllSuppliers();
            try
            {
                if (ModelState.IsValid && stockDto.MedicineId != Guid.Empty && stockDto.MedicineId != Guid.Empty)
                {
                    if (_stockService.AddStock(stockDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Stock Added Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "stock", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Add Stock");
                        return View(stockDto);
                    }
                }
                else
                {
                    if(stockDto.MedicineId == Guid.Empty)
                    {
                        ModelState.AddModelError("MedicineId", "Please Select Medicine");
                    }
                    if(stockDto.SupplierId == Guid.Empty)
                    {
                        ModelState.AddModelError("SupplierId", "Please Select Supplier");
                    }
                    return View(stockDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(stockDto);
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid id, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            ViewBag.Medicines = _medicineService.GetAllMedicines();
            ViewBag.Suppliers = _supplierService.GetAllSuppliers();
            var stock = _stockService.GetStockById(id);
            if (stock != null)
            {
                StockDto stockDto = _stockMapper.MapToDto(stock);
                return View(stockDto);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", id + ": Stock Not Found", MessageType.Error);
                return _paginationRedirectHelper.RedirectToPage("admin", "stock", "index", search, pageindex, pagesize);
            }
        }

        [HttpPost]
        public IActionResult Edit(StockDto stockDto, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            ViewBag.Medicines = _medicineService.GetAllMedicines();
            ViewBag.Suppliers = _supplierService.GetAllSuppliers();
            try
            {
                if (ModelState.IsValid && stockDto.MedicineId != Guid.Empty && stockDto.MedicineId != Guid.Empty)
                {
                    if (_stockService.UpdateStock(stockDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Stock Updated Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "stock", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Update Stock");
                        return View(stockDto);
                    }
                }
                else
                {
                    if (stockDto.MedicineId == Guid.Empty)
                    {
                        ModelState.AddModelError("MedicineId", "Please Select Medicine");
                    }
                    if (stockDto.SupplierId == Guid.Empty)
                    {
                        ModelState.AddModelError("SupplierId", "Please Select Supplier");
                    }
                    return View(stockDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(stockDto);
            }
        }
        [HttpGet]
        public IActionResult Delete(Guid id, int sn, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            if (_stockService.DeleteStock(id))
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Stock Deleted Successfully", MessageType.Success);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Failed To Delete Stock", MessageType.Error);
            }
            return _paginationRedirectHelper.RedirectToPageAfterDelete("admin", "Stock", "index", sn, search, pageindex, pagesize);
        }
    }
}
