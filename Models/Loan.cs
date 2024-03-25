using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinChallenge_Loan.Models
{


    public enum LoanType
    {
        CarLoan,
        HomeLoan
    }

    public enum LoanStatus
    {
        Pending = 0,
        Approved = 1,
    }
    public  class Loan
    {

        public int LoanId { get; set; }

        public int CustomerId {  get; set; }

        public decimal PrincipalAmount { get; set; }

        public double InterestRate { get; set; }

        public int LoanTerm {  get; set; } //Months

        public LoanType LoanType { get; set; }

        public LoanStatus LoanStatus { get; set; } 

        public Loan()
        {

        }

        public override string ToString()
        {
            return  $"Loan Id = {LoanId} \t Customer id = {CustomerId} \t Principal Amount = {PrincipalAmount} \t LoanType = {LoanType} \t LoanStatus = {LoanStatus}";
        }

    }
}
