using MedicalCatalog.Domain.Entities;

namespace MedicalCatalog.Application.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task<(bool Success, string? Error)> CreateAsync(Producto producto);
    Task<(bool Success, string? Error)> UpdateAsync(Producto producto);
    Task<(bool Success, string? Error)> DeleteAsync(int id);
}
