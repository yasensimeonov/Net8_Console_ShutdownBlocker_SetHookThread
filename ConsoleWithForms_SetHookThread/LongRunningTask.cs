namespace ConsoleWithForms_SetHookThread
{
    public class LongRunningTask
    {
        private string _pathToFile;
        private StreamWriter _streamWriter;
        private Thread _hookThread;
        private DateTime _currentTimeFrame;
        private CancellationTokenSource _cts;

        public LongRunningTask(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        public void StartWorking()
        {
            _cts = new CancellationTokenSource();

            _hookThread = new Thread(() => SetHook(_cts.Token));
            _hookThread.IsBackground = true;
            _hookThread.Start();            
        }

        public void StopWorking()
        {
            _cts.Cancel();

            _hookThread.Join();
        }

        private void SetHook(CancellationToken token)
        {
            Form form = new ConsoleReceiver();

            // Handle cancellation request
            token.Register(() => form.Invoke(new Action(() => form.Close())));

            _streamWriter = new StreamWriter($"{_pathToFile}\\longRunnerLog_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}.txt");
            _currentTimeFrame = DateTime.Now;

            _streamWriter.Flush();
            _streamWriter.WriteLine();
            _streamWriter.WriteLine();
            _streamWriter.WriteLine($"Long running task started running at: {_currentTimeFrame}");
            _streamWriter.Flush();

            Application.Run(form);

            DisposeData();
        }

        private void DisposeData()
        {
            _streamWriter.WriteLine();
            _streamWriter.WriteLine();
            _streamWriter.Flush();
            _streamWriter.WriteLine($"Long running task stopped running at: {_currentTimeFrame}");
            _streamWriter.Flush();

            _streamWriter.Close();
            _streamWriter.Dispose();
        }

    }
}
