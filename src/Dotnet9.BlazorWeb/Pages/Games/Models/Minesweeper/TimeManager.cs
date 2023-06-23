using System.Diagnostics;

namespace Dotnet9.BlazorWeb.Pages.Games.Models.Minesweeper;

public class TimeManager
{
    Stopwatch _Timer;

    public int ElapsedSeconds => (int)_Timer.Elapsed.TotalSeconds;

    public TimeManager()
    {
        _Timer = new Stopwatch();
    }

    public void Start()
    {
        _Timer.Start();
    }

    public void Stop()
    {
        _Timer.Stop();
    }
}