using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<Parcours> Parcours;
}

[System.Serializable]
public class Parcours
{
    public List<JouetObjet> JouetObjets;

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
    
    [System.NonSerialized]
    private const float DEFAULT_Y = 3f;

    public GameObject ToGameObject()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.transform.position.Set(X, DEFAULT_Y, Z);
        go.name = Name;

        return go;
    }

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


