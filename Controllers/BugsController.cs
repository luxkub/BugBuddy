﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugBuddy.Data;
using BugBuddy.Models;

namespace BugBuddy.Controllers
{
    public class BugsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BugsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(int bugId, string noteText, string returnUrl)
        {
            var bug = await _context.Bug.Include(b => b.Notes).FirstOrDefaultAsync(b => b.Id == bugId);

            if (bug == null)
            {
                return NotFound();
            }

            var newNote = new Note
            {
                Text = noteText
            };

            bug.Notes.Add(newNote);
            await _context.SaveChangesAsync();

            // Determine the return action based on the provided returnUrl
            if (returnUrl == "Details")
            {
                return View("Details", bug);
            }
            else if (returnUrl == "Edit")
            {
                return View("Edit", bug);
            }
            else
            {
                // Handle any other cases or return an error if an unexpected returnUrl is provided
                return BadRequest("Invalid return URL specified.");
            }
        }

        // GET: Bugs
        public async Task<IActionResult> Index()
        {
            var bugs = await _context.Bug
                .Select(bug => new Bug
                {
                    // Copy other properties
                    Id = bug.Id,
                    Title = bug.Title,
                    Description = bug.Description,
                    Project = bug.Project,
                    Priority = bug.Priority,
                    Status = bug.Status,
                    Resolution = bug.Resolution ?? "", // Handle NULL here
                    CreatedDate = bug.CreatedDate
                })
                .ToListAsync();

            return View(bugs);
        }

        // GET: Bugs/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        //PoST: Jokes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Bug.Where( b => b.Title.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.Include(b => b.Notes).FirstOrDefaultAsync(m => m.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            // Check for null and assign default
            if (bug.Resolution == null)
            {
                bug.Resolution = "";
            }

            return View(bug);
        }

        // GET: Bugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Project,Priority,Status,CreatedDate")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }

        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.Include(b => b.Notes).FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            // Check for NULL before accessing the 'Resolution' property
            bug.Resolution = bug.Resolution ?? "";

            return View(bug);
        }

        // POST: Bugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Project,Priority,Status,Resolution,CreatedDate")] Bug bug)
        {
            if (id != bug.Id)
            {
                return NotFound();
            }

            // Handle NULL value for Resolution
            bug.Resolution = bug.Resolution ?? "";

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bug.FindAsync(id);
            if (bug != null)
            {
                _context.Bug.Remove(bug);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
            return _context.Bug.Any(e => e.Id == id);
        }
    }
}
