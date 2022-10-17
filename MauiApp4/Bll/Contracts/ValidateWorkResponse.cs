using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.Contracts
{
    /// <summary>
    /// Word validation result
    /// </summary>
    public class ValidateWorkResponse
    {
        public bool[] CharInPlace { get; internal set; }
        public bool[] CharExists { get; internal set; }
        public bool Validated { get; internal set; }
        public bool GameFinished { get; internal set; }
        public bool WordAccepted { get; internal set; }
    }
}
