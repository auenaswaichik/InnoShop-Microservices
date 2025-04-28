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
        try {
            return await _service.CreateTag(tagDTO);
        }
        catch (Exception ex) {
            throw new Exception("creating tag was unsuccesfull");
        }
    }
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<TagDTO> UpdateTag([FromBody] TagDTO tagDTO, Guid id) {
        try {
            return await _service.UpdateTag(id, tagDTO);
        }
        catch (Exception ex) {
            throw new Exception("updating tag was unsuccesfull");
        }
    }
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task DeleteTag(Guid id) {
        try {
            await _service.RemoveTag(id);
        }
        catch (Exception ex) {
            throw new Exception("removing tag was unsuccesfull");
        }
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<List<TagDTO>> GetAll() {
        try {
            return await _service.GetAll();
        }
        catch (Exception ex) {
            throw new Exception("getting all tags was unsuccesfull");
        }
    }
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<TagDTO> GetById(Guid id) {
        try {
            return await _service.GetById(id);
        }
        catch (Exception ex) {
            throw new Exception("getting tag was unsuccesfull");
        }
    }
}