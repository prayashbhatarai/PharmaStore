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
    public class MedicineCategoryController : Controller
    {
        private readonly IMedicineCategoryService _medicineCategoryService;
        private readonly IMedicineCategoryMapper _medicineCategoryMapper;
        private readonly IPaginationRedirectHelper _paginationRedirectHelper;
        private readonly IToastrHelper _toastrHelper;

        public MedicineCategoryController(IMedicineCategoryService medicineCategoryService, IMedicineCategoryMapper medicineCategoryMapper, IPaginationRedirectHelper paginationRedirectHelper, IToastrHelper toastrHelper)
        {
            _medicineCategoryService = medicineCategoryService;
            _medicineCategoryMapper = medicineCategoryMapper;
            _paginationRedirectHelper = paginationRedirectHelper;
            _toastrHelper = toastrHelper;
        }

        [HttpGet]
        public IActionResult Index(string? search, int page = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            var medicineCategories = _medicineCategoryService.GetCategoriesWithPagination(page, pagesize);
            if (!String.IsNullOrEmpty(search))
            {
                medicineCategories = _medicineCategoryService.SearchCategoriesWithPagination(search, page, pagesize);
            }
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            return View(medicineCategories);
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
        public IActionResult Add(MedicineCategoryDto medicineCategoryDto, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            try
            {
                if (ModelState.IsValid)
                {
                    if (_medicineCategoryService.AddCategory(medicineCategoryDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Category Added Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "medicinecategory", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Add Category");
                        return View(medicineCategoryDto);
                    }
                }
                else
                {
                    return View(medicineCategoryDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(medicineCategoryDto);
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid id, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            var medicineCategory = _medicineCategoryService.GetCategoryById(id);
            if (medicineCategory != null)
            {
                MedicineCategoryDto medicineCategoryDto = _medicineCategoryMapper.MapToDto(medicineCategory);
                return View(medicineCategoryDto);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", id +": Category Not Found", MessageType.Error);
                return _paginationRedirectHelper.RedirectToPage("admin", "medicinecategory", "index", search, pageindex, pagesize);
            }
        }

        [HttpPost]
        public IActionResult Edit(MedicineCategoryDto medicineCategoryDto, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageIndex = pageindex;
            ViewBag.PageSize = pagesize;
            try
            {
                if (ModelState.IsValid)
                {
                    if (_medicineCategoryService.UpdateCategory(medicineCategoryDto))
                    {
                        _toastrHelper.SendMessage(this, "PharmaStore", "Category Updated Successfully", MessageType.Success);
                        return _paginationRedirectHelper.RedirectToPage("admin", "medicinecategory", "index", search, pageindex, pagesize);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed To Update Category");
                        return View(medicineCategoryDto);
                    }
                }
                else
                {
                    return View(medicineCategoryDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Exception : " + ex.Message);
                _toastrHelper.SendMessage(this, "PharmaStore", ex.Message, MessageType.Error);
                return View(medicineCategoryDto);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id, int sn, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            if (_medicineCategoryService.DeleteCategory(id))
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Category Deleted Successfully", MessageType.Success);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Failed To Delete Category", MessageType.Error);
            }
            return _paginationRedirectHelper.RedirectToPageAfterDelete("admin", "medicinecategory", "index", sn, search, pageindex, pagesize);
        }
    }
}