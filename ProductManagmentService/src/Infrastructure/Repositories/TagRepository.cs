using Application.IRepository;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ProductManagmentServiceDbContext _context;
    public TagRepository(ProductManagmentServiceDbContext context) =>
        _context = context;
    public async Task<Tag> CreateTag(Tag tag)
    {
        await _context.Tags   
            .AddAsync(tag);
        return await _context.Tags.FirstOrDefaultAsync(m => m.TagName == tag.TagName);
    }

    public async Task<List<Tag>> GetAll()
    {
        return await _context.Tags
            .ToListAsync();
    }

    public async Task<Tag> GetById(Guid id)
    {
        return await _context.Tags
            .FirstOrDefaultAsync(m => m.TagId == id);
    }

    public async Task RemoveTag(Guid id)
    {
        _context.Tags
            .Remove(
                await GetById(id)
            );
    }

    public async Task<Tag> UpdateTag(Guid id, Tag tag)
    {
        var tempEntry = await GetById(id);
        _context.Tags
            .Entry(tag)
            .CurrentValues
            .SetValues(tag);
        return tag;
    }
}