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
        try {
            var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return await _service.CreateProduct(clientId, productDTO);
        }
        catch(Exception ex) {
            throw new Exception("creating product was unsecesfull");
        }
    }
    [HttpGet("/bytag")]
    [AllowAnonymous]
    public async Task<List<ProductDTO>> GetByTags([FromBody] Guid tagId) {
        try {
            var products = await _service.GetByTag(tagId);
            return products;
        }
        catch(Exception ex) {
            throw new Exception("getting products was unsecesfull");
        }
    }
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task<ProductDTO> UpdateProduct([FromBody] ProductDTO productDTO, Guid id) {
        try {
            var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return await _service.UpdateProduct(clientId, id, productDTO);
        }
        catch(Exception ex) {
            throw new Exception("updating product was unsecesfull");
        }
    }
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task RemoveProduct(Guid id) {
        try {
            var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _service.RemoveProduct(clientId, id);
        }
        catch(Exception ex) {
            throw new Exception("removing product was unsecesfull");
        }
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<List<ProductDTO>> GetAll() {
        try {
            return await _service.GetAll();
        }
        catch(Exception ex) {
            throw new Exception("getting products was unsecesfull");
        }
    }
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ProductDTO> GetById(Guid id) {
        try {
            return await _service.GetById(id);
        }
        catch(Exception ex) {
            throw new Exception("getting product was unsecesfull");
        }
    }
    [HttpGet("{id:guid}/tag")]
    [AllowAnonymous]
    public async Task<List<TagDTO>> GetAllTags(Guid id) {
        try {
            return await _service.GetAllTags(id);
        }
        catch(Exception ex) {
            throw new Exception("getting tags was unsecesfull");
        }
    }
    [HttpPost("{id:guid}/tag")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task<TagDTO> AddTagToProduct([FromBody] Guid tagId, Guid id) {
        try {
            var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return await _service.AddTagToProduct(clientId, id, tagId);
        }
        catch(Exception ex) {
            throw new Exception("adding tag to product was unsecesfull");
        }
    }
    [HttpDelete("{id:guid}/tag")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task RemoveTag([FromBody] Guid tagId, Guid id) {
        try {
            var clientId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _service.RemoveTag(clientId, id, tagId);
        }
        catch(Exception ex) {
            throw new Exception("removing tag from product was unsecesfull");
        }
    }

}