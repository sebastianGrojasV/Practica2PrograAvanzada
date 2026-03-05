using MedicalCatalog.Domain.Entities;

namespace MedicalCatalog.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int id);
    Task<(bool Success, string? Error)> CreateAsync(Categoria categoria);
    Task<(bool Success, string? Error)> UpdateAsync(Categoria categoria);
    Task<(bool Success, string? Error)> DeleteAsync(int id);
}
