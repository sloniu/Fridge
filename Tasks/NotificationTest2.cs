using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Notifications;

namespace Tasks
{
    class NotificationTest2
    {
        public static void CreateToast(object objectInfo)
        {
            var xDoc = new XDocument(
                new XElement("toast",
                    new XElement("visual",
                        new XElement("binding", new XAttribute("template", "ToastGeneric"),
                            new XElement("text", "C# Corner"),
                            new XElement("text", "Did you got MVP award?")
                            )
                        ), // actions  
                    new XElement("actions",
                        new XElement("action", new XAttribute("activationType", "background"),
                            new XAttribute("content", "Yes"), new XAttribute("arguments", "yes")),
                        new XElement("action", new XAttribute("activationType", "background"),
                            new XAttribute("content", "No"), new XAttribute("arguments", "no"))
                        )
                    )
                );

            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
            //return xmlDoc;
            //var xmdock = NotificationTest.CreateToast();
            var toast = new ToastNotification(xmlDoc);
            var notifi = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
            notifi.Show(toast);
        }

        public static async Task Timer()
        {
            //Timer timer = new Timer(CreateToast, "Some state", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            TimerCallback tmCallback = CreateToast;
            Timer timer = new Timer(tmCallback, "test", TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));

        }
    }
}
