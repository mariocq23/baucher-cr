using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MySql.Data.MySqlClient;

public class DatabaseExamples
{
    public static void Main(string[] args)
    {
        MySqlExample();
        MongoDbExample();
    }

    public static void MySqlExample()
    {
        string connectionString = "server=localhost;user=your_user;password=your_password;database=your_database;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Create a table (if it doesn't exist)
                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Name VARCHAR(255),
                        Email VARCHAR(255)
                    );";
                using (var command = new MySqlCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert data
                string insertSql = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email);";
                using (var command = new MySqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Name", "John Doe");
                    command.Parameters.AddWithValue("@Email", "john.doe@example.com");
                    command.ExecuteNonQuery();
                }

                // Select data
                string selectSql = "SELECT * FROM Users;";
                using (var command = new MySqlCommand(selectSql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id: {reader.GetInt32(0)}, Name: {reader.GetString(1)}, Email: {reader.GetString(2)}");
                        }
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
        }
    }

    public static void MongoDbExample()
    {
        string connectionString = "mongodb://localhost:27017";
        string databaseName = "mydatabase";
        string collectionName = "users";

        try
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            // Insert a document
            var document = new BsonDocument
            {
                { "Name", "Jane Doe" },
                { "Email", "jane.doe@example.com" }
            };
            collection.InsertOne(document);

            // Find documents
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documents = collection.Find(filter).ToList();

            foreach (var doc in documents)
            {
                Console.WriteLine($"Name: {doc["Name"]}, Email: {doc["Email"]}");
            }

            // Find one document
            var filterOne = Builders<BsonDocument>.Filter.Eq("Name", "Jane Doe");
            var documentOne = collection.Find(filterOne).FirstOrDefault();

            if (documentOne != null) {
                Console.WriteLine($"Found one document: Name: {documentOne["Name"]}, Email: {documentOne["Email"]}");
            }

        }
        catch (MongoException ex)
        {
            Console.WriteLine($"MongoDB Error: {ex.Message}");
        }
    }
}