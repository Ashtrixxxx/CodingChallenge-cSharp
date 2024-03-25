using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinChallenge_Loan.Models
{
    

        public class HomeLoan : Loan
        {
            public string PropertyAddress { get; set; }
            public double PropertyValue { get; set; }


        public override string ToString()
        {
            return $"Loan ID {LoanId} \t Loan Status {LoanStatus} Loan TYpe {LoanType}";
        }

    }

    
}
