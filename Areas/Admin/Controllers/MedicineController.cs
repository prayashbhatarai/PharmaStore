using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Helpers.Implementation;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Models.Toastr;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class MedicineController : Controller
    {
        private readonly IMedicineService _medicineService;
        private readonly IMedicineCategoryService _medicineCategoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IMedicineMapper _medicineMapper;
        private readonly IToastrHelper _toastrHelper;
        private readonly IPaginationRedirectHelper _paginationRedirectHelper;

        public MedicineController(IMedicineService medicineService, IMedicineCategoryService medicineCategoryService, IManufacturerService manufacturerService, IMedicineMapper medicineMapper, IToastrHelper toastrHelper, IPaginationRedirectHelper paginationRedirectHelper)
        {
            _medicineService = medicineService;
            _medicineCategoryService = medicineCategoryService;
            _manufacturerService = manufacturerService;
            _medicineMapper = medicineMapper;
            _toastrHelper = toastrHelper;
            _paginationRedirectHelper = paginationRedirectHelper;
        }

        [HttpGet]
        public IActionResult Index(string? search, int page = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            var medicines = _medicineService.GetMedicinesWithPagination(page, pagesize);
            if (!String.IsNullOrEmpty(search))
            {
                medicines = _medicineService.SearchMedicinesWithPagination(search, page, pagesize);
            }
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            return View(medicines);
        }

        [HttpGet]
        public IActionResult Add(string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            ViewBag.Manufacturers = _manufacturerService.GetAllManufacturers();
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            return View();
        }

        [HttpPost]
        public IActionResult Add(MedicineDto medicineDto, IFormFile image, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            ViewBag.Manufacturers = _manufacturerService.GetAllManufacturers();
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            try
            {
                if (ModelState.IsValid && medicineDto.MedicineCategoryId !=Guid.Empty && medicineDto.ManufacturerId != Guid.Empty && image != null)
                {
                    if (_medicineService.AddMedicine(medicineDto, image))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Medicine Added Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "medicine", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Update Medicine");
                        return View(medicineDto);
                    }
                }
                else
                {
                    if(medicineDto.ManufacturerId == Guid.Empty)
                    {
                        ModelState.AddModelError("ManufacturerId", "Please Select Manufacturer");
                    } 
                    if(medicineDto.MedicineCategoryId == Guid.Empty)
                    {
                        ModelState.AddModelError("MedicineCategoryId", "Please Select Category");
                    }
                    if(image == null)
                    {
                        ModelState.AddModelError("", "Please Upload Image For Medicine");
                    }
                    return View(medicineDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(medicineDto);
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid id, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            ViewBag.Manufacturers = _manufacturerService.GetAllManufacturers();
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            var medicine = _medicineService.GetMedicineById(id);
            if (medicine != null)
            {
                MedicineDto medicineDto = _medicineMapper.MapToDto(medicine);
                return View(medicineDto);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", id + ": Medicine Not Found", MessageType.Error);
                return _paginationRedirectHelper.RedirectToPage("admin", "medicine", "index", search, pageindex, pagesize);
            }
        }

        [HttpPost]
        public IActionResult Edit(MedicineDto medicineDto, IFormFile image, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            ViewBag.Manufacturers = _manufacturerService.GetAllManufacturers();
            ViewBag.Categories = _medicineCategoryService.GetAllCategory();
            try
            {
                if (ModelState.IsValid && medicineDto.MedicineCategoryId != Guid.Empty && medicineDto.ManufacturerId != Guid.Empty && image != null)
                {
                    if (_medicineService.UpdateMedicine(medicineDto, image))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Medicine Updated Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "medicine", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Add Medicine");
                        return View(medicineDto);
                    }
                }
                else
                {
                    if (medicineDto.ManufacturerId == Guid.Empty)
                    {
                        ModelState.AddModelError("MedicineId", "Please Select Manufacturer");
                    }
                    if (medicineDto.MedicineCategoryId == Guid.Empty)
                    {
                        ModelState.AddModelError("MedicineCategoryId", "Please Select Category");
                    }
                    if (image == null)
                    {
                        ModelState.AddModelError("", "Please Upload Image For Medicine");
                    }
                    return View(medicineDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                return View(medicineDto);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id, int sn, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            if (_medicineService.DeleteMedicine(id))
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Medicine Deleted Successfully", MessageType.Success);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Failed To Delete Medicine", MessageType.Error);

            }
            return _paginationRedirectHelper.RedirectToPageAfterDelete("admin", "medicine", "index", sn, search, pageindex, pagesize);
        }
    }
}