﻿namespace Seminar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString1 = new ConnectionString
            {
                DatabaseName = "Database1",
                Host = "localhost",
                Password = "12345",
                UserName = "User1"
            };

            var connectionString2 = new ConnectionString
            {
                DatabaseName = "Database2",
                Host = "localhost",
                Password = "54321",
                UserName = "User2"
            };

            var connectionString3 = new ConnectionString
            {
                DatabaseName = "Database3",
                Host = "localhost3",
                Password = "ABCDEF",
                UserName = "User3"
            };

            List<ConnectionString> connections = new List<ConnectionString>();
            connections.Add(connectionString1);
            connections.Add(connectionString2);
            connections.Add(connectionString3);

            CacheProvider cacheProvider = new CacheProvider();
            cacheProvider.CacheConnections(connections);

            connections = cacheProvider.GetConnectionsFromCache();

            foreach (var connection in connections)
                Console.WriteLine(connection);

            Console.ReadKey(true);

        }

    }
}