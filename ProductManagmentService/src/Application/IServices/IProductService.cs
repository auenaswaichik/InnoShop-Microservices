using Shared.DTOs;

namespace Application.IService;

public interface IProductService {
    public Task<List<ProductDTO>> GetAll();
    public Task<ProductDTO> GetById(Guid id);
    public Task<ProductDTO> CreateProduct(ProductDTO productDTO);
    public Task RemoveProduct(Guid id);
    public Task<ProductDTO> UpdateProduct(Guid id, ProductDTO productDTO);
    public Task<TagDTO> AddTagToProduct(Guid id);
    public Task<List<TagDTO>> GetAllTags();
    public Task RemoveTag(Guid id);
}