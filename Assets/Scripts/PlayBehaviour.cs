using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayBehaviour : MonoBehaviour
{
    GameData gameData;

    string FILEPATH = "Assets/StreamingAssets/data.json";

    // Start is called before the first frame update
    void Start()
    {
        gameData = LoadJson();

        int parkIndex = PlayerPrefs.GetInt("ParcoursIndex");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameData LoadJson()
    {
        if (File.Exists(FILEPATH))
        {
            return JsonUtility.FromJson<GameData>(File.ReadAllText(FILEPATH));
        }
        else
        {
            Debug.Log("Fichier introuvable !");
            return null;
        }
    }
}
