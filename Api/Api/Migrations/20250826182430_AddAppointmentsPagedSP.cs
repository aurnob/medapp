using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentsPagedSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetAppointmentsPaged 
                    @PageNumber INT = 1,
                    @PageSize INT = 10,
                    @DoctorId INT = NULL,
                    @VisitType NVARCHAR(50) = NULL,
                    @Search NVARCHAR(100) = NULL
                AS
                BEGIN
                    SET NOCOUNT ON;

                    -- Create a temporary table to store filtered appointments
                    CREATE TABLE #FilteredAppointments (
                        AppointmentId INT,
                        AppointmentDate DATETIME,
                        VisitType NVARCHAR(50),
                        Notes NVARCHAR(MAX),
                        Diagnosis NVARCHAR(MAX),
                        PatientId INT,
                        PatientName NVARCHAR(100),
                        PatientEmail NVARCHAR(100),
                        DoctorId INT,
                        DoctorName NVARCHAR(100),
                        DoctorSpecialty NVARCHAR(100)
                    );

                    -- Insert filtered data
                    INSERT INTO #FilteredAppointments
                    SELECT 
                        a.Id AS AppointmentId,
                        a.AppointmentDate,
                        a.VisitType,
                        a.Notes,
                        a.Diagnosis,
                        p.Id AS PatientId,
                        p.Name AS PatientName,
                        p.Email AS PatientEmail,
                        d.Id AS DoctorId,
                        d.Name AS DoctorName,
                        d.Specialty AS DoctorSpecialty
                    FROM 
                        Appointments a
                    INNER JOIN Patients p ON a.PatientId = p.Id
                    INNER JOIN Doctors d ON a.DoctorId = d.Id
                    WHERE 
                        (@DoctorId IS NULL OR a.DoctorId = @DoctorId)
                        AND (@VisitType IS NULL OR a.VisitType = @VisitType)
                        AND (
                            @Search IS NULL 
                            OR p.Name LIKE '%' + @Search + '%'
                            OR d.Name LIKE '%' + @Search + '%'
                        );

                    -- Return paginated result
                    SELECT *
                    FROM #FilteredAppointments
                    ORDER BY AppointmentDate DESC
                    OFFSET (@PageNumber - 1) * @PageSize ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    -- Return total count
                    SELECT COUNT(*) AS TotalCount FROM #FilteredAppointments;

                    -- Clean up
                    DROP TABLE #FilteredAppointments;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetAppointmentsPaged");
        }
    }
}
