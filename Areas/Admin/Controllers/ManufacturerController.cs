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
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly IManufacturerMapper _manufacturerMapper;
        private readonly IPaginationRedirectHelper _paginationRedirectHelper;
        private readonly IToastrHelper _toastrHelper;

        public ManufacturerController(IManufacturerService manufacturerService, IManufacturerMapper manufacturerMapper, IPaginationRedirectHelper paginationRedirectHelper, IToastrHelper toastrHelper)
        {
            _manufacturerService = manufacturerService;
            _manufacturerMapper = manufacturerMapper;
            _paginationRedirectHelper = paginationRedirectHelper;
            _toastrHelper = toastrHelper;
        }

        [HttpGet]
        public IActionResult Index(string? search, int page = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            var manufacturers = _manufacturerService.GetManufacturersWithPagination(page, pagesize);
            if (!String.IsNullOrEmpty(search))
            {
                manufacturers = _manufacturerService.SearchManufacturersWithPagination(search, page, pagesize);
            }
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            return View(manufacturers);
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
        public IActionResult Add(ManufacturerDto manufacturerDto, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            try
            {
                if (ModelState.IsValid)
                {
                    if (_manufacturerService.AddManufacturer(manufacturerDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Manufacturer Added Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "manufacturer", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Add Manufacturer");
                        return View(manufacturerDto);
                    }
                }
                else
                {
                    return View(manufacturerDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(manufacturerDto);
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            var manufacturer = _manufacturerService.GetManufacturerById(id);
            if (manufacturer != null)
            {
                ManufacturerDto manufacturerDto = _manufacturerMapper.MapToDto(manufacturer);
                return View(manufacturerDto);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", id + ": Manufacturer Not Found", MessageType.Error);
                return _paginationRedirectHelper.RedirectToPage("admin", "manufacturer", "index", search, pageindex, pagesize);
            }
        }

        [HttpPost]
        public IActionResult Edit(ManufacturerDto manufacturerDto, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            try
            {
                if (ModelState.IsValid)
                {
                    if (_manufacturerService.UpdateManufacturer(manufacturerDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Manufacturer Updated Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "manufacturer", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Update Manufacturer");
                        return View(manufacturerDto);
                    }
                }
                else
                {
                    return View(manufacturerDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(manufacturerDto);
            }
        }
        [HttpGet]
        public IActionResult Delete(Guid id, int sn, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            if (_manufacturerService.DeleteManufacturer(id))
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Manufacturer Deleted Successfully", MessageType.Success);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Failed To Delete Manufacturer", MessageType.Error);
            }
            return _paginationRedirectHelper.RedirectToPageAfterDelete("admin", "manufacturer", "index", sn, search, pageindex, pagesize);
        }
    }
}
