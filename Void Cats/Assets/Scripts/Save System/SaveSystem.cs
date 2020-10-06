using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public static class SaveSystem 
{
    public static void saveJournal (JournalDataStorage journal)
    {
        Debug.Log("Starting to save journal"); 
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GAMESAVE.txt";
        Debug.Log("Created file at " + path);

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveJournalData data = new SaveJournalData(journal);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save file stream closed");
    }

    public static SaveJournalData loadJournal()
    {
        Debug.Log("starting to load file");
        string path = Application.persistentDataPath + "/GAMESAVE.txt";

        if(File.Exists(path))
        {
            Debug.Log("File found at " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveJournalData data = formatter.Deserialize(stream) as SaveJournalData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Journal Save not found at " + path);
            return null;
        }
    }
}
