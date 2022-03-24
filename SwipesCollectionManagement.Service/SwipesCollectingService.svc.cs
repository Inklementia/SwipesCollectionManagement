using SwipesCollectionManagement.DAL.DBO;
using SwipesCollectionManagement.DAL.Repositories;
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
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class SwipesCollectingService : ISwipesCollectingService
    {
        private SynConnection _connectionInstance = SynConnection.GetInstance();
        private static List<TerminalModel> _terminals = new List<TerminalModel>();
        private Semaphore semaphore = new Semaphore(3, 3);
        private int _terminalNumber = 8;
        RandomIPAddressGenerator generator = new RandomIPAddressGenerator();

        //repository to add.get from db
        private readonly SwipeRepository _repository;

        public SwipesCollectingService(SwipeRepository repository)
        {
            _repository = repository;
        }

        private void CreateTerminals()
        {
            _terminals.Clear();
            Random random = new Random();
            for (int i = 0; i < _terminalNumber; i++)
            {
                string ip = generator.Generate(random);
                _terminals.Add(new TerminalModel(ip));
            }
        }

        private void PopulateTerminal(TerminalModel terminal)
        {
            semaphore.WaitOne();
            terminal.Status = TerminalStatus.InProcess;

            string swipesData = _connectionInstance.RetrieveSwipes(terminal.IPAddress);

            string[] swipes = swipesData.Split('\n');

            foreach (string swipe in swipes)
            {
                terminal.Swipes.Add(new SwipeModel(terminal.IPAddress, swipe.Split(',')));
            }

            _repository.CreateRange(MapSwipeModelToSwipe(terminal.Swipes));
            semaphore.Release();
        }

        public List<TerminalModel> GetStatus()
        {
            return _terminals;
        }

        public void StartCollectingSwipes()
        {
            CreateTerminals();
            List<AutoResetEvent> events = new List<AutoResetEvent>();

            for (int i = 0; i < _terminals.Count; i++)
                new Thread((object obj) =>
                {
                    int count = (int)obj;
                    events.Add(new AutoResetEvent(false));

                    PopulateTerminal(_terminals[count]);

                    _terminals[count].Status = TerminalStatus.Finished;
                    events[count].Set();
                }).Start(i);

            new Thread(() =>
            {
                WaitHandle.WaitAll(events.ToArray());
            }).Start();
        }

        public List<SwipeModel> GetAllSwipes()
        {
            List<Swipe> swipes = _repository.GetAll();
            List<SwipeModel> swipesModels = new List<SwipeModel>();
            swipesModels = swipes.Select(x => new SwipeModel()
            {
                //do your variable mapping here 
                PersonID = x.PersonID,
                IPAddress = x.IPAddress,
                SwipeDateTime = x.SwipeDateTime,
                SwipeDirection = x.SwipeDirection
            }).ToList();

            return swipesModels;
        }
        public void DeleteAllSwipes()
        {
            _repository.DeleteAll();
        }

        public List<Swipe> MapSwipeModelToSwipe(List<SwipeModel> swipeModels)
        {
            List<Swipe> swipes = new List<Swipe>();
            swipes = swipeModels.Select(x => new Swipe()
            {
                //do your variable mapping here 
                PersonID = x.PersonID,
                IPAddress = x.IPAddress,
                SwipeDateTime = x.SwipeDateTime,
                SwipeDirection = x.SwipeDirection
            }).ToList();
            return swipes;
        }
      
    }
}
