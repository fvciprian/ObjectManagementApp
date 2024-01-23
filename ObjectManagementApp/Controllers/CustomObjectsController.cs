﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectManagementApp.Models;
using ObjectManagementApp.Services.Interfaces;

namespace ObjectManagementApp.Controllers
{
    public class CustomObjectsController : Controller
    {
        private readonly IObjectService _objectService;
        private readonly ILogger _logger;

        public CustomObjectsController(IObjectService objectService, ILogger<CustomObjectsController> logger)
        {
            _objectService = objectService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string search)
        {
            _logger.LogInformation($"Searching {search}");
            return View(await _objectService.SearchAsync(search));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogInformation($"Id is null.");
                return NotFound();
            }

            var customObject = await _objectService.GetAsync(id);
            if (customObject == null)
            {
                _logger.LogInformation($"Custom object with Id {id} not found.");
                return NotFound();
            }

            return View(customObject);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Type")] CustomObject customObject)
        {
            if (ModelState.IsValid)
            {
                await _objectService.CreateAsync(customObject);
                return RedirectToAction(nameof(Index));
            }
            return View(customObject);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogInformation($"Id is null.");
                return NotFound();
            }

            var customObject = await _objectService.GetAsync(id);
            if (customObject == null)
            {
                _logger.LogInformation($"Custom object with Id {id} not found.");
                return NotFound();
            }
            return View(customObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Type")] CustomObject customObject)
        {
            if (id != customObject.Id)
            {
                _logger.LogInformation($"Provided Id {id} is difdferent than customObject Id {customObject.Id}.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _objectService.UpdateAsync(customObject);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!(await _objectService.CustomObjectExistsAsync(customObject.Id)))
                    {
                        _logger.LogInformation($"Custom object with Id {customObject.Id} not found.");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customObject);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogInformation($"Id is null.");
                return NotFound();
            }

            var customObject = await _objectService.GetAsync(id);
            if (customObject == null)
            {
                _logger.LogInformation($"Custom object with Id {id} not found.");
                return NotFound();
            }

            return View(customObject);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _objectService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
