using CodinChallenge_Loan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinChallenge_Loan.Repository
{
    public interface ILoanRepository
    {
        int ApplyLoan(Loan loan);
        double CalculateInterest(int loanId);
        void LoanStatus(int loanId);
        double CalculateEMI(int loanId);
        void LoanRepayment(int loanId, double amount);
        List<Loan> GetAllLoan();
        Loan GetLoanById(int loanId);
    }
}
