namespace Api.Models
{
    public class PrescriptionDetail
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }
        public string Dosage { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Appointment? Appointment { get; set; }
        public Medicine? Medicine { get; set; }
    }
}
