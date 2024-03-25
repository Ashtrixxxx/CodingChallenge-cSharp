using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodinChallenge_Loan.Exceptions
{
    public class InvalidInputException : Exception
    {

        public InvalidInputException(string message):base(message) { }


        public static void CheckIfInteger(string input, ref int value)
        {
            if (!int.TryParse(input, out int value1))
            {
                throw new InvalidInputException("Please Provide input in Number Format..");
            }
            value = value1;
        }

    }
}
