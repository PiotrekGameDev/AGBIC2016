using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    [System.Serializable]
    public class Data
    {
        public int unlockedLevels=1;
        public int coins = 0;
    }
    public static Data data=new Data();
    private const string path = "/save.txt";

    static public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + path); //you can call it anything you want
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Saved Save: " + Application.dataPath + path);
    }

    static public void Load()
    {
        if (File.Exists(Application.dataPath + path)&&new FileInfo(Application.dataPath + path).Length > 0)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + path, FileMode.Open);
            data = (Data)bf.Deserialize(file);
            file.Close();
            Debug.Log("Loaded Save: " + Application.dataPath + path);
        }
    }
}

/*
[ExecuteInEditMode]
public class SaveSystem : MonoBehaviour {

    [System.Serializable]
    public class Game
    {
        public string name;
        public int value;
    }
    public Game game;
    public bool save=false;
    public bool load=false;

    public static SaveSystem Instance { get; private set; }

    void Awake()
    {
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }

        // Here we save our singleton instance
        Instance = this;

        // Furthermore we make sure that we don't destroy between scenes (this is optional)
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (save)
        {
            Save(game);
            save = false;
        }
        if (load)
        {
            Load(game.name);
            load = false;
        }
    }

    public void Save(Game saveGame)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + saveGame.name + ".sav"); //you can call it anything you want
        bf.Serialize(file, saveGame);
        file.Close();
        Debug.Log("Saved Game: " + Application.dataPath + saveGame.name + ".sav");
    }

    public void Load(string gameToLoad)
    {
        if (File.Exists(Application.dataPath + gameToLoad + ".sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + gameToLoad + ".sav", FileMode.Open);
            game = (Game)bf.Deserialize(file);
            file.Close();
            Debug.Log("Loaded Game: " + Application.dataPath + gameToLoad + ".sav");
        }
    }
}
*/
