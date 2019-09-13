using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

namespace com.imie.geocaching
{
    public class CameraBehaviour_Play : MonoBehaviour
    {
        public GameData gameData;
        public string FILEPATH;

        public List<GameObject> lesGOs;
        public int goIndex = 0;

        public ArrowBehaviour arrow;
        /*
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
        */
        private const float MIN_DIST = 5f;
        private const float DEFAULT_Y = 3f;

        // Start is called before the first frame update
        void Start()
        {
            FILEPATH = GameData.filepath;
            gameData = GameData.LoadJson();

            List<JouetObjet> lesJOs = gameData.Parcours[PlayerPrefs.GetInt("ParcoursIndex")].JouetObjets;
            foreach(JouetObjet jo in lesJOs)
            {
                lesGOs.Add(ToGameObject(jo));
            }

            arrow.target = lesGOs[0];

            //Cursor.lockState = CursorLockMode.Locked;

            //foreach (GameObject go in lesGOs)
            //{
            //    go.SetActive(false);
            //}
        }

        // Update is called once per frame
        void Update()
        {
            /*
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
            */

            // AFFICHAGE GAMEOBJECTS
            if (Approximately(lesGOs[goIndex].transform.position, MIN_DIST))
            {
                //goToFind.SetActive(true);
                lesGOs[goIndex].GetComponent<MeshRenderer>().material.color = Color.green;

                goIndex++;

                if (goIndex == lesGOs.Count)
                {
                    arrow.target = null;


                    //SceneManager.LoadScene("Menu");
                }

                arrow.target = lesGOs[goIndex];
            }
        }


        private void OnGUI()
        {
            if (GUI.Button(new Rect(20, 40, 400, 200), "Retour au menu"))
            {
                SceneManager.LoadScene("Menu");
            }

            if (goIndex == lesGOs.Count)
            {
                arrow.target = null;

                SceneManager.LoadScene("Menu");
            }
        }

        //With absolute value
        public bool Approximately(Vector3 other, float allowedDifference)
        {
            Vector3 me = this.transform.position;

            var dx = me.x - other.x;
            if (Mathf.Abs(dx) > allowedDifference)
                return false;

            var dy = me.y - other.y;
            return Mathf.Abs(dy) < allowedDifference;
        }

        public GameObject ToGameObject(JouetObjet jo)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

            go.transform.position = new Vector3(jo.X, DEFAULT_Y, jo.Z);
            go.name = jo.Name;

            return go;
        }
    }
}