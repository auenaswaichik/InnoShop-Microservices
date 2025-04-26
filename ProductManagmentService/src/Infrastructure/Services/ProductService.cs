using Application.IManagers;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Shared.DTOs;

namespace Infrastructure.Service;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    public ProductService(IRepositoryManager repository, IMapper mapper) {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<TagDTO> AddTagToProduct(Guid productId, Guid tagId)
    {
        var tag = await _repository.TagRepository.GetById(tagId);
        var product = await _repository.ProductRepository.GetById(productId);
        tag.Products.Add(product);
        product.Tags.Add(tag);
        var productDTO = _mapper.Map<ProductDTO>(await _repository.ProductRepository.UpdateProduct(productId, product));
        var tagDTO = _mapper.Map<TagDTO>(await _repository.TagRepository.UpdateTag(tagId, tag));
        await _repository.SaveAsync();
        return tagDTO;
    }

    public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);
        var createdProduct = await _repository.ProductRepository.CreateProduct(product);
        await _repository.SaveAsync();
        return _mapper.Map<ProductDTO>(createdProduct);
    }

    public async Task<List<ProductDTO>> GetAll()
    {
        return _mapper.Map<List<ProductDTO>>(await _repository.ProductRepository.GetAll());
    }

    public async Task<List<TagDTO>> GetAllTags(Guid productId)
    {
        var product = await _repository.ProductRepository.GetById(productId);
        var tags = product.Tags;
        return _mapper.Map<List<TagDTO>>(tags); 
    }

    public async Task<ProductDTO> GetById(Guid id)
    {
        var product = await _repository.ProductRepository.GetById(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task RemoveProduct(Guid id)
    {
        await _repository.ProductRepository.RemoveProduct(id);
        await _repository.SaveAsync();
    }

    public async Task RemoveTag(Guid productId, Guid tagId)
    {   
        var tag = await _repository.TagRepository.GetById(tagId);
        var product = await _repository.ProductRepository.GetById(productId);
        tag.Products.Remove(product);
        product.Tags.Remove(tag);
        await _repository.SaveAsync();   
    }

    public async Task<ProductDTO> UpdateProduct(Guid id, ProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);
        var updatedProduct = await _repository.ProductRepository.UpdateProduct(id, product);
        await _repository.SaveAsync();
        return _mapper.Map<ProductDTO>(updatedProduct);
    }
}