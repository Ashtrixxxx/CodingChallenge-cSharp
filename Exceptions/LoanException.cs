using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinChallenge_Loan.Exceptions
{
    public class LoanException : Exception
    {


        public LoanException(string message):base(message) { }

        public static void IsLoanAvailable(int n)
        {
            if (n == 0)
            {

                throw new LoanException("The loan Id you have provided is not Available");
            }

        }

    }
}
