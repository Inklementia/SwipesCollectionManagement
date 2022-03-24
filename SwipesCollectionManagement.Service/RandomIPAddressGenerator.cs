using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwipesCollectionManagement.Service
{
    public class RandomIPAddressGenerator
    {
        public string Generate(Random _random)
        {
            return $"{_random.Next(0, 255)}.{_random.Next(0, 255)}.{_random.Next(0, 255)}.{_random.Next(0, 255)}";
        }
    }
}