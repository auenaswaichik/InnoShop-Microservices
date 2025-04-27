using Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/tag")]
[Authorize]
public class TagController : ControllerBase {
    private readonly ITagService _service;
    public TagController(ITagService service) =>
        _service = service;
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<TagDTO> CreateTag([FromBody] TagDTO tagDTO) {
        return await _service.CreateTag(tagDTO);
    }
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<TagDTO> UpdateTag([FromBody] TagDTO tagDTO, Guid id) {
        return await _service.UpdateTag(id, tagDTO);
    }
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task DeleteTag(Guid id) {
        await _service.RemoveTag(id);
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<List<TagDTO>> GetAll() {
        return await _service.GetAll();
    }
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<TagDTO> GetById(Guid id) {
        return await _service.GetById(id);
    }
}