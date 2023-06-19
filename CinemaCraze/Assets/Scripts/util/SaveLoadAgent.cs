using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadAgent : MonoBehaviour {

    public Lives lives;
    public Score score;
    public Inventory inventory;
    public CustomerManager customerManager;


    public void Save()
    {
        Debug.Log("Saving game...");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Game.save";

        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(lives, score, inventory, customerManager);

        formatter.Serialize(stream, gameData);
        stream.Close();
        Debug.Log("Game saved");
    }

    public void Load()
    {
        Debug.Log("Loading game...");
        string path = Application.persistentDataPath + "/Game.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();

            // TODO - update text and sprites
            lives.SetLives(gameData.lives);
            score.SetScore(gameData.score);
            inventory.SetAll(gameData.invNachos, gameData.invPopcorn, gameData.invSoda);

            //customerManager.customers = gameData.customers;
            customerManager.nCustomers = gameData.nCustomers;
            customerManager.nTotalCustomers = gameData.nTotalCustomers;
            customerManager.tLastSpawn = gameData.tLastSpawn;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return;
        }

        Debug.Log("Game loaded");
    }


}