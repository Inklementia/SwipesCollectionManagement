using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwipesCollectionManagement.Service
{
    public static class RandomIPAddressGenerator
    {
        public static string Generate(Random _random)
        {
            //generates random ip address
            return $"{_random.Next(0, 255)}.{_random.Next(0, 255)}.{_random.Next(0, 255)}.{_random.Next(0, 255)}";
        }
    }
}