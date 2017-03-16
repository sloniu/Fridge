using System.Diagnostics;
using Windows.ApplicationModel.Background;

namespace Tasks
{
    public sealed class BgTask : IBackgroundTask
    {
        //private static IBackgroundTaskInstance _taskInstance;


        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //            _taskInstance = taskInstance;
            //            BackgroundTaskDeferral _deferral = _taskInstance.GetDeferral();

            //
            // TODO: Insert code to start one or more asynchronous methods using the
            //       await keyword, for example:
            //
            // await ExampleMethodAsync();
            //

            //await NotificationTest2.Timer();


            Debug.WriteLine("Tworzę Toast");
            Notification.CreateToast();
            Debug.WriteLine("Stworzyłem Toast");
            //_deferral.Complete();
        }
    }
}
