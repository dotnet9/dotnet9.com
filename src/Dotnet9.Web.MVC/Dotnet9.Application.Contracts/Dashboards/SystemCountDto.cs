namespace Dotnet9.Application.Contracts.Dashboards;

public class SystemCountDto
{
    public int PostCount { get; set; }

    public int IPOf24Hours { get; set; }

    public int NotFoundRequestIn24Hours { get; set; }

    public int? CpuLoad { get; set; }

    public int? MemoryUsage { get; set; }

    public string? DiskRead { get; set; }

    public string? DiskWrite { get; set; }
}