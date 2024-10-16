using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct BestTimeEntry : System.IComparable<BestTimeEntry>
{
    public string name;
    public float time;

    public int CompareTo(BestTimeEntry other)
    {
        return other.time.CompareTo(time);
    }
}

public class PlayerData
{
    public static PlayerData instance;

    protected string saveFile = "";

    public List<BestTimeEntry> bestTimes = new List<BestTimeEntry>();

    public string previousName = "Player";

    // Best Time management
    public int GetTimePlace(float time)
    {
        BestTimeEntry entry = new BestTimeEntry();
        entry.time = time;
        entry.name = "";

        int index = bestTimes.BinarySearch(entry);

        return index < 0 ? (~index) : index;
    }

    public void InsertTime(float time, string name)
    {
        BestTimeEntry entry = new BestTimeEntry();
        entry.time = time;
        entry.name = name;

        bestTimes.Insert(GetTimePlace(time), entry);

        // Keep only the 10 best times.
        while (bestTimes.Count > 10)
            bestTimes.RemoveAt(bestTimes.Count - 1);
    }

    public static void Create()
    {
        if (instance == null)
        {
            instance = new PlayerData();
        }

        instance.saveFile = Application.persistentDataPath + "/save.bin";

        if (File.Exists(instance.saveFile))
        {
            instance.Read();
        }
        else
        {
            NewSave();
        }
    }

    public static void NewSave()
    {
        instance.Save();
    }

    public void Read()
    {
        BinaryReader reader = new BinaryReader(new FileStream(saveFile, FileMode.Open));

        bestTimes.Clear();
        
        int count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
        {
            BestTimeEntry entry = new BestTimeEntry();
            entry.name = reader.ReadString();
            entry.time = reader.ReadInt32();

            bestTimes.Add(entry);
        }

        previousName = reader.ReadString();

        reader.Close();
    }

    public void Save()
    {
        BinaryWriter writer = new BinaryWriter(new FileStream(saveFile, FileMode.OpenOrCreate));

        writer.Write(bestTimes.Count);

        for (int i = 0; i < bestTimes.Count; i++)
        {
            writer.Write(bestTimes[i].name);
            writer.Write(bestTimes[i].time);
        }

        writer.Write(previousName);

        writer.Close();
    }
}
