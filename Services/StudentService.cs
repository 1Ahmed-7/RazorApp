using ComprehensiveBlazorApp.Models;

namespace ComprehensiveBlazorApp.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> _students =
    [
        new() { Id = 1, Name = "Alice Ahmed", Email = "alice@example.com", Gpa = 3.8, EnrollmentDate = DateTime.Today.AddDays(-120) },
        new() { Id = 2, Name = "Badr Hassan", Email = "badr@example.com", Gpa = 3.5, EnrollmentDate = DateTime.Today.AddDays(-220) },
        new() { Id = 3, Name = "Celine Omar", Email = "celine@example.com", Gpa = 3.9, EnrollmentDate = DateTime.Today.AddDays(-30) }
    ];

    private int _nextId = 4;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public Task<List<Student>> GetAllAsync()
    {
        var result = _students
            .OrderByDescending(s => s.EnrollmentDate)
            .ThenByDescending(s => s.Gpa)
            .ToList();
        return Task.FromResult(result);
    }

    public Task<Student?> GetByIdAsync(int id) =>
        Task.FromResult(_students.FirstOrDefault(s => s.Id == id));

    public async Task AddOrUpdateAsync(Student student)
    {
        await _lock.WaitAsync();
        try
        {
            if (student.Id == 0)
            {
                student.Id = _nextId++;
                _students.Add(student);
                return;
            }

            var existing = _students.FirstOrDefault(s => s.Id == student.Id);
            if (existing is null)
            {
                return;
            }

            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Gpa = student.Gpa;
            existing.EnrollmentDate = student.EnrollmentDate;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _lock.WaitAsync();
        try
        {
            var existing = _students.FirstOrDefault(s => s.Id == id);
            if (existing is not null)
            {
                _students.Remove(existing);
            }
        }
        finally
        {
            _lock.Release();
        }
    }
}
