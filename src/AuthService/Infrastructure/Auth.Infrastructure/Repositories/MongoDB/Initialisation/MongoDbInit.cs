using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Auth.Infrastructure.Repositories.MongoDB.Initialisation;

public static class MongoDbInit
{
    public static void InitializeMongoDB(IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration.GetSection("MongoDBSettings")["ConnectionString"]);
        var database = mongoClient.GetDatabase("auth_database");

        var usersCollection = database.GetCollection<BsonDocument>("Users");
        if (!CollectionExists(database, "Users"))
        {
            database.CreateCollection("Users");
            if (usersCollection.CountDocuments(FilterDefinition<BsonDocument>.Empty) == 0)
            {
                var initialUserss = new List<BsonDocument>
                {
                    new()
                    {
                        { "_id", Guid.NewGuid().ToString() },
                        { "Active", true },
                        { "Email", "karim@bouneb.com" },
                        {
                            "Password",
                            "H+cwtkpByvIl5frKl3gslnFwXDkGu+nU0oJZruxrfQ3clT9mazn0WzMcWswFMv0MNcmrvtbgd0Km3RtbWO7LqA=="
                        },
                        {
                            "Salt",
                            "hLT79dNxmKY8VGsen1R5tL+94ycNTFaFn+6bObKdvNvOgkoGOfZJoYBzZn7lNtuFZbG8Wn2UO4nULD185xdapA=="
                        },
                        { "Name", "Karim" },
                        { "LastName", "Bouneb" },
                        {
                            "Claims", new BsonArray
                            {
                                new BsonDocument
                                {
                                    { "Type", "scope" },
                                    { "Value", "can_read_clients can_change_clients" }
                                }
                            }
                        },
                        { "RefreshToken", BsonNull.Value },
                        { "CreationDate", DateTime.UtcNow },
                        { "UpdateDate", DateTime.UtcNow }
                    },
                    new()
                    {
                        { "_id", Guid.NewGuid().ToString() },
                        { "Active", true },
                        { "Email", "emna@benzina.com" },
                        {
                            "Password",
                            "K/7nypt56p2+xIsWQK4eQPvIAOaYTHs3YsvsVb+22dOWrGYMVvsu08nKoGyLejopzTmROQNKPhHmPttB0zeW0w=="
                        },
                        {
                            "Salt",
                            "5y9alpf2gOIXSH5pF69mSXzXKJUh/Im/0hr7y4ipXCFumvfzPSDykS0g48ymEh/Fsf6tNW9RZX/2wZj09gbJNw=="
                        },
                        { "Name", "Emna" },
                        { "LastName", "Benzina" },
                        { "Claims", BsonNull.Value },
                        { "RefreshToken", BsonNull.Value },
                        { "CreationDate", DateTime.UtcNow },
                        { "UpdateDate", DateTime.UtcNow }
                    }
                };
                usersCollection.InsertMany(initialUserss);
            }
        }
    }

    private static bool CollectionExists(IMongoDatabase database, string collectionName)
    {
        var filter = new BsonDocument("name", collectionName);
        var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
        return collections.Any();
    }
}