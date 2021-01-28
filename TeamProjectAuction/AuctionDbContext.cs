using System;
using System.Data.Entity;
using System.IO;

namespace TeamProjectAuction
{
    public class AuctionDbContext : DbContext
    {
        const string DbName = "AuctionDatabaseTest1.mdf";
        static string DbPath = Path.Combine(Environment.CurrentDirectory, DbName);

        public AuctionDbContext() : base(
            $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DbPath};Integrated Security=True;Connect Timeout=30")
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAddress> ClientsAddresses { get; set; }
        public DbSet<ClientContact> ClientsContacts { get; set; }
        public DbSet<ClientPayment> ClientsPayments { get; set; }
        // public DbSet<ClientPayment> ClientsPayments { get; set; }   // not sure
        public DbSet<ProductOwner> ProductOwner { get; set; }     // not sure
    }
}