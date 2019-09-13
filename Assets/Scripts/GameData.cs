using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public List<Parcours> Parcours;

    [System.NonSerialized]
    public static string filepath;

    public GameData()
    {
        this.Parcours = new List<Parcours>();
    }

    public static GameData LoadJson()
    {
        if (filepath == null)
        {
            filepath = Application.dataPath + "/Data/data.json";
        }

        if (!File.Exists(filepath))
        {
            var fs = new FileStream(filepath, FileMode.Create);
            fs.Dispose();
        }

        return JsonUtility.FromJson<GameData>(File.ReadAllText(filepath));
    }

    public static void SaveJson(GameData gd)
    {
        string dataAsJson = JsonUtility.ToJson(gd);

        File.WriteAllText(filepath, dataAsJson);
    }
}

[System.Serializable]
public class Parcours
{
    public List<JouetObjet> JouetObjets;

    public Parcours(){
        this.JouetObjets = new List<JouetObjet>();
    }

    public override string ToString()
    {
        return JouetObjets.ToString();
    }
}

[System.Serializable]
public class JouetObjet
{
    public float X;
    public float Z;
    public string Name;

    public static JouetObjet toJouetObjet(GameObject g)
    {
        return new JouetObjet
        {
            X = g.transform.position.x,
            Z = g.transform.position.z,
            Name = g.name
        };
    }
    

    public override string ToString()
    {
        return Name;
    }
}


