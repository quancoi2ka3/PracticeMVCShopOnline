using BanHangMVC.Models;
using BanHangMVC.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NewsController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: NewsController
        public ActionResult Index()
        {
            var items = _db.New.OrderByDescending(x => x.ID).ToList();
            return View(items);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsController/Add
        public ActionResult Add()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        // POST: NewsController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(New model)
        {
            if (ModelState.IsValid)
            {
                var categoryExists = _db.Categories.Any(c => c.ID == model.CategoryID);
                if (!categoryExists)
                {
                    Console.WriteLine($"❌ CategoryID {model.CategoryID} does not exist.");
                    ModelState.AddModelError("CategoryID", "Invalid category.");
                    return View(model);
                }

                try
                {
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                    model.Alias = BanHangMVC.Models.Common.Filter.FilterChar(model.Title);
                    _db.New.Add(model);
                    int rowsAffected = _db.SaveChanges(); // Lưu dữ liệu vào DB

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("✅ Dữ liệu đã được lưu vào database.");
                        return RedirectToAction("Index", "News", new { area = "Admin" });
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

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = _db.New.Find(id);
            return View(item);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(New model)
        {
            var categoryExists = _db.Categories.Any(c => c.ID == model.CategoryID);
            if (!categoryExists)
            {
                Console.WriteLine($"❌ CategoryID {model.CategoryID} không tồn tại.");
                ModelState.AddModelError("CategoryID", "Danh mục không hợp lệ.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.New.Attach(model);
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                    model.Alias = BanHangMVC.Models.Common.Filter.FilterChar(model.Title);
                    //_db.Entry(model).State = EntityState.Modified;
                    //_db.Entry(model).Property(x => x.Title).IsModified = true;
                    //_db.Entry(model).Property(x => x.Description).IsModified = true;
                    //_db.Entry(model).Property(x => x.Detail).IsModified = true;
                    //_db.Entry(model).Property(x => x.Image).IsModified = true;
                    //_db.Entry(model).Property(x => x.Alias).IsModified = true;
                    //_db.Entry(model).Property(x => x.CategoryID).IsModified = true;
                    //_db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                    //_db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                    //_db.Entry(model).Property(x => x.SeoKeyword).IsModified = true;
                    //_db.Entry(model).Property(x => x.ModifiedBy).IsModified = true;
                    //_db.Entry(model).Property(x => x.CreatedBy).IsModified = true;
                    //_db.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                    //_db.Entry(model).Property(x => x.CreatedDate).IsModified = true;
                    _db.Entry(model).State = EntityState.Modified;
                    int rowsAffected = _db.SaveChanges(); // Lưu dữ liệu vào DB

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("✅ Dữ liệu đã được lưu vào database.");
                        return RedirectToAction("Index", "News", new { area = "Admin" });
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
            var item = _db.New.Find(id);
            if (item != null)
            {
                var DeleteItem = _db.New.Attach(item);
                _db.New.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true, message = "Xóa thành công" });
            }

            return Json(new { success = false, message = "Xóa thất bại" });
        }
        public ActionResult IsActive(int id)
        {
            var item = _db.New.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                return Json(new { success = true, IsActive = item.IsActive });
            }

            return Json(new { success = false });
        }

    }
}
