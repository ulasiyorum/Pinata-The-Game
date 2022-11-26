using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour
{
    [SerializeField] static Player playerSaved;
    [SerializeField] Player player;

    private void Start()
    {
        this.enabled = false;        
    }
    public void SaveGame()
    {
        playerSaved = player;
        if (File.Exists(Application.persistentDataPath + "/savefile.sf")) 
        {
            FileStream fs = File.OpenWrite(Application.persistentDataPath + "/savefile.sf");
            BinaryFormatter formatter = new BinaryFormatter();
            fs.Flush();
            formatter.Serialize(fs, playerSaved);
            fs.Close();
        }
        else
        {
            FileStream fs = File.Create(Application.persistentDataPath + "/savefile.sf");
            BinaryFormatter formatter = new BinaryFormatter();
            fs.Flush();
            formatter.Serialize(fs, playerSaved);
            fs.Close();
        }
    }   
    public Player LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/savefile.sf"))
        {
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/savefile.sf");
            fs.Flush();
            BinaryFormatter formatter = new BinaryFormatter();
            player = (Player)formatter.Deserialize(fs);
            fs.Close();
        }
        else { SaveGame();

            FileStream fs = File.OpenRead(Application.persistentDataPath + "/savefile.sf");
            fs.Flush();
            BinaryFormatter formatter = new BinaryFormatter();
            player = (Player)formatter.Deserialize(fs);
            fs.Close();
        }
        
        
        return player;
    }
}
