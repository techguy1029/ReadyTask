using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReadyTask.Data;
using ReadyTask.Models;
using ReadyTask.ViewModels;

namespace ReadyTask.Controllers
{
    public class TaskItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TaskItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: TaskItem
        public ActionResult Index()
        {
            List<TaskItem> allTasks = _context.TaskItems.Include(t => t.AssignedUser).ToList();
            return View(allTasks);
        }

        // GET: TaskItem/Details/5
        public ActionResult Details(int id)
        {
            TaskItem task = _context.TaskItems.Include(t =>t.AssignedUser).FirstOrDefault(t => t.Id == id);
            //post to git
            return View(task);
        }

        // GET: TaskItem/Create
        public ActionResult Create()
        {
            TaskItemCreate viewModel = new TaskItemCreate();
            viewModel.ReadyTaskUsers = _context.Users.ToList();
            return View(viewModel);
        }

        // POST: TaskItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Title,Description,AssignedUserId")] TaskItem task)
        {
            if(ModelState.IsValid)
            {
                _context.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
            
        }

        // GET: TaskItem/Edit/5
        public ActionResult Edit(int id)
        {
            TaskItemCreate viewModel = new TaskItemCreate();
            viewModel.TaskItem = _context.TaskItems.FirstOrDefault(t => t.Id == id);
            viewModel.ReadyTaskUsers = _context.Users.ToList();
            return View(viewModel);
        }

        // POST: TaskItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Title,Description,AssignedUserId")] TaskItem task)
        {
            if(ModelState.IsValid)
            {
                _context.Update(task);
                _context.SaveChanges();
                //return RedirectToAction("Index");
            }
            //TaskItemCreate viewModel = new TaskItemCreate();
            //viewModel.ReadyTaskUsers = _context.Users.ToList();
            return View(task);
        }

        // GET: TaskItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if(task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: TaskItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            _context.TaskItems.Remove(task);
            await  _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
