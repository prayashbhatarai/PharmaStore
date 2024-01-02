using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Models.Toastr;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierMapper _supplierMapper;
        private readonly IPaginationRedirectHelper _paginationRedirectHelper;
        private readonly IToastrHelper _toastrHelper;

        public SupplierController(ISupplierService supplierService, ISupplierMapper supplierMapper, IPaginationRedirectHelper paginationRedirectHelper, IToastrHelper toastrHelper)
        {
            _supplierService = supplierService;
            _supplierMapper = supplierMapper;
            _paginationRedirectHelper = paginationRedirectHelper;
            _toastrHelper = toastrHelper;
        }

        [HttpGet]
        public IActionResult Index(string? search, int page = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            var suppliers = _supplierService.GetSuppliersWithPagination(page, pagesize);
            if (!String.IsNullOrEmpty(search))
            {
                suppliers = _supplierService.SearchSuppliersWithPagination(search, page, pagesize);
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
            return View();
        }

        [HttpPost]
        public IActionResult Add(SupplierDto supplierDto, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            try
            {
                if (ModelState.IsValid)
                {
                    if (_supplierService.AddSupplier(supplierDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Supplier Added Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "supplier", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Add Supplier");
                        return View(supplierDto);
                    }
                }
                else
                {
                    return View(supplierDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(supplierDto);
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier != null)
            {
                SupplierDto supplierDto = _supplierMapper.MapToDto(supplier);
                return View(supplierDto);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", id + ": Supplier Not Found", MessageType.Error);
                return _paginationRedirectHelper.RedirectToPage("admin", "supplier", "index", search, pageindex, pagesize);
            }
        }

        [HttpPost]
        public IActionResult Edit(SupplierDto supplierDto, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            try
            {
                if (ModelState.IsValid)
                {
                    if (_supplierService.UpdateSupplier(supplierDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Supplier Updated Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "supplier", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Update Supplier");
                        return View(supplierDto);
                    }
                }
                else
                {
                    return View(supplierDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(supplierDto);
            }
        }
        [HttpGet]
        public IActionResult Delete(Guid id, int sn, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            if (_supplierService.DeleteSupplier(id))
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Supplier Deleted Successfully", MessageType.Success);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Failed To Delete Supplier", MessageType.Error);
            }
            return _paginationRedirectHelper.RedirectToPageAfterDelete("admin", "supplier", "index", sn, search, pageindex, pagesize);
        }
    }
}
