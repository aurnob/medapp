namespace Api.Dtos
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
