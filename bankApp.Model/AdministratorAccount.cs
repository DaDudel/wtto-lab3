using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankApp
{
    public class AdministratorAccount
    {
        public AdministratorAccount(BankSystem bank)
        {
            this.Bank = bank;
        }

        public BankSystem Bank { get; set; }

        public void AddClient (ClientAccount client)
        {
            client.Bank = Bank;
            Bank.ClientList.Add(client);
        }

        public void RemoveClient (long clientBid)
        {
            ClientAccount clientToRemove = new ClientAccount();

            foreach(var client in Bank.ClientList) 
            {
                if(client.Bid== clientBid) 
                {
                    clientToRemove = client;
                    break;
                }
            }

            clientToRemove.Bank = null;
            Bank.ClientList.Remove(clientToRemove);
        }

        public void EditClient (long clientBid, ClientAccount editedClient)
        {
            ClientAccount user = new ClientAccount();

            for (int i = 0; i < Bank.ClientList.Count; i++)
            {
                if (Bank.ClientList[i].Bid == clientBid) 
                {
                    Bank.ClientList[i] = editedClient;
                    break;
                }
            }
        }

        public IEnumerable<long> GetOperations(long clientBid)
        {
            foreach(var client in Bank.ClientList) 
            {
                if (client.Bid == clientBid)
                {
                    return client.Operations;
                }
            }

            return null;
        }
    }
}
