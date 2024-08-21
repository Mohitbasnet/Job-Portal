using _2022E_WebApp.Entities;
using _2022E_WebApp.Models;
using _2022E_WebApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2022E_WEBAPP.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        AppDbContext _dbContext = new AppDbContext();
        // GET: JobController
        public ActionResult Index()
        {
            var job_list = _dbContext.Jobs.Select(j => new JobViewModel()
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                Salary = j.Salary,
                IsActive = j.IsActive,
            }).ToList();
            if (job_list.Count > 0)
            {
                var paged_list = new PaginatedJobViewModel()
                {
                    CurrentPage = 1,
                    Count = job_list.Count,
                    Data = job_list
                };
                return View(paged_list);
            }
            return View(Enumerable.Empty<PaginatedJobViewModel>());

        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {

            var job_details = _dbContext.Jobs.Where(j=>j.Id == id)
                .Select(x =>new JobViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    Salary = x.Salary,
                    IsActive = x.IsActive,
                }).FirstOrDefault();
            if(job_details != null)
            {
                return View(job_details);
            }
            return View();
        }

        // GET: JobController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobViewModel Jvm)
        {
            try
            {
                var _job = new Job()
                {
                 
                    Title = Jvm.Title,
                    Description = Jvm.Description,
                    IsActive = Jvm.IsActive,
                    Salary = Jvm.Salary,
                    CreatedDate = DateTime.UtcNow,
                    AddedById = Convert.ToInt32(HttpContext.User.Claims.ElementAt(1).Value),
                };
                _dbContext.Jobs.Add(_job);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: JobController/Edit/5
        public ActionResult Edit(int id)
        {
            var job_details = _dbContext.Jobs.Where(j => j.Id == id)
                .Select(x => new JobViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    Salary = x.Salary,
                    IsActive = x.IsActive,
                }).FirstOrDefault();
            if (job_details != null)
            {
                return View(job_details);
            }
            return View();
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, JobViewModel jvm)
        {
            try
            {
                var edit_job = new Job()
                {
                    Id = id,
                    Title = jvm.Title,
                    Description = jvm.Description,
                    Salary = jvm.Salary,
                    IsActive = jvm.IsActive,
                    AddedById = Convert.ToInt32(HttpContext.User.Claims.ElementAt(1).Value),
                    ModifiedDate = DateTime.UtcNow,
                };
                _dbContext.Jobs.Update(edit_job);
                _dbContext.SaveChanges();
                 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            var job_details = _dbContext.Jobs.Where(j => j.Id == id)
                .Select(x => new JobViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    Salary = x.Salary,
                    IsActive = x.IsActive,
                }).FirstOrDefault();
          
            if (job_details != null)
            {
                
                return View(job_details);
            }
            
            return View();
        }

        // POST: JobController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var delete_job = _dbContext.Jobs.Where(j => j.Id == id).FirstOrDefault();
                _dbContext.Jobs.Remove(delete_job);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}