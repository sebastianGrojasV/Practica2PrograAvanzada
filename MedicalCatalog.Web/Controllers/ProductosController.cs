using MedicalCatalog.Application.Interfaces;
using MedicalCatalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalCatalog.Web.Controllers;

public class ProductosController : Controller
{
    private readonly IProductoService _productoService;
    private readonly ICategoriaService _categoriaService;

    public ProductosController(IProductoService productoService, ICategoriaService categoriaService)
    {
        _productoService = productoService;
        _categoriaService = categoriaService;
    }

    public async Task<IActionResult> Index()
    {
        var productos = await _productoService.GetAllAsync();
        return View(productos);
    }

    public async Task<IActionResult> Details(int id)
    {
        var producto = await _productoService.GetByIdAsync(id);
        if (producto is null)
        {
            return NotFound();
        }

        return View(producto);
    }

    public async Task<IActionResult> Create()
    {
        await LoadCategoriasAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Producto producto)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategoriasAsync(producto.CategoriaId);
            return View(producto);
        }

        var result = await _productoService.CreateAsync(producto);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            await LoadCategoriasAsync(producto.CategoriaId);
            return View(producto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var producto = await _productoService.GetByIdAsync(id);
        if (producto is null)
        {
            return NotFound();
        }

        await LoadCategoriasAsync(producto.CategoriaId);
        return View(producto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Producto producto)
    {
        if (id != producto.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await LoadCategoriasAsync(producto.CategoriaId);
            return View(producto);
        }

        var result = await _productoService.UpdateAsync(producto);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            await LoadCategoriasAsync(producto.CategoriaId);
            return View(producto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _productoService.GetByIdAsync(id);
        if (producto is null)
        {
            return NotFound();
        }

        return View(producto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _productoService.DeleteAsync(id);
        if (!result.Success)
        {
            TempData["Error"] = result.Error;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategoriasAsync(int? selectedId = null)
    {
        var categorias = await _categoriaService.GetAllAsync();
        ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", selectedId);
    }
}
