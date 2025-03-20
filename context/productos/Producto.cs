using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace context.productos
{
    public class Producto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("descripcion")]
        public string? Descripcion { get; set; }

        [BsonElement("precio")]
        public decimal Precio { get; set; }

        [BsonElement("Categoria")]
        public string? Categoria { get; set; }
    }
}
