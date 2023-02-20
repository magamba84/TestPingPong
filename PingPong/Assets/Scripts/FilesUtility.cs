using System.IO;
using UnityEngine;

public class FilesUtility
{
    private static string fileName = "PingPong";

    public static void SaveTextFile(string data)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName +".txt");
        SaveTextToPath(data, fullPath);
    }

    public static void SaveTextToPath(string data, string path)
    {
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            writer.WriteLine(data);
            writer.Flush();
        }

    }

    public static string LoadTextFile()
    {
        var path = Path.Combine(Application.persistentDataPath, fileName + ".txt");

        UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get("file://" + path);
        www.SendWebRequest();
        while (!www.isDone)
        {
        }

        return www.downloadHandler.text;
    }


}
