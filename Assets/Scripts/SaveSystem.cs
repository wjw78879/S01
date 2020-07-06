using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(int gem, int level, float posX, float posY, float posZ, int prog)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savdat.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gem, level, posX, posY, posZ, prog);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Save data to " + path);
    }

    public static SaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savdat.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
