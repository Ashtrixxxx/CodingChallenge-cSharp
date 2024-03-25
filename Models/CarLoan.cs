using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinChallenge_Loan.Models
{
    public class CarLoan : Loan
    {
        public string CarModel { get; set; }
        public double CarValue { get; set; }

        public override string ToString()
        {
            return $"Loan ID {LoanId} \t Loan Status {LoanStatus} Loan TYpe {LoanType}";
        }

    }

}
