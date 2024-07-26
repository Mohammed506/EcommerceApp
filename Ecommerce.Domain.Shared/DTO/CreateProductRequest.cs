using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Domain.Shared.DTO;

public class CreateProductRequest
{

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Description")]
    public string Description { get; set; }

    [BsonElement("Price")]
    public decimal Price { get; set; }

    [BsonElement("Quantity")]
    public int Quantity { get; set; }
}

