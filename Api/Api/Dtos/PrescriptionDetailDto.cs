namespace Api.Dtos
{
    public class PrescriptionDetailDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }
        public required string Dosage { get; set; } = string.Empty;
        public required string Instructions { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
