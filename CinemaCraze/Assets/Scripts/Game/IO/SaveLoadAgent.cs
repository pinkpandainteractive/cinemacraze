using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoadAgent : MonoBehaviour
{

    public AudioClip buttonsound;
    public AudioSource source;
    public Lives lives;
    public Score score;
    public Inventory inventory;
    public CustomerManager customerManager;
    public MachineManager machineManager;

    public Product popcorn;
    public Product nacho;
    public Product soda;
    public Upgrades popcornUpgrades;
    public Upgrades nachoUpgrades;
    public Upgrades sodaUpgrades;
    public ProductManager productManager;
    public UpgradesManager upgradesManager;
public void Save()
{
    source.PlayOneShot(buttonsound, 1f);

    Debug.Log("Saving game...");

    string path = Application.persistentDataPath + "/Game.save";
    
    try
    {
        // Überprüfen Sie, ob die Datei verwendet wird und versuchen Sie es später erneut
        if (IsFileLocked(new FileInfo(path)))
        {
            Debug.LogError("File is currently in use, unable to save.");
            return;
        }

        // BinaryFormatter and FileStream are placed in using statements to ensure they are disposed properly
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            /* Debug.Log(popcorn.GetProductData().refillLevel); */
            GameData gameData = new GameData(lives, score, inventory, customerManager, machineManager, popcorn, nacho, soda, popcornUpgrades, nachoUpgrades, sodaUpgrades);
            formatter.Serialize(stream, gameData);
        }
        
        PlayerPrefs.SetString("SavePath", path);
        Debug.Log("Game saved");
    }
    catch (Exception ex)
    {
        Debug.LogError("Failed to save game: " + ex.Message);
    }
}

// Methode zum Überprüfen, ob die Datei gesperrt ist
private bool IsFileLocked(FileInfo file)
{
    FileStream stream = null;

    try
    {
        stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
    }
    catch (IOException)
    {
        // Datei ist gesperrt
        return true;
    }
    finally
    {
        stream?.Close();
    }

    // Datei ist nicht gesperrt
    return false;
}

    public void Load()
    {
        Debug.Log("Loading game...");
        string path = Application.persistentDataPath + "/Game.save";

        if (File.Exists(path))
        {
            try{
                using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            

            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();
        
            // TODO - update text and sprites
            lives.SetLives(gameData.lives);
            score.SetScore(gameData.score);
            inventory.SetAll(gameData.invNachos, gameData.invPopcorn, gameData.invSoda);

            customerManager.LoadCustomers(gameData.customers);
            customerManager.currentCustomersCount = gameData.customerCount;
            customerManager.totalCustomersCount = gameData.totalCustomerCount;
            customerManager.lastSpawnTime = gameData.timeOfLastSpawn;

            machineManager.popcornMachineUnlocked = gameData.popcornUnlocked;
            machineManager.sodaMachineUnlocked = gameData.sodaUnlocked;
            machineManager.nachosMachineUnlocked = gameData.nachosUnlocked;

            machineManager.LoadMachines();
            upgradesManager.LoadUpgrades(gameData);
            productManager.LoadProducts(gameData, "Popcorn");
            productManager.LoadProducts(gameData, "Nachos");
            productManager.LoadProducts(gameData, "Soda");
            Debug.Log("CapacityLevel 1: "+gameData.products[0].maxCapacityLevel);
            /* productManager.LoadProducts(gameData, "Popcorn");
            productManager.LoadProducts(gameData, "Nachos");
            productManager.LoadProducts(gameData, "Soda"); */
        }
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to load game: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return;
        }

       
    }


}