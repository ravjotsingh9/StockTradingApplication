using System;
using System.Timers;
using System.Threading;
using System.Timers;

public class ServerUpdateThread
{
    private static System.Timers.Timer theTimer;
    static Thread bgThread = new Thread(new ThreadStart(serverBgThread));

    // background thread to update stock price
    static public void serverBgThread()
    {
        // create a timer with a two mins interval
        theTimer = new System.Timers.Timer(120000);
        
        // add the Elapsed event for the timer 
        theTimer.Elapsed += OnTimedEvent;
        theTimer.Enabled = true;
    }

    // the method to run when the timer is raised
    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        // here to add the updateAllPrice function
        Console.WriteLine("All stock prices updated at {0}", e.SignalTime);
    }


    // entrance of the program
    public static void Main()
    {
        bgThread.Start();
    }



}