using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class ActualSaveSystem
{
    static BinaryFormatter bf = new BinaryFormatter();
    static string saveFile = Application.persistentDataPath + "/0001.bin";


    public static void Save(AttackingPinata player)
    {
    FileStream fs = new FileStream(saveFile,
                                   FileMode.OpenOrCreate,
                                   FileAccess.ReadWrite,
                                   FileShare.ReadWrite);
        ActualPlayerData data = new ActualPlayerData(player);
        if (File.Exists(saveFile))
        {

            bf.Serialize(fs, data);
            fs.Close();
        }
        else
        {
            fs = new FileStream(saveFile,FileMode.Create);
            bf.Serialize(fs, data);
            fs.Close();
        }
    }
    public static ActualPlayerData Load()
    {
        FileStream fs = new FileStream(saveFile,
                                   FileMode.OpenOrCreate,
                                   FileAccess.ReadWrite,
                                   FileShare.ReadWrite);
        if (File.Exists(saveFile))
        {
            ActualPlayerData data = bf.Deserialize(fs) as ActualPlayerData;
            fs.Close();
            return data;
        } else
        {
            return null;
        }
    }

    public static bool FileExists()
    {
        return File.Exists(saveFile);
    }
}
