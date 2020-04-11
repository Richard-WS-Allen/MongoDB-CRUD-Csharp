using MongoDB.Bson;

namespace mongo_crud
{
    class Course
    {
        public ObjectId id { get; set; }
        public string CourseName { get; set; }
        public double Rating{ get; set; }
        public double Slope { get; set; }
    }
}
