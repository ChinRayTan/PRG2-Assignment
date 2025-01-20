//==========================================================
// Student Number   : S10268411
// Student Name	: Tan Chin Ray
// Partner Name	: -
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268411_PRG2Assignment
{
    internal class BoardingGate
    {
        public string GateName { get; set; }

        public bool SupportsCFFT { get; set; }

        public bool SupportsDDJB { get; set; }

        public bool SupportsLWTT { get; set; }

        public Flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
        }

        public double CalculateFees()
        {
            return Flight.CalculateFees() + 300;
        }

        public override string ToString()
        {
            return $"Gate Name: {GateName} \tSupports CFFT: {SupportsCFFT} \tSupports DDJB: {SupportsDDJB} \tSupports LWTT: {SupportsLWTT} \tFlight: {Flight}";
        }
    }
}
