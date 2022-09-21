using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TemporarySave {
    static BinaryFormatter bf = new BinaryFormatter();
    static string saveFile = Application.persistentDataPath + "/temporary.bin";


    public static void Save(AttackingPinata player)
    {
        FileStream fs = new FileStream(saveFile,
                                       FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.ReadWrite);
        TActualPlayerData data = new TActualPlayerData(player);
        if (File.Exists(saveFile))
        {

            bf.Serialize(fs, data);
            fs.Close();
        }
        else
        {
            fs = new FileStream(saveFile, FileMode.Create);
            bf.Serialize(fs, data);
            fs.Close();
        }
    }
    public static TActualPlayerData Load()
    {
        FileStream fs = new FileStream(saveFile,
                                   FileMode.OpenOrCreate,
                                   FileAccess.ReadWrite,
                                   FileShare.ReadWrite);
        if (File.Exists(saveFile))
        {
            TActualPlayerData data = bf.Deserialize(fs) as TActualPlayerData;
            fs.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static bool FileExists()
    {
        return File.Exists(saveFile);
    }
}
