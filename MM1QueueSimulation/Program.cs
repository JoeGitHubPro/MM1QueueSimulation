using System;
using System.Collections.Generic;
using System.Text;

namespace MM1QueueSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a Bank
            Bank ThomastonBankandTrust = new Bank();
            // create a customer
            BankCustomer JP = new BankCustomer("J P Morgan", Bank.BankingActivity.deposit.ToString(), 335445, 30000);
            // add the customer to the TellerLine
            ThomastonBankandTrust.TellerLine.Enqueue(JP);
            BankCustomer Butch = new BankCustomer("Butch Cassidy", Bank.BankingActivity.transferFunds.ToString(), 555445, 3500);
            BankCustomer Sundance = new BankCustomer("Sundance Kid", Bank.BankingActivity.withdrawl.ToString(), 555444, 3500);
            BankCustomer John = new BankCustomer("John Dillinger", Bank.BankingActivity.withdrawl.ToString(), 12345, 2000);
            ThomastonBankandTrust.TellerLine.Enqueue(Sundance);
            ThomastonBankandTrust.TellerLine.Enqueue(Butch);
            ThomastonBankandTrust.TellerLine.Enqueue(John);

            Console.WriteLine("Peek: " + ThomastonBankandTrust.TellerLine.Peek());

            // Pause
            Console.ReadLine();
            ThomastonBankandTrust.QueuePeek(ThomastonBankandTrust.TellerLine);
            // View the queue using a copy 
            ThomastonBankandTrust.QueueContentsCopy(ThomastonBankandTrust.TellerLine);
            // view the queue through enumeration 
            ThomastonBankandTrust.QueueContentsEnum(ThomastonBankandTrust.TellerLine);

            // Pause
            Console.ReadLine();
            // verify that the origional queue is not modified.
            Console.WriteLine("Count of items in queue after copy & enum :" + ThomastonBankandTrust.TellerLine.Count.ToString());
            do
            {
                BankCustomer nextInLine = new BankCustomer();
                nextInLine = (BankCustomer)ThomastonBankandTrust.TellerLine.Dequeue();
                ThomastonBankandTrust.ProcessCustomerRequest(nextInLine);
                // Pause
                Console.ReadLine();
            } while (ThomastonBankandTrust.TellerLine.Count != 0);

            // Pause
            Console.ReadLine();

        }
    }
}