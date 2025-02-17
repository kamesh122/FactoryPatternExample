namespace FactoryPatternExample.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }  
        public string? Email { get; set; }  
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }



}
