using MedicalCatalog.Application.Interfaces;
using MedicalCatalog.Domain.Entities;

namespace MedicalCatalog.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IRepository<Categoria> _categoriaRepository;
    private readonly IRepository<Producto> _productoRepository;

    public CategoriaService(IRepository<Categoria> categoriaRepository, IRepository<Producto> productoRepository)
    {
        _categoriaRepository = categoriaRepository;
        _productoRepository = productoRepository;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _categoriaRepository.GetAllAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _categoriaRepository.GetByIdAsync(id);
    }

    public async Task<(bool Success, string? Error)> CreateAsync(Categoria categoria)
    {
        categoria.Nombre = categoria.Nombre.Trim();
        categoria.Descripcion = categoria.Descripcion?.Trim();

        var nombreDuplicado = await _categoriaRepository.AnyAsync(c => c.Nombre == categoria.Nombre);
        if (nombreDuplicado)
        {
            return (false, "Ya existe una categoria con ese nombre.");
        }

        await _categoriaRepository.AddAsync(categoria);
        await _categoriaRepository.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(Categoria categoria)
    {
        categoria.Nombre = categoria.Nombre.Trim();
        categoria.Descripcion = categoria.Descripcion?.Trim();

        var nombreDuplicado = await _categoriaRepository.AnyAsync(c => c.Nombre == categoria.Nombre && c.Id != categoria.Id);
        if (nombreDuplicado)
        {
            return (false, "Ya existe una categoria con ese nombre.");
        }

        _categoriaRepository.Update(categoria);
        await _categoriaRepository.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria is null)
        {
            return (false, "La categoria no existe.");
        }

        var tieneProductos = await _productoRepository.AnyAsync(p => p.CategoriaId == id);
        if (tieneProductos)
        {
            return (false, "No se puede eliminar una categoria con productos asociados.");
        }

        _categoriaRepository.Remove(categoria);
        await _categoriaRepository.SaveChangesAsync();
        return (true, null);
    }
}
