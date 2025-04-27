using System.Security.Claims;
using Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http.Logging;
using Shared.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/product")]
[Authorize]
public class ProductController : ControllerBase {
    private readonly IProductService _service;
    public ProductController(IProductService service) =>
        _service = service;
    [HttpPost]
    [Authorize(Policy = "RequireClientRole")]
    public async Task<ProductDTO> CreateProduct([FromBody] ProductDTO productDTO) {
        var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return await _service.CreateProduct(clientId, productDTO);
    }
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task<ProductDTO> UpdateProduct([FromBody] ProductDTO productDTO, Guid id) {
        var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return await _service.UpdateProduct(clientId, id, productDTO);
    }
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task RemoveProduct(Guid id) {
        var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _service.RemoveProduct(clientId, id);
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<List<ProductDTO>> GetAll() {
        return await _service.GetAll();
    }
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ProductDTO> GetById(Guid id) {
        return await _service.GetById(id);
    }
    [HttpGet("{id:guid}/tags")]
    [AllowAnonymous]
    public async Task<List<TagDTO>> GetAllTags(Guid id) {
        return await _service.GetAllTags(id);
    }
    [HttpPost("{id:guid}/tag")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task<TagDTO> AddTagToProduct([FromBody] Guid tagId, Guid id) {
        var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return await _service.AddTagToProduct(clientId, id, tagId);
    }
    [HttpDelete("{id:guid}/tag")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task RemoveTag([FromBody] Guid tagId, Guid id) {
        var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _service.RemoveTag(clientId, id, tagId);
    }

}