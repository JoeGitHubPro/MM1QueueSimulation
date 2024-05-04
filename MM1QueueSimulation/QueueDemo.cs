using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections; // needed for Queue 

namespace MM1QueueSimulation
{

    public class BankCustomer
    {

        private string customerName;
        private string customerActivity;
        private int customerNumber;
        private int customerAmount;

        public BankCustomer()
        { }
        public BankCustomer(string name, string bankingActivity, int accountNumber, int amount)
        {
            customerName = name;
            customerActivity = bankingActivity;
            customerNumber = accountNumber;
            customerAmount = amount;
        }
        public string name
        {
            get { return customerName; }
            set { customerName = value; }
        }
        public string bankingActivity
        {
            get { return customerActivity; }
            set { customerActivity = value; }
        }
        public int accountNumber
        {
            get { return customerNumber; }
            set { customerNumber = value; }
        }
        public int amount
        {
            get { return customerAmount; }
            set { customerAmount = value; }
        }
    }

    public class Bank
    {
        public Queue TellerLine = new Queue();
        public int AmountOnDeposit = 10000;
        public enum BankingActivity
        { deposit, withdrawl, transferFunds };


        public void QueueContentsCopy(Queue localQueue)
        {
            BankCustomer tempCustomer = new BankCustomer();
            Queue copyoflocalQueue = new Queue();
            // make the copy 
            copyoflocalQueue = (Queue)localQueue.Clone();
            Console.WriteLine(" ");
            Console.WriteLine("View the queue using a copy");
            do
            {
                tempCustomer = (BankCustomer)copyoflocalQueue.Dequeue();
                Console.WriteLine("Name: " + tempCustomer.name + ",  Activity: " + tempCustomer.bankingActivity + ",  Account no: " + tempCustomer.accountNumber.ToString() + ", Amount $" + tempCustomer.amount.ToString());
            } while (copyoflocalQueue.Count != 0);
        }
        public void QueueContentsEnum(Queue localQueue)
        {
            BankCustomer tempCustomer = new BankCustomer();
            // get the built in enumerator
            System.Collections.IEnumerator en = localQueue.GetEnumerator();
            Console.WriteLine(" ");
            Console.WriteLine("View the queue using an enumerator");
            while (en.MoveNext())
            {
                tempCustomer = (BankCustomer)en.Current;
                Console.WriteLine("Name: " + tempCustomer.name + ",  Activity: " + tempCustomer.bankingActivity + ",  Account no: " + tempCustomer.accountNumber.ToString() + ", Amount $" + tempCustomer.amount.ToString());

            }
        }
        public void QueuePeek(Queue localQeue)
        {
            BankCustomer tempCustomer = new BankCustomer();
            tempCustomer = (BankCustomer)localQeue.Peek();
            Console.WriteLine("The next Customer in line: " + tempCustomer.name);
        }

        public void ProcessCustomerRequest(BankCustomer customer)
        {
            Console.WriteLine("Customer: " + customer.name);
            Console.WriteLine("Activity: " + customer.bankingActivity);
            if ((customer.bankingActivity == "deposit") | customer.bankingActivity == "transferFunds")
            {
                AmountOnDeposit += customer.amount;
                Console.WriteLine("Amount on Deposit: " + AmountOnDeposit);
            }
            if (customer.bankingActivity == "withdrawl" && (customer.name != "John Dillinger"))
            {
                AmountOnDeposit -= customer.amount;
                Console.WriteLine("Amount on Deposit: " + AmountOnDeposit);
            }
            if ((customer.bankingActivity == "withdrawl") && (customer.name == "John Dillinger"))
            {
                AmountOnDeposit = 0;
                Console.WriteLine("Big Bank Robery !!!    Amount on Deposit: " + AmountOnDeposit);
            }
            Console.WriteLine("------------------------------------------------");
        }
    }
}
