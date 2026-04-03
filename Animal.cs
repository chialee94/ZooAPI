namespace ZooAPI.Models
{
    public class Animal
    {
        public int Id { get; set; }                 // Primary Key
        public required string Name { get; set; }   // Animal name (required)
        public required string Species { get; set; }// Type of animal (required)
        public required string HealthStatus { get; set; } // Health condition (required)
    }
}