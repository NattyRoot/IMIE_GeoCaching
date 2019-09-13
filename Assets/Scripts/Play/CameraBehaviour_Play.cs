using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using System.Linq;

namespace com.imie.geocaching
{
    public class CameraBehaviour_Play : MonoBehaviour
    {
        public GameData gameData;

        public List<GameObject> lesGOs;
        public int goIndex = 0;

        public ArrowBehaviour arrow;
        
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
        
        private const float MIN_DIST = 5f;
        private const float DEFAULT_Y = 0f;

        public bool developpement = false;

        // Start is called before the first frame update
        void Start()
        {
            gameData = GameData.LoadJson();
            Camera.main.fieldOfView = 80f;

            List<JouetObjet> lesJOs = gameData.Parcours[PlayerPrefs.GetInt("ParcoursIndex")].JouetObjets;

            lesJOs.ForEach(jo => lesGOs.Add(ToGameObject(jo)));

            arrow.target = lesGOs[0];

            //lesGOs.ForEach(go => go.SetActive(false));
        }

        // Update is called once per frame
        void Update()
        {

            if (developpement)
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
            }
            

            // AFFICHAGE GAMEOBJECTS
            if (goIndex != lesGOs.Count && Approximately(lesGOs[goIndex].transform.position, MIN_DIST))
            {
                lesGOs[goIndex].GetComponent<MeshRenderer>().material.color = Color.green;
                lesGOs[goIndex].SetActive(true);

                goIndex++;

                if (goIndex == lesGOs.Count)
                {
                    arrow.target = null;

                    //SceneManager.LoadScene("Menu");
                }
                else
                {
                    arrow.target = lesGOs[goIndex];
                }
            }
        }


        private void OnGUI()
        {
            if (GUI.Button(new Rect(20, 40, 400, 200), "Retour au menu"))
            {
                SceneManager.LoadScene("Menu");
            }

            GUI.Label(new Rect(Screen.width - 250, 0f, 500, 200), " Camera : {" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + "}");
            if (lesGOs.Count > 0 && goIndex < lesGOs.Count)
            {
                GUI.Label(new Rect(Screen.width - 250, 200f, 500, 200), " Cube (" + goIndex + ") : {" + lesGOs[goIndex].transform.position.x + ", " + lesGOs[goIndex].transform.position.y + ", " + lesGOs[goIndex].transform.position.z + "}");
            }
        }

        //With absolute value
        public bool Approximately(Vector3 other, float allowedDifference)
        {
            Vector3 me = this.transform.position;

            var dx = me.x - other.x;
            if (Mathf.Abs(dx) > allowedDifference)
                return false;

            var dz = me.z - other.z;
            return Mathf.Abs(dz) < allowedDifference;
        }

        public GameObject ToGameObject(JouetObjet jo)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

            go.transform.position = new Vector3(jo.X + transform.position.x, DEFAULT_Y, jo.Z + transform.position.z);
            go.name = jo.Name;

            return go;
        }
    }
}