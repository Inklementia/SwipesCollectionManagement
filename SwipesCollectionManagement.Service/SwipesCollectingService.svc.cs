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
    //ensures that there is 1 instance of a service, whcih can be used by multiple clients
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class SwipesCollectingService : ISwipesCollectingService
    {
        private SynConnection _connectionInstance = SynConnection.GetInstance();
        private static List<TerminalModel> _terminals = new List<TerminalModel>();


        // to make only 3 terminals process at a time
        private Semaphore semaphore = new Semaphore(3, 3);

        private int _terminalsNumber = 5;

        //repository to add/get from db
        private readonly SwipeRepository _repository;

        // can be used bacause of autofac container
        public SwipesCollectingService(SwipeRepository repository)
        {
            _terminals.Clear();// upon runnin application clear list of terminals
            _repository = repository; 
        }

        //retrieves and process swipes
        private void PopulateTerminal(object terminalModelObj)
        {
            TerminalModel terminal = (TerminalModel)terminalModelObj;

            // 3 terminals at a time
            semaphore.WaitOne();
            // mark terminal status as InProcess
            terminal.Status = TerminalStatus.InProcess;

            // retrieve raw data for swipes for 1 terminal
            string swipesData = _connectionInstance.RetrieveSwipes(terminal.IPAddress);
            // split data in rows
            string[] swipes = swipesData.Split('\n');
            //split rows by "," to create swipe fields
            foreach (string swipe in swipes)
            {
                //create swipes for terminal
                terminal.Swipes.Add(new SwipeModel(terminal.IPAddress, swipe.Split(',')));
            }
            //save swipes to db
            _repository.CreateRange(MapSwipeModelToSwipe(terminal.Swipes));
            // mark terminal status as Finished
            terminal.Status = TerminalStatus.Finished;
            semaphore.Release();
        }
        //return termianls with according statuses
        public List<TerminalModel> GetStatus()
        {
            return _terminals;
        }
        //create Terminals with random ips
        private void CreateTerminals()
        {
            Random random = new Random();
            for (int i = 0; i < _terminalsNumber; i++)
            {
                //generate random ip
                string ip = RandomIPAddressGenerator.Generate(random);

                //create new terminal with that random ip
                TerminalModel newTerminal = new TerminalModel(ip);

                //add to terminal list
                _terminals.Add(newTerminal);
                var thread = new Thread(PopulateTerminal);
                thread.Start(newTerminal);
            }
        }
        // called from client
        public void StartCollectingSwipes()
        {
            CreateTerminals();
        }

        //called from client to show all swipes from db
        public List<SwipeModel> GetAllSwipes()
        {
            // gets all swipes from repository
            List<Swipe> swipes = _repository.GetAll();

            //maps swipe (used for db) to swipeModel
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
        //deletes all swipes from database
        public void DeleteAllSwipes()
        {
             _repository.DeleteAll();
        }

        // maps swipeModel to swipe
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
