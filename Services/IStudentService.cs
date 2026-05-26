using ComprehensiveBlazorApp.Models;

namespace ComprehensiveBlazorApp.Services;

public interface IStudentService
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task AddOrUpdateAsync(Student student);
    Task DeleteAsync(int id);
}
