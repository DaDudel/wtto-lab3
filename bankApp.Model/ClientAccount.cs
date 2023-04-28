namespace bankApp
{
    public class ClientAccount
    {
        public ClientAccount(int bid) 
        { 
            this.Bid = bid;
        }
        public ClientAccount() { }

        public string Name { get; set; }

        public string Password { get; set; }

        public long Bid { get; set; }

        public long MoneyToPay { get; set; }

        public string AccountNumber { get; set; }

        public List<long> Operations = new List<long>();

        public BankSystem Bank { get; set; }

        public void AddFounds(long money)
        {
            Operations.Add(money);
        }

        public IEnumerable<long> ShowOperations()
        {
            return Operations;
        }

        public void TakeLoan(long loan)
        {
            Operations.Add(loan);
            MoneyToPay += (long) (loan * 1.2);
        }

        public bool SendTransfer(long amount, string accountNumber)
        {
            if(accountNumber.Count() != 26)
            {
                return false;
            }

            //some quirky validation
            if (!accountNumber[2].Equals(accountNumber[6]) ||
                !accountNumber[3].Equals(accountNumber[11]) ||
                !accountNumber[4].Equals(accountNumber[16]) ||
                !accountNumber[5].Equals(accountNumber[21]))
            {
                return false;
            }

            for (int i = 0; i < Bank.ClientList.Count(); i++)
            {
                if (Bank.ClientList[i].AccountNumber == accountNumber)
                {
                    Bank.ClientList[i].Operations.Add(amount);
                    Operations.Add(-amount);
                    return true;
                }
            }

            return false;
        }
    }
}
