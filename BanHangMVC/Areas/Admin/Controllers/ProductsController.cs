﻿using BanHangMVC.Models;
using BanHangMVC.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Products model)
        {
            if (ModelState.IsValid)
            {
                var categoryExists = _db.Categories.Any(c => c.ID == model.ProductCategoryID);
                if (!categoryExists)
                {
                    Console.WriteLine($"❌ CategoryID {model.ProductCategoryID} does not exist.");
                    ModelState.AddModelError("CategoryID", "Invalid category.");
                    return View(model);
                }

                try
                {
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                    model.Alias = BanHangMVC.Models.Common.Filter.FilterChar(model.Title);
                    _db.Productss.Add(model);
                    int rowsAffected = _db.SaveChanges(); // Lưu dữ liệu vào DB

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("✅ Dữ liệu đã được lưu vào database.");
                        return RedirectToAction("Index", "Products", new { area = "Admin" });
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

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = _db.Productss.Find(id);
            return View(item);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Products model)
        {
            var categoryExists = _db.Categories.Any(c => c.ID == model.ProductCategoryID);
            if (!categoryExists)
            {
                Console.WriteLine($"❌ CategoryID {model.ProductCategoryID} không tồn tại.");
                ModelState.AddModelError("CategoryID", "Danh mục không hợp lệ.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Productss.Attach(model);
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
            var item = _db.Productss.Find(id);
            if (item != null)
            {
                var DeleteItem = _db.Productss.Attach(item);
                _db.Productss.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true, message = "Xóa thành công" });
            }

            return Json(new { success = false, message = "Xóa thất bại" });
        }
        public ActionResult IsActive(int id)
        {
            var item = _db.Productss.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                return Json(new { success = true, IsActive = item.IsActive });
            }

            return Json(new { success = false });
        }
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _db.Productss.Find(Convert.ToInt32(item));
                        _db.Productss.Remove(obj);
                        _db.SaveChanges();
                    }
                }
                return Json(new { success = true, message = "Xóa thành công" });
            }
            return Json(new { success = false });
        }
    }
}
