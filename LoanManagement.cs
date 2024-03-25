using CodinChallenge_Loan.Repository;
using CodinChallenge_Loan.Exceptions;
using CodinChallenge_Loan.Models;
using System.Text;

namespace CodinChallenge_Loan
{
    internal class LoanManagement
    {
        static void Main(string[] args)
        {

            ILoanRepositoryImpl loanRepositoryImpl = new ILoanRepositoryImpl();

            while (true)
            {
                try
                {


                    Console.WriteLine("Loan Management Application");
                    Console.WriteLine("----------------------------");

                    Console.WriteLine("1. Apply for a Loan");
                    Console.WriteLine("2. Calulate Interest for a Loan");
                    Console.WriteLine("3. Check Loan Status ");
                    Console.WriteLine("4. Calculate Emi");
                    Console.WriteLine("5. Loan Repayment");
                    Console.WriteLine("6. Get All the loans");
                    Console.WriteLine("7. Get Loan By id");

                    Console.WriteLine("Enter Your OPtion");
                    string id = Console.ReadLine();
                    int option = 0;

                    InvalidInputException.CheckIfInteger(id, ref option);


                    switch (option)
                    {

                        case 1:

                            Console.WriteLine("Apply For a Loan");
                            Console.WriteLine("Select The type of Loan 1. Home Loan 2.CarLoan");
                            int type = Convert.ToInt32(Console.ReadLine());
                            switch (type)
                            {
                                case 1:
                                    Console.WriteLine("Enter The Customer ID");
                                    int CustomerId = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter the principal Amount");
                                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                                    Console.WriteLine("Enter the interest rate for the loan");
                                    double interest = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("Enter the months for Loan");
                                    int months = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter your property's Address");
                                    string address = Console.ReadLine();
                                    Console.WriteLine("Enter the value for Property");
                                    double propertyValue = Convert.ToDouble(Console.ReadLine());


                                    HomeLoan homeLoan = new HomeLoan()
                                    {
                                        CustomerId = CustomerId,
                                        InterestRate = interest,
                                        PrincipalAmount = amount,
                                        LoanTerm = months,
                                        PropertyAddress = address,
                                        PropertyValue = propertyValue,
                                        LoanType = LoanType.HomeLoan,
                                        LoanStatus = LoanStatus.Pending
                                    };
                                    loanRepositoryImpl.ApplyLoan(homeLoan);

                                    break;

                                case 2:
                                    Console.WriteLine("Enter The Customer ID");
                                    int carCustomerId = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter the principal Amount");
                                    decimal carAmount = Convert.ToDecimal(Console.ReadLine());
                                    Console.WriteLine("Enter the interest rate for the loan");
                                    double carInterest = Convert.ToDouble(Console.ReadLine());
                                    Console.WriteLine("Enter the months for Loan");
                                    int carMonths = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter your Car's Model");
                                    string carModel = Console.ReadLine();
                                    Console.WriteLine("Enter the value for Car");
                                    double carValue = Convert.ToDouble(Console.ReadLine());


                                    CarLoan carLoan = new CarLoan()
                                    {
                                        CustomerId = carCustomerId,
                                        InterestRate = carInterest,
                                        PrincipalAmount = carAmount,
                                        LoanTerm = carMonths,
                                        CarModel = carModel,
                                        CarValue = carValue,
                                        LoanType = LoanType.CarLoan,
                                        LoanStatus = LoanStatus.Pending
                                    };
                                    loanRepositoryImpl.ApplyLoan(carLoan);
                                    break;
                                default:

                                    break;
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter your Loan Id to calculate The interest");
                            int loanId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine($"Your Interest is {loanRepositoryImpl.CalculateInterest(loanId)}");

                            break;

                        case 3:
                            Console.WriteLine("Enter your loan id to check and update the status");
                            int statusLoanId = Convert.ToInt32(Console.ReadLine());

                            loanRepositoryImpl.LoanStatus(statusLoanId);

                            break;

                        case 4:
                            Console.WriteLine("Enter the Loan ID to calculate the Emi ");
                            int emiLoanId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine($"Your EMI is {loanRepositoryImpl.CalculateEMI(emiLoanId)}");

                            break;

                        case 5:
                            Console.WriteLine("Enter your Loan id ");
                            int repayLoanId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter The amount you like to repay");
                            double repayAmount = Convert.ToDouble(Console.ReadLine());

                            loanRepositoryImpl.LoanRepayment(repayLoanId, repayAmount);

                            break;

                        case 6:
                            Console.WriteLine("Displaying all the loans");
                            List<Loan> loans = loanRepositoryImpl.GetAllLoan();

                            foreach (Loan loan in loans)
                            {
                                Console.WriteLine(loan); 
                            }

                            break;

                        case 7:
                            Console.WriteLine("Enter the loan Id to display its details");
                            int displayLoanId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine(loanRepositoryImpl.GetLoanById(displayLoanId));

                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}