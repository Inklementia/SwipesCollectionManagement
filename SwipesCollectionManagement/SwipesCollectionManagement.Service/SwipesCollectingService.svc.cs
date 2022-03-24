using SwipesCollectionManagement.Service.Enums;
using SwipesCollectionManagement.Service.Models;
using SynConnectDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace SwipesCollectionManagement.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SwipesCollectingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SwipesCollectingService.svc or SwipesCollectingService.svc.cs at the Solution Explorer and start debugging.
    public class SwipesCollectingService : ISwipesCollectingService
    {
        Semaphore semaphore = new Semaphore(3, 3);
        static List<Terminal> terminals = new List<Terminal>();
        SynConnection connection = SynConnectDLL.SynConnection.GetInstance();

        private void UpdateTerminals()
        {
            terminals.Clear();
            Random random = new Random();
            for (int i = 0; i < 10; i++) terminals.Add(new Terminal(random));
        }

        private void FillTerminalSwipes(Terminal terminal)
        {
            semaphore.WaitOne();
            terminal.Status = Status.InProgress;
            terminal.ParseSwipes(terminal.IPAddress, connection.RetrieveSwipes(terminal.IPAddress));
            semaphore.Release();
        }

        public Dictionary<string, string> GetStatus()
        {
            return terminals.ToDictionary(t => t.IPAddress, t => t.Status.ToString());
        }

        void ISwipesCollectingService.StartCollectingSwipes()
        {
            UpdateTerminals();
            List<AutoResetEvent> events = new List<AutoResetEvent>();

            for (int i = 0; i < terminals.Count; i++)
                new Thread((object obj) =>
                {
                    int count = (int)obj;
                    events.Add(new AutoResetEvent(false));
                    FillTerminalSwipes(terminals[count]);
                    terminals[count].Status = Status.Finished;
                    events[count].Set();
                }).Start(i);

            new Thread(() =>
            {
                WaitHandle.WaitAll(events.ToArray());
            }).Start();
        }

        public ICollection<Swipe> GetAllSwipes()
        {
            throw new NotImplementedException();
        }

        /*
        public ICollection<Terminal> GetStatus()
        {
            return _terminalList.ToList();
        }
        public void PopulateIPAdressesList()
        {
            _terminalIPAdressesList.Add("192.168.1.23");
            _terminalIPAdressesList.Add("202.200.63.43");
            _terminalIPAdressesList.Add("100.67.102.153");
            _terminalIPAdressesList.Add("140.62.42.69");
            _terminalIPAdressesList.Add("27.69.77.230");
        }
        public void StartCollectingSwipes()
        {
          
                _terminalList.Clear();

                var threads = new Thread[6];

                for (int i = 0; i < 6; i++)
                {
                    var thread = new Thread(RetrieveSwipesFromTerminalByIP);
                    threads[i] = thread;

                    thread.Start("192.168.1."+i);
                }

                foreach (var item in threads)
                {
                    item.Join();
                }
            
        }
        private void RetrieveSwipesFromTerminalByIP(object IPAddressObj)
        {
            var IPAddress = IPAddressObj.ToString();

            var terminal = _terminalList.Find(x => string.Equals(IPAddress, x.IPAddress));

            if (terminal == null)
            {
                terminal = new Terminal()
                {
                    IPAddress = IPAddress,
                    Status = SwipeStatus.Waiting
            };
                _terminalList.Add(terminal);
            }

            semaphore.WaitOne();

            terminal.Status = SwipeStatus.InProgress;

            // dll
            var swipeListStr = _connectionInstance.RetrieveSwipes(IPAddress);

            var swipes = ParseSwipesData(swipeListStr, IPAddress);

            //_repository.CreateMultiple(swipes);

            terminal.Status = SwipeStatus.Finished;

            semaphore.Release();
        }

        private List<Swipe> ParseSwipesData(string dataStr, string IPAddress)
        {

            var swipeListStr = dataStr.Split('\n');
            var swipeList = new List<Swipe>();

            foreach (var swipeStr in swipeListStr)
            {
                string[] swipeData = swipeStr.Split(',');

                var swipe = new Swipe
                {
                    IPAddress = IPAddress,
                    ID = int.Parse(swipeData[0]),
                    SwipeDateTime = DateTime.Parse(swipeData[1]),
                    SwipeDirection = swipeData[2]
                };
                swipeList.Add(swipe);
            }
            return swipeList;
        }
        */
    }
}
