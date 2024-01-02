using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Models.Toastr;
using PharmaStore.Modules.Services.Implementation;
using PharmaStore.Modules.Services.Interface;
using System.Drawing.Printing;

namespace PharmaStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IStockService _stockService;
        private readonly IPaginationRedirectHelper _paginationRedirectHelper;
        private readonly IStockRepository _stockRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IToastrHelper _toastrHelper;

        public OrderController(IOrderService orderService, IStockService stockService, IPaginationRedirectHelper paginationRedirectHelper, IStockRepository stockRepository, IOrderRepository orderRepository, IToastrHelper toastrHelper)
        {
            _orderService = orderService;
            _stockService = stockService;
            _paginationRedirectHelper = paginationRedirectHelper;
            _stockRepository = stockRepository;
            _orderRepository = orderRepository;
            _toastrHelper = toastrHelper;
        }

        [HttpGet]
        public IActionResult Index(string? search, int page = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            var order = _orderService.GetOrdersWithPagination(page, pagesize);
            if (!String.IsNullOrEmpty(search))
            {
                order = _orderService.SearchOrdersWithPagination(search, page, pagesize);
            }
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            return View(order);
        }
        [HttpGet]
        public IActionResult History(string? search, int page = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            var order = PaginatedList<Order>.Create(_orderRepository.GetQueryable().Include(x => x.Stock).Include(x => x.Stock.Medicine).Include(x => x.ApplicationUser).Where(x => x.OrderStatus != OrderStatus.Pending).OrderByDescending(x => x.OrderStatus == OrderStatus.OnTheWay), page, pagesize);
            if (!String.IsNullOrEmpty(search))
            {
                order = PaginatedList<Order>.Create(_orderRepository.GetQueryable().Include(x => x.Stock).Include(x => x.Stock.Medicine).Include(x => x.ApplicationUser).OrderByDescending(x => x.OrderStatus == OrderStatus.OnTheWay).Where(x => 
                EF.Functions.Like(x.ApplicationUser.UserName, $"%{search}%")
                || EF.Functions.Like(x.Stock.Medicine.MedicineName, $"%{search}%")
                && x.OrderStatus == OrderStatus.Pending), page, pagesize);
            }
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            return View(order);
        }
        [HttpGet]
        public IActionResult SeePrescription(Guid id)
        {
            var order = _orderService.GetOrderById(id);
            return View(order);
        }

        [HttpGet]
        public IActionResult OnTheWay(Guid id, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            var order = _orderService.GetOrderById(id);
            var stock = _stockService.GetStockById(order.StockId);
            stock.StockQuantity = stock.StockQuantity - order.OrderQuantity;
            _stockRepository.Update(stock);
            order.OrderStatus = Data.Entities.OrderStatus.OnTheWay;
            _orderRepository.Update(order);
            return _paginationRedirectHelper.RedirectToPage("admin", "order", "index", search, pageindex, pagesize);
        }

        [HttpGet]
        public IActionResult Rejected(Guid id, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            var order = _orderService.GetOrderById(id);
            order.OrderStatus = Data.Entities.OrderStatus.Rejected;
            _orderRepository.Update(order);
            return _paginationRedirectHelper.RedirectToPage("admin", "order", "index", search, pageindex, pagesize);
        }

        [HttpGet]
        public IActionResult Delivered(Guid id, string? search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            ViewBag.Search = search;
            ViewBag.PageSize = pagesize;
            var order = _orderService.GetOrderById(id);
            order.OrderStatus = Data.Entities.OrderStatus.Delivered;
            _orderRepository.Update(order);
            return _paginationRedirectHelper.RedirectToPage("admin", "order", "history", search, pageindex, pagesize);
        }

        [HttpGet]
        public IActionResult Delete(Guid id, int sn, string search, int pageindex = DefaultPageValue.DEFAULT_PAGEINDEX, int pagesize = DefaultPageValue.DEFAULT_PAGESIZE)
        {
            if (_orderService.DeleteOrder(id))
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Order Deleted Successfully", MessageType.Success);
            }
            else
            {
                _toastrHelper.SendMessage(this, "PharmaStore", "Failed To Delete Order", MessageType.Error);
            }
            return _paginationRedirectHelper.RedirectToPageAfterDelete("admin", "order", "history", sn, search, pageindex, pagesize);
        }
    }
}
