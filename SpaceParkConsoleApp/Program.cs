using SpaceParkConsoleApp.ApiCalls;
using SpaceParkConsoleApp.Helper;
using System;
using System.Globalization;

namespace SpaceParkConsoleApp
{
    class Program
    {
        private const double maxLengthToParkStarship = 35;
        private const int totalParkingLots = 5;
        static void Main(string[] args)
        {
            //Added this line to Parse double values to not mix "." and ","
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine("Welcome dear traveller!\n");

            var showSpacePort = GetSpaceport.CreateSpaceportAsync();
        }
    }
}

