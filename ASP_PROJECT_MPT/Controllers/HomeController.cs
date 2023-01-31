using ASP_PROJECT_MPT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ASP_PROJECT_MPT.Controllers
{
    public class HomeController : Controller
    {
        private AplicationContext db;
        public HomeController(AplicationContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index(int? Id, string email, string Login, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            ViewBag.LoginUser = HttpContext.Session.GetString("IdUser");
            IQueryable<User> users = db.Users;
            //Фильтрация или поиск
            if(Id != null && Id > 0)
            {
                users = users.Where(prop => prop.Id == Id);
            }
            if(!String.IsNullOrEmpty(Login))
            {
                users = users.Where(p => p.Login.Contains(Login));
            }
            switch (sortOrder)
            {
                case SortState.IdAsc:
                    {
                        users = users.OrderBy(m => m.Id);
                        break;
                    }
                case SortState.IdDesc:
                    {
                        users = users.OrderByDescending(m => m.Id);
                        break;
                    }
                case SortState.EmailAsc:
                    {
                        users = users.OrderBy(m => m.Email);
                        break;
                    }
                case SortState.EmailSesc:
                    {
                        users = users.OrderByDescending(m => m.Email);
                        break;
                    }
                case SortState.LoginAsc:
                    {
                        users = users.OrderBy(m => m.Login);
                        break;
                    }
                case SortState.LoginDesc:
                    {
                        users = users.OrderByDescending(m => m.Login);
                        break;
                    }
                default:
                    {
                        users = users.OrderBy(m => m.Id);
                        break;
                    }
            }

            //Пагинация
            int pageSize = 4;
            var count = await users.CountAsync();
            var item = await users.Skip((page - 1)* pageSize).Take(pageSize).ToListAsync();
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(Id, email, Login),
                Users = item
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(Predicate => Predicate.Id == id);
                if (user != null)
                     return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(Predicate => Predicate.Id == id);
                if (id != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                    
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(Predicate => Predicate.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult registr()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> registr(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("auth");
        }
        public IActionResult auth()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Auth(User user)
        {
            var userLogin = db.Users.FirstOrDefault(predicate => predicate.Login == user.Login && predicate.Password == user.Password);
            if(userLogin != null)
            {
                HttpContext.Session.SetString("IdUser", userLogin.Login.ToString());
                return RedirectToAction("Index");
            }
            else return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(Predicate => Predicate.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }


    }
}
