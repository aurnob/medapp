namespace Api.Dtos
{
    public class AppointmentListItemDto
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? VisitType { get; set; }
        public string? Notes { get; set; }
        public string? Diagnosis { get; set; }

        public int PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? PatientEmail { get; set; }

        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorSpecialty { get; set; }
    }
}
