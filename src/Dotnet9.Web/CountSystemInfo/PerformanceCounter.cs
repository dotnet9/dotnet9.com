namespace Dotnet9.Extensions.CountSystemInfo;

public class PerformanceCounter
{
    public long Time { get; set; }

    public float CpuLoad { get; set; }

    public float MemoryUsage { get; set; }

    public float DiskRead { get; set; }

    public float DiskWrite { get; set; }

    public float Upload { get; set; }

    public float Download { get; set; }

    public override string ToString()
    {
        return
            $"当前时间戳: {Time}, CPU当前负载: {CpuLoad}%, 内存使用率: {MemoryUsage}%, 磁盘读: {DiskRead}, 磁盘写: {DiskWrite}, 网络上行: {Upload}, 网络下行: {Download}";
    }
}