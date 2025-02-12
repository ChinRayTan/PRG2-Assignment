﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268411_PRG2Assignment
{
    internal class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee = 300) : base(flightNumber, origin, destination, expectedTime)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            if (Origin == "Singapore (SIN)")
                return 800 + 300;
            else
                return 500 + 300;
        }

        public override string ToString()
        {
            return $"Flight Type: DDJB \n{base.ToString()} \tRequest Fee: {RequestFee}";
        }
    }
}
