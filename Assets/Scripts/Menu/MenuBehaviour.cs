using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Newtonsoft.Json;

public class MenuBehaviour : MonoBehaviour
{
    public static GameData gameData;


    const string FILEPATH = "Assets/StreamingAssets/data.json";

    // Start is called before the first frame update
    void Start()
    {
        LoadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width / 2) - 100, (Screen.height / 3) - 100, 200, 50), "Sélectionner un parcours"))
        {
            SceneManager.LoadScene("SelectParcours");
        }

        if (GUI.Button(new Rect((Screen.width / 2) - 100, ((Screen.height / 3) - 100) * 2, 200, 50), "Créer un parcours"))
        {
            SceneManager.LoadScene("CreateParcours");
        }
    }

    public void LoadJson()
    {
        if (File.Exists(FILEPATH))
        {
            gameData = JsonUtility.FromJson<GameData>(File.ReadAllText(FILEPATH));

            foreach (Parcours p in gameData.Parcours)
            {
                Debug.Log(p.ToString());
            }
        }
        else
        {
            Debug.Log("Fichier introuvable !");
        }
    }
}
