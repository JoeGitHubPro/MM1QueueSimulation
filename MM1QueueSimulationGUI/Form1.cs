using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MM1QueueSimulationGUI
{
    public partial class Form1 : Form
    {
        private Bank ThomastonBankandTrust;
        public Form1()
        {
            InitializeComponent();
            cbxBankingActivity.SelectedIndex = 0;
            ThomastonBankandTrust = new Bank();


            BankCustomer JP = new BankCustomer("J P Morgan", Bank.BankingActivity.deposit.ToString(), 335445, 30000);
            // add the customer to the TellerLine
            ThomastonBankandTrust.TellerLine.Enqueue(JP);
            BankCustomer Butch = new BankCustomer("Butch Cassidy", Bank.BankingActivity.transferFunds.ToString(), 555445, 3500);
            BankCustomer Sundance = new BankCustomer("Sundance Kid", Bank.BankingActivity.withdrawl.ToString(), 555444, 3500);
            BankCustomer John = new BankCustomer("John Dillinger", Bank.BankingActivity.withdrawl.ToString(), 12345, 2000);
            ThomastonBankandTrust.TellerLine.Enqueue(Sundance);
            ThomastonBankandTrust.TellerLine.Enqueue(Butch);
            ThomastonBankandTrust.TellerLine.Enqueue(John);
            Process();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string bankingActivity = cbxBankingActivity.SelectedItem?.ToString(); // Assuming numericUpDown represents BankingActivity
            int accountNumber = int.Parse(txtAccountNumber.Text);
            int amount = int.Parse(txtAmount.Text);

            if (Enum.TryParse<Bank.BankingActivity>(bankingActivity, out Bank.BankingActivity activityEnum))
            {
                BankCustomer customer = new BankCustomer(name, activityEnum.ToString(), accountNumber, amount);
                ThomastonBankandTrust.TellerLine.Enqueue(customer);

                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Invalid banking activity selected.");
            }

            Process();

        }

        private void btnProcessCustomers_Click(object sender, EventArgs e)
        {
            Process();

            // Show the count of items in the original queue
            MessageBox.Show("Count of items in the original queue after processing: " + ThomastonBankandTrust.TellerLine.Count.ToString());
        }

        private void Process()
        {
            tvBankCustomer.Nodes.Clear();

            // Create a copy of ThomastonBankandTrust without modifying the original queue
            Bank copiedBank = new Bank();
            //  copiedBank.TellerLine = ThomastonBankandTrust.TellerLine;

            foreach (var customer in ThomastonBankandTrust.TellerLine)
            {
                copiedBank.TellerLine.Enqueue(customer);
            }

            // Declare a list to store processed customers
            List<BankCustomer> processedCustomers = new List<BankCustomer>();

            while (copiedBank.TellerLine.Count > 0)
            {
                // Peek at the next customer without removing them from the copied queue
                BankCustomer nextInLine = (BankCustomer)copiedBank.TellerLine.Peek();

                // Process the customer (assuming ProcessCustomerRequest does not modify the queue)
                copiedBank.ProcessCustomerRequest(nextInLine);

                // Add the customer to the processed list
                processedCustomers.Add(nextInLine);

                // Dequeue the customer to move to the next one
                copiedBank.TellerLine.Dequeue();
            }

            // Display the processed customers in the TreeView
            foreach (var customer in processedCustomers)
            {
                TreeNode customerNode = new TreeNode(customer.name);
                customerNode.Nodes.Add($"Banking Activity: {customer.bankingActivity}");
                customerNode.Nodes.Add($"Account Number: {customer.accountNumber}");
                customerNode.Nodes.Add($"Amount: {customer.amount}");

                tvBankCustomer.Nodes.Add(customerNode);
            }
        }



        private void btnQueuePeek_Click(object sender, EventArgs e)
        {
            if (ThomastonBankandTrust.TellerLine.Count > 0)
            {
                BankCustomer peekedCustomer = (BankCustomer)ThomastonBankandTrust.TellerLine.Peek();
                peekedCustomer = (BankCustomer)ThomastonBankandTrust.TellerLine.Dequeue();
                MessageBox.Show($"Name: {peekedCustomer.name}, Activity: {peekedCustomer.bankingActivity}, Account Number: {peekedCustomer.accountNumber}, Amount: {peekedCustomer.amount}");
            }
            else
            {
                MessageBox.Show("Queue is empty.", "Empty Queue", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Process();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
