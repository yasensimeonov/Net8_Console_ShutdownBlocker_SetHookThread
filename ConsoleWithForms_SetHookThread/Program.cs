using ConsoleWithForms_SetHookThread;

LongRunningTask longRunner = new LongRunningTask("C:\\Tmp");
longRunner.StartWorking();

Console.WriteLine("Working...");
Console.WriteLine("Press any key to stop...");
Console.ReadLine();

longRunner.StopWorking();
Console.WriteLine("Stopped successfully.");
