using UnityEngine;
using SQLite;
using System.IO;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }
    
    private string dbPath;
    private SQLiteConnection dbConnection;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        SetDatabasePath();
        InitializeDatabase();
    }

   public List<HighScore> GetTopHighScores(int count)
{
    List<HighScore> topScores = dbConnection.Table<HighScore>()
        .OrderByDescending(score => score.Wave)
        .Take(count)
        .ToList();
    
    return topScores;
}
    
    void SetDatabasePath()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "gamedata.db");
    }
    
    void InitializeDatabase()
    {
        dbConnection = new SQLiteConnection(dbPath);
        CreateHighScoresTable();
    }
    
    void CreateHighScoresTable()
    {
       dbConnection.CreateTable<HighScore>();
       Debug.Log("High Scores table created at: " + dbPath);
    }

    public void SaveHighScore(string Type, int Wave)
    {
        HighScore newScore = new HighScore
        {
            Type = Type,
            Wave = Wave
        };
        
        dbConnection.Insert(newScore);
        Debug.Log("High score saved: " + Type + " - " + Wave);
    }
}

public class HighScore
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    public string Type { get; set; }
    public int Wave { get; set; }
}