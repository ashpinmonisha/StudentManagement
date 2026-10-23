using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Student>> GetAllAsync()
    {
        return _context.Students
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<Student?> GetByIdAsync(int id)
    {
        return _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student> AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return student;
    }

    public async Task<bool> UpdateAsync(Student student)
    {
        var existing = await _context.Students
            .FindAsync(student.Id);

        if (existing is null)
            return false;

        existing.Name = student.Name;
        existing.Email = student.Email;
        existing.Age = student.Age;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student is null)
            return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return true;
    }
}