using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public List<GameObject> lesGOs;
    public GameObject closest;

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
    private const float DEFAULT_Y = 3f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(-106.4f, DEFAULT_Y, -75f);

        lesGOs.Add(cube);
        //Cursor.lockState = CursorLockMode.Locked;*

        foreach(GameObject go in lesGOs)
        {
            go.SetActive(false);
        }
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
        if (Input.GetKey("q") )
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
        GameObject closest = GetClosestGameObject();

        arrow.target = closest;

        if (Approximately(closest.transform.position, MIN_DIST))
        {
            closest.SetActive(true);
        }
        else
        {
            closest.SetActive(false);
        }

        if (Input.GetKey(KeyCode.O))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(this.transform.position.x, DEFAULT_Y, this.transform.position.z);
            lesGOs.Add(cube);
        }
    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 180, 20), "Créer un objet (O)"))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(this.transform.position.x, DEFAULT_Y, this.transform.position.z);
            lesGOs.Add(cube);
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

    public GameObject GetClosestGameObject()
    {
        float smallest = 99999f;
        int smallestIndex = 0;
        GameObject toReturn = null;
        int i = 1;

        foreach(GameObject go in lesGOs)
        {
            float dist = Vector3.Distance(go.transform.position, this.transform.position);
            if (dist < smallest)
            {
                smallest = dist;
                toReturn = go;
                smallestIndex = i;
            }

            go.SetActive(false);

            i++;
        }
        Debug.Log("Le plus proche est le numéro " + smallestIndex);

        return toReturn;
    }
}
