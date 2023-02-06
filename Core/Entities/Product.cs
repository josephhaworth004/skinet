namespace Core.Entities
{
    // Entity Framework 2.0: Object Relational Mapper
    // ORM stores objects in database
    
    public class Product
    {
        // EF is convention based - it will automatically make "Id" the primary key 
        // It would not work if the prop was called something like TheId
        public int Id { get; set; } 
        public string Name { get; set; } 
    }
}