using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectParcoursBehaviour : MonoBehaviour
{
    string FILEPATH = "Assets/StreamingAssets/data.json";

    GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = LoadJson();
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

    private void OnGUI()
    {
        float x = 0;
        float y = 50;
        int i = 1;

        if (GUI.Button(new Rect(x, y, Screen.width, 100), "Retour au menu"))
        {
            SceneManager.LoadScene("Menu");
        }

        y = 150;

        foreach(Parcours p in gameData.Parcours)
        {
            if (GUI.Button(new Rect(x, y, Screen.width, 100), "Parcours n." + i))
            {
                PlayerPrefs.SetInt("ParcoursIndex", i - 1);
                SceneManager.LoadScene("Play");
            }
            //x += 50;
            y += 100;
            i++;
        }
    }
}
