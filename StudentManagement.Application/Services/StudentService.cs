using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Student>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<Student?> GetByIdAsync(int id)
        => _repository.GetByIdAsync(id);

    public Task<Student> AddAsync(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(student.Email))
            throw new ArgumentException("Email is required.");

        if (student.Age < 1 || student.Age > 120)
            throw new ArgumentException("Age must be between 1 and 120.");

        return _repository.AddAsync(student);
    }

    public Task<bool> UpdateAsync(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name) ||
            string.IsNullOrWhiteSpace(student.Email))
        {
            throw new ArgumentException("Name and email are required.");
        }

        if (student.Age < 1 || student.Age > 120)
            throw new ArgumentException("Invalid age.");

        return _repository.UpdateAsync(student);
    }

    public Task<bool> DeleteAsync(int id)
        => _repository.DeleteAsync(id);
}