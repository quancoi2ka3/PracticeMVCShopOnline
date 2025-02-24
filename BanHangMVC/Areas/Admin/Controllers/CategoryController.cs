using BanHangMVC.Models;
using BanHangMVC.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangMVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var items = _db.Categories.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                    model.Alias = BanHangMVC.Models.Common.Filter.FilterChar(model.Title);
                    _db.Categories.Add(model);
                    int rowsAffected = _db.SaveChanges(); // Lưu dữ liệu vào DB

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("✅ Dữ liệu đã được lưu vào database.");
                        return RedirectToAction("Index", "Category", new { area = "Admin" });
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Không có dữ liệu nào được thêm vào.");
                    }
                }
                catch (DbUpdateException dbEx) // Lỗi từ database
                {
                    Console.WriteLine($"🛑 Lỗi database: {dbEx.InnerException?.Message ?? dbEx.Message}");
                    ModelState.AddModelError("", "Lỗi database xảy ra.");
                }
                catch (Exception ex) // Lỗi chung
                {
                    Console.WriteLine($"🚨 Lỗi khi lưu dữ liệu: {ex.Message}");
                    ModelState.AddModelError("", "Lỗi không xác định khi lưu dữ liệu.");
                }
            }
            else
            {
                Console.WriteLine("⚠️ Model không hợp lệ.");
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"❌ Validation Error: {modelError.ErrorMessage}");
                }
            }

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _db.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Categories.Attach(model);
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                    model.Alias = BanHangMVC.Models.Common.Filter.FilterChar(model.Title);
                    //_db.Entry(model).State = EntityState.Modified;
                    //_db.Entry(model).Property(x => x.Title).IsModified = true;
                    //_db.Entry(model).Property(x => x.Description).IsModified = true;
                    //_db.Entry(model).Property(x => x.Alias).IsModified = true;
                    //_db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                    //_db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                    //_db.Entry(model).Property(x => x.SeoKeyword).IsModified = true;
                    //_db.Entry(model).Property(x => x.Position).IsModified = true;
                    //_db.Entry(model).Property(x => x.ModifiedBy).IsModified = true;
                    //_db.Entry(model).Property(x => x.CreatedBy).IsModified = true;
                    //_db.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                    //_db.Entry(model).Property(x => x.CreatedDate).IsModified = true;
                    _db.Entry(model).State = EntityState.Modified;
                    int rowsAffected = _db.SaveChanges(); // Lưu dữ liệu vào DB

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("✅ Dữ liệu đã được lưu vào database.");
                        return RedirectToAction("Index", "Category", new { area = "Admin" });
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Không có dữ liệu nào được thêm vào.");
                    }
                }
                catch (DbUpdateException dbEx) // Lỗi từ database
                {
                    Console.WriteLine($"🛑 Lỗi database: {dbEx.InnerException?.Message ?? dbEx.Message}");
                    ModelState.AddModelError("", "Lỗi database xảy ra.");
                }
                catch (Exception ex) // Lỗi chung
                {
                    Console.WriteLine($"🚨 Lỗi khi lưu dữ liệu: {ex.Message}");
                    ModelState.AddModelError("", "Lỗi không xác định khi lưu dữ liệu.");
                }
            }
            else
            {
                Console.WriteLine("⚠️ Model không hợp lệ.");
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"❌ Validation Error: {modelError.ErrorMessage}");
                }
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {

            var item = _db.Categories.Find(id);
            if (item != null)
            {
                var DeleteItem = _db.Categories.Attach(item);
                _db.Categories.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true, message = "Xóa thành công" });
            }

            return Json(new { success = false, message = "Xóa thất bại" });
        }
    }
}
