using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace com.imie.geocaching
{
    public class CameraBehaviour_CreateParcours : MonoBehaviour
    {
        public GameData gameData;
        public string FILEPATH = "Assets/StreamingAssets/data.json";

        public List<GameObject> lesGOs;
        public GameObject closest;

        public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityX = 5F;
        public float sensitivityY = 5F;

        public float minimumX = -360F;
        public float maximumX = 360F;

        public float minimumY = -60F;
        public float maximumY = 60F;

        float rotationY = 0F;

        float speed = 3;

        private const float MIN_DIST = 3f;
        private const float DEFAULT_Y = 3f;

        // Start is called before the first frame update
        void Start()
        {
            gameData = LoadJson();
        }

        // Update is called once per frame
        void Update()
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }

            // MOVEMENTS
            if (Input.GetKey("d"))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey("q"))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey("z"))
            {
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }
            if (Input.GetKey("s"))
            {
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 6;
            }
            else
            {
                speed = 3;
            }


            // AFFICHAGE GAMEOBJECTS
            if (Input.GetKey(KeyCode.O))
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(this.transform.position.x, DEFAULT_Y, this.transform.position.z);
                lesGOs.Add(cube);
            }
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(20, 40, 400, 200), "Créer un objet (O)"))
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(this.transform.position.x, DEFAULT_Y, this.transform.position.z);
                lesGOs.Add(cube);
            }

            if (GUI.Button(new Rect(420, 40, 400, 200), "Sauvegarder le parcours"))
            {
                if (lesGOs.Count == 0)
                {
                    Debug.Log("Vous ne pouvez pas enregistrer un parcours sans étapes !");
                }
                else
                {
                    SaveParcours();
                }
            }

            if (GUI.Button(new Rect(820, 40, 400, 200), "Retour au menu"))
            {
                SceneManager.LoadScene("Menu");
            }
        }

        public void SaveParcours()
        {
            Parcours p = new Parcours();
            p.JouetObjets = new List<JouetObjet>();

            foreach (GameObject go in lesGOs)
            {
                p.JouetObjets.Add(JouetObjet.toJouetObjet(go));
            }

            gameData.Parcours.Add(p);

            SaveJson();
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

        public void SaveJson()
        {
            string dataAsJson = JsonUtility.ToJson(gameData);

            File.WriteAllText(FILEPATH, dataAsJson);

            SceneManager.LoadScene("Menu");
        }
    }

    
}