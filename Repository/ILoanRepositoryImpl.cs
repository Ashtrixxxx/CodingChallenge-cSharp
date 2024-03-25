using CodinChallenge_Loan.Exceptions;
using CodinChallenge_Loan.Models;
using CodinChallenge_Loan.Utilities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinChallenge_Loan.Repository
{
    public class ILoanRepositoryImpl : ILoanRepository
    {

        SqlConnection _sqlConn = null;
        SqlCommand _sqlCmd = null;
        public ILoanRepositoryImpl() {
        
            _sqlConn = new SqlConnection(DbUtils.GetConnection());
            _sqlCmd = new SqlCommand();

        }

        public  int ApplyLoan(Loan loan)
        {
            int rows = 0;
            try
            {
                _sqlCmd.CommandText = "Insert into Loan(CustomerID, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) values(@CustomerID,@principal,@interest, @LoanTerm,@LoanType,@LoanStatus)";
                _sqlCmd.Connection = _sqlConn;

                _sqlCmd.Parameters.AddWithValue("@CustomerID", loan.CustomerId);
                _sqlCmd.Parameters.AddWithValue("@principal", loan.PrincipalAmount);

                _sqlCmd.Parameters.AddWithValue("@interest", loan.InterestRate);

                _sqlCmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);

                _sqlCmd.Parameters.AddWithValue("@LoanType", loan.LoanType);

                _sqlCmd.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);

                _sqlConn.Open();
                rows = _sqlCmd.ExecuteNonQuery();

                Console.WriteLine("The Interest is " + CalculateInterest(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm));
                _sqlCmd.Parameters.Clear();



            }

            catch (Exception ex) {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rows;

        }

        public double CalculateInterest(decimal amount, double interestRate, int months)
        {
             double   interestCalculated = ((double)amount * (interestRate / 100) * months) / 12;
            return interestCalculated;

        }



        public double CalculateInterest(int loanId)
        {
            double interestCalculated = 0;

            try
            {
                _sqlCmd.CommandText = "Select * from Loan where LoanId = @id";
                _sqlCmd.Connection = _sqlConn;
                int n = 0;
                _sqlCmd.Parameters.AddWithValue("@id", loanId);
                _sqlConn.Open();
                SqlDataReader reader1 = _sqlCmd.ExecuteReader();
                while (reader1.Read())
                {
                    n = 1;
                }

                LoanException.IsLoanAvailable(n);

                _sqlCmd.Parameters.Clear();
                _sqlConn.Close();



                _sqlCmd.CommandText = "Select PrincipalAmount,InterestRate,LoanTerm from Loan where LoanId = @id";
                _sqlCmd.Connection = _sqlConn;

                _sqlCmd.Parameters.AddWithValue("@id", loanId);
                _sqlConn.Open();
                SqlDataReader reader = _sqlCmd.ExecuteReader();
                double interest_rate = 0;
                double amount = 0;
                int months = 0;
                while (reader.Read())
                {
                    amount = (double)((decimal)reader["PrincipalAmount"]);
                    interest_rate = (double)((decimal)reader["InterestRate"]);
                    months = (int)reader["LoanTerm"];
                }

                 interestCalculated = (amount * (interest_rate/100) * months) / 12;
                _sqlCmd.Parameters.Clear();

            }
            catch(LoanException l)
            {
                Console.WriteLine(l.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                _sqlConn.Close();
            }

            return interestCalculated;
        }

        public void LoanStatus(int LoanId)
        {
            int credit = 0;
            int customer_ID =0;
            int creditScore = 0;
            try
            {

                _sqlCmd.CommandText = "Select * from Loan where LoanId = @id";
                _sqlCmd.Connection = _sqlConn;
                int n = 0;
                _sqlCmd.Parameters.AddWithValue("@id", LoanId);
                _sqlConn.Open();
                SqlDataReader reader1 = _sqlCmd.ExecuteReader();
                while (reader1.Read())
                {
                    n = 1;
                }

                LoanException.IsLoanAvailable(n);

                _sqlCmd.Parameters.Clear();
                _sqlConn.Close();



                _sqlCmd.CommandText = "select CustomerId from Loan where LoanId = @LoanId";
                _sqlCmd.Connection = _sqlConn;
                _sqlCmd.Parameters.AddWithValue("@LoanId", LoanId);

                _sqlConn.Open();
                SqlDataReader reader = _sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    customer_ID = (int)reader["CustomerId"];
                }
                _sqlCmd.Parameters.Clear();
                _sqlConn.Close();

                _sqlCmd.CommandText = "select CreditScore from Customer where CustomerId = @customer_ID";
                _sqlCmd.Parameters.AddWithValue("@customer_ID", customer_ID);
                _sqlConn.Open();

                SqlDataReader sqlDataReader = _sqlCmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    creditScore = (int)sqlDataReader["CreditScore"];

                }
                _sqlConn.Close();
                _sqlCmd.Parameters.Clear();

                if(creditScore > 650) {

                    _sqlCmd.CommandText = "update Loan set LoanStatus = 1 where LoanId = @loanId and loanStatus = 0";
                    //_sqlCmd.Connection = _sqlConn;
                    _sqlCmd.Parameters.AddWithValue("@loanId", LoanId);
                    _sqlConn.Open();
                    int rows = _sqlCmd.ExecuteNonQuery();
                    Console.WriteLine("Your Loan Status Has been approved");
                    _sqlCmd.Parameters.Clear();
                    _sqlConn.Close();
                }
                else
                {
                    Console.WriteLine("Your Loan Status has not been approved yet!!");
                }



            }
            catch (LoanException l)
            {
                Console.WriteLine(l.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }

        public double CalculateEMI(int loanId)
        {
            double emi = 0;
            try
            {
                _sqlCmd.CommandText = "Select * from Loan where LoanId = @id";
                _sqlCmd.Connection = _sqlConn;
                int n = 0;
                _sqlCmd.Parameters.AddWithValue("@id", loanId);
                _sqlConn.Open();
                SqlDataReader reader1 = _sqlCmd.ExecuteReader();
                while (reader1.Read())
                {
                    n = 1;
                }

                LoanException.IsLoanAvailable(n);

                _sqlCmd.Parameters.Clear();
                _sqlConn.Close();



                _sqlCmd.CommandText = "Select PrincipalAmount,InterestRate,LoanTerm from Loan where LoanId = @id";
                _sqlCmd.Connection = _sqlConn;

                _sqlCmd.Parameters.AddWithValue("@id", loanId);
                _sqlConn.Open();
                SqlDataReader reader = _sqlCmd.ExecuteReader();
                double interest_rate = 0;
                double amount = 0;
                int months = 0;
                while (reader.Read())
                {
                    amount = (double)((decimal)reader["PrincipalAmount"]);
                    interest_rate = (double)((decimal)reader["InterestRate"]);
                    months = (int)reader["LoanTerm"];
                }
                double monthly_interestRate = (interest_rate / 12 / 100);

                double powerFactor = Math.Pow(1 + monthly_interestRate, months);

                emi = (amount * monthly_interestRate * powerFactor)/ (powerFactor-1);
                _sqlCmd.Parameters.Clear();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            finally { _sqlConn.Close(); }

            return emi;
        }


        public void LoanRepayment(int loanId, double amount)
        {
            try { 
            double monthlyEmi = CalculateEMI(loanId);
                //Console.WriteLine(monthlyEmi);

                if (amount < monthlyEmi)
                {
                    throw new Exception("The emi for the Loan is greater than the amount given");
                }
                else
                {
                    int noOfEmis = (int)( amount/monthlyEmi);

                    decimal afterRepay = noOfEmis *(decimal) amount;

                    _sqlCmd.CommandText = "update loan set PrincipalAmount = PrincipalAmount - @amount where LoanId = @loanId";
                    _sqlCmd.Connection = _sqlConn;
                    _sqlConn.Open();

                    _sqlCmd.Parameters.AddWithValue("@amount", afterRepay);
                    _sqlCmd.Parameters.AddWithValue("@loanId", loanId);
                    int rows = _sqlCmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine($"You  have paid EMI's upto {noOfEmis} months");

                    _sqlCmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _sqlConn.Close();
            }

        }

        public List<Loan> GetAllLoan()
        {
            List<Loan> loans = new List<Loan>();

            try
            {
                
                _sqlCmd.CommandText = "select * from Loan";
                _sqlCmd.Connection = _sqlConn;
                _sqlConn.Open();
                SqlDataReader reader = _sqlCmd.ExecuteReader();
                while (reader.Read()) {
                    Loan loan = new Loan();
                    loan.CustomerId = (int)reader["CustomerID"];
                    loan.LoanId = (int)reader["LoanId"];
                    loan.InterestRate = (double)((decimal)reader["InterestRate"]);
                    loan.LoanTerm = (int)(reader["loanTerm"]);
                    loan.LoanType = (LoanType)Enum.Parse(typeof(LoanType), (string)reader["LoanType"]);
                    loan.LoanStatus = (LoanStatus)reader["LoanStatus"];
                    loan.PrincipalAmount = ((decimal)reader["PrincipalAmount"]);

                    loans.Add(loan);
                
                }
                _sqlCmd.Parameters.Clear();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { _sqlConn.Close(); }
            
            return loans;

        }


        public Loan GetLoanById(int loanId)
        {
            Loan loan = new Loan();

            try
            {
                _sqlCmd.CommandText = "select * from Loan where LoanId = @loanId";
                _sqlCmd.Parameters.AddWithValue("@loanId", loanId);
                _sqlCmd.Connection = _sqlConn;
                _sqlConn.Open();
                SqlDataReader reader = _sqlCmd.ExecuteReader();
                while (reader.Read())
                {

                    loan.CustomerId = (int)reader["CustomerID"];
                    loan.LoanId = (int)reader["LoanId"];
                    loan.InterestRate = (double)((decimal)reader["InterestRate"]);
                    loan.LoanTerm = (int)(reader["loanTerm"]);

                    loan.LoanType = (LoanType)Enum.Parse(typeof(LoanType), (string)reader["LoanType"]);
                    loan.LoanStatus = (LoanStatus)reader["LoanStatus"];
                    loan.PrincipalAmount = ((decimal)reader["PrincipalAmount"]);
                    

                }

                _sqlCmd.Parameters.Clear();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { _sqlConn.Close(); }

            return loan;
        }

    }
}
