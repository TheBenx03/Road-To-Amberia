using UnityEngine;
using Mono.Data.Sqlite;
using Mirror;
using System.Data;


public class Database : MonoBehaviour
{
    [Header("Clear Table")]
    public bool clearTable = false;
    public string tableName = "";

    public string DBName = "URI=file:" + Application.dataPath + "/Database/RoadDB.db";
    public static Database instance;

    private void Awake() => instance = this;

    /// <summary>
    /// Called on server from ChatAuthenticator to create the necesary tables in the database
    /// </summary>
    [ServerCallback]
    public void CreateTables(){
        using var connection = new SqliteConnection(DBName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            string sqlcreation = "";
            sqlcreation += "CREATE TABLE IF NOT EXISTS login(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,username TEXT UNIQUE NOT NULL,password TEXT NOT NULL)";
            command.CommandText = sqlcreation;
            command.ExecuteNonQuery();
        }
        using (var command = connection.CreateCommand())
        {
            string sqlcreation = "";
            sqlcreation += "CREATE TABLE IF NOT EXISTS rooms(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,roomname TEXT NOT NULL,matchid TEXT NOT NULL)";
            command.CommandText = sqlcreation;
            command.ExecuteNonQuery();
        }
        connection.Close();
    }

    [ServerCallback]
    public void LoginRegister(string username, string password){
        using var connection = new SqliteConnection(DBName);
        connection.Open();

        using (var command = connection.CreateCommand()){
            string sqlinsert = "";
            sqlinsert += $"INSERT INTO login(username, password) VALUES ({username}) ({password})";
            command.CommandText = sqlinsert;

            try {command.ExecuteNonQuery();}
            catch (SqliteException){Debug.LogWarning("LoginRegister: SqliteException");}
        }
        connection.Close();
    }

    [ServerCallback]
    public bool LoginAuth(string username, string password){
        using var connection = new SqliteConnection(DBName);
        connection.Open();
        using (var command = connection.CreateCommand()){
            string sqliread = "";
            sqliread += $"SELECT username, password FROM login WHERE username = '{username}' AND password = '{password}'";
            command.CommandText = sqliread;     

            try {IDataReader auth = command.ExecuteReader();
            if (auth != null) {
                Debug.LogWarning("LoginAuth: Not Registered");
                return false;}
            }
            catch (SqliteException){
                Debug.LogWarning("LoginAuth: SqliteException");
                return false;}
        }
        connection.Close();
        return true;
    }
}