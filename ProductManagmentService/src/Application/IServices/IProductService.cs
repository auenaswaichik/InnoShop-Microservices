using Shared.DTOs;

namespace Application.IService;

public interface IProductService {
    public Task<List<ProductDTO>> GetAll();
    public Task<ProductDTO> GetById(Guid id);
    public Task<ProductDTO> CreateProduct(ProductDTO productDTO);
    public Task RemoveProduct(Guid id);
    public Task<ProductDTO> UpdateProduct(Guid id, ProductDTO productDTO);
    public Task<TagDTO> AddTagToProduct(Guid productId, Guid tagId);
    public Task<List<TagDTO>> GetAllTags(Guid productId);
    public Task RemoveTag(Guid productId, Guid tagId);
}