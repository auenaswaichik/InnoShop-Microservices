namespace Shared.DTOs;

public record class ProductDTO (
    string ProductName,
    decimal ProductPrice,
    string ProductDescription
);