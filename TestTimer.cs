using System.Diagnostics;

namespace VideoLOB
{
    internal class TestTimer
    {
        public Stopwatch stopwatch = new Stopwatch();
        public float timeElapsed;
        public void StartTimer(string functionName)
        {
            stopwatch.Start();
            timeElapsed = stopwatch.ElapsedTicks;
        }
        public void StopTimer(string functionName)
        {
            stopwatch.Stop();
            Debug.WriteLine($"{functionName} took {stopwatch.ElapsedTicks - timeElapsed} ticks to execute.");
            File.AppendAllText("timer.log", $"{functionName} took: {stopwatch.ElapsedTicks - timeElapsed} ticks to execute." + Environment.NewLine);
        }
        public void Testing(string functionName, int operations = 1)
        {
            for (int i = 0; i < operations; i++)
            {
                Debug.WriteLine("Iteration : " + i.ToString());
                StartTimer(functionName);
                // Execute function here.
                StopTimer(functionName);
            }
        }
    }
}
