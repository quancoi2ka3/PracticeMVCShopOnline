using BanHangMVC.Models;
using BanHangMVC.Models.EF;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace BanHangMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: ProductsController
        public ActionResult Index(string search, int? page)
        {
            int pageSize = 5;
            int pageIndex = page.GetValueOrDefault(1); // Đảm bảo page không null
            search = search?.Trim(); // Loại bỏ khoảng trắng thừa

            // Truy vấn dữ liệu từ cơ sở dữ liệu
            IQueryable<Products> query = _db.Productss.OrderByDescending(x => x.ID);

            // Lọc theo từ khóa tìm kiếm (nếu có)
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Alias.Contains(search) || x.Title.Contains(search));
            }

            // Phân trang
            var items = query.ToPagedList(pageIndex, pageSize);

            // Truyền dữ liệu sang View
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;
            ViewBag.Search = search; // Giữ lại giá trị tìm kiếm

            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
    }
}
