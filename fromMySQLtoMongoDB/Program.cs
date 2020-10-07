using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using static System.Console;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

namespace fromMySQLtoMongoDB
{
    class Program
    {
        static void Main()
        {
            string serverName = "mysql60.hostland.ru";
            string userName = "host1323541_itstep";
            string dbName = "host1323541_itstep27";
            string port = "3306";
            string password = "269f43dc";
            string connStr = "server=" + serverName + ";user=" + userName + ";database=" + dbName + ";port=" + port + ";password=" + password + ";";

            MySqlConnection db = new MySqlConnection(connStr);
            List<DBmodel> students = new List<DBmodel>();

            try
            {
                db.Open();
                WriteLine("соединение успешно установлено");

                string sqlExpression = "SELECT id, name, is_study, birthday FROM table_student";
                MySqlCommand command = new MySqlCommand(sqlExpression, db);
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DBmodel student = new DBmodel();
                        student.id = reader.GetInt32("id");
                        student.name = reader.GetString("name");
                        student.is_study = reader.GetBoolean("is_study");
                        student.birthday = reader.GetDateTime("birthday");
                        students.Add(student);
                    }
                }

                db.Close();
            }
            catch(InvalidOperationException e)
            {
                WriteLine(e);
            }

            string connStrMongo = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connStrMongo);

            IMongoDatabase dbMongo = client.GetDatabase("student");
            var collection = dbMongo.GetCollection<BsonDocument>("std");
            BsonDocument doc = new BsonDocument();

            foreach (var std in students)
            {
                doc = std.ToBsonDocument();
                collection.InsertOne(doc);
            }
            


            
        }
    }
}
