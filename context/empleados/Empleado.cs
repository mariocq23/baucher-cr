using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace context.empleados
{
    public class Empleado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("apellido")]
        public string Apellido { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("departamento")]
        public string? Departmento { get; set; }
    }
}
