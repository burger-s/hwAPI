using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace hwAPI.Model
{
    public class Student
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int gender { get; set; }
    }
}
