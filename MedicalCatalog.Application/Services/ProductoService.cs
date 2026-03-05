using Microsoft.EntityFrameworkCore;
using MedicalCatalog.Application.Interfaces;
using MedicalCatalog.Domain.Entities;

namespace MedicalCatalog.Application.Services;

public class ProductoService : IProductoService
{
    private readonly IRepository<Producto> _productoRepository;
    private readonly IRepository<Categoria> _categoriaRepository;

    public ProductoService(IRepository<Producto> productoRepository, IRepository<Categoria> categoriaRepository)
    {
        _productoRepository = productoRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _productoRepository.GetAllAsync(include: q => q.Include(p => p.Categoria));
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _productoRepository.GetByIdAsync(id, q => q.Include(p => p.Categoria));
    }

    public async Task<(bool Success, string? Error)> CreateAsync(Producto producto)
    {
        producto.Nombre = producto.Nombre.Trim();
        producto.Descripcion = producto.Descripcion?.Trim();

        var categoriaExiste = await _categoriaRepository.AnyAsync(c => c.Id == producto.CategoriaId);
        if (!categoriaExiste)
        {
            return (false, "La categoria seleccionada no existe.");
        }

        await _productoRepository.AddAsync(producto);
        await _productoRepository.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(Producto producto)
    {
        producto.Nombre = producto.Nombre.Trim();
        producto.Descripcion = producto.Descripcion?.Trim();

        var categoriaExiste = await _categoriaRepository.AnyAsync(c => c.Id == producto.CategoriaId);
        if (!categoriaExiste)
        {
            return (false, "La categoria seleccionada no existe.");
        }

        _productoRepository.Update(producto);
        await _productoRepository.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto is null)
        {
            return (false, "El producto no existe.");
        }

        _productoRepository.Remove(producto);
        await _productoRepository.SaveChangesAsync();
        return (true, null);
    }
}
