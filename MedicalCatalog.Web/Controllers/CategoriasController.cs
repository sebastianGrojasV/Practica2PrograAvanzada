using MedicalCatalog.Application.Interfaces;
using MedicalCatalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCatalog.Web.Controllers;

public class CategoriasController : Controller
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    public async Task<IActionResult> Index()
    {
        var categorias = await _categoriaService.GetAllAsync();
        return View(categorias);
    }

    public async Task<IActionResult> Details(int id)
    {
        var categoria = await _categoriaService.GetByIdAsync(id);
        if (categoria is null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Categoria categoria)
    {
        if (!ModelState.IsValid)
        {
            return View(categoria);
        }

        var result = await _categoriaService.CreateAsync(categoria);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(categoria);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _categoriaService.GetByIdAsync(id);
        if (categoria is null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Categoria categoria)
    {
        if (id != categoria.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(categoria);
        }

        var result = await _categoriaService.UpdateAsync(categoria);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(categoria);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _categoriaService.GetByIdAsync(id);
        if (categoria is null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _categoriaService.DeleteAsync(id);
        if (!result.Success)
        {
            TempData["Error"] = result.Error;
        }

        return RedirectToAction(nameof(Index));
    }
}
