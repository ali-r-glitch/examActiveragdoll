using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layermanager : MonoBehaviour
{
    // Start is called before the first frame update
    private int layer=-1;
    public string[] layerName;

    public string setlayer()
    {
        layer++;
        return layerName[layer];
    }

}
