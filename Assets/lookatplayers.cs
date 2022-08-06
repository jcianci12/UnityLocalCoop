using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatplayers : MonoBehaviour
{
    public Transform target;
    public GameObject go =null;

    void Update()
    {
        if (go != null)
        {
        transform.LookAt(go.transform);
        }
        else
        {
        go = GameObject.Find("PlayerPrefab(Clone)");

        }
        // Rotate the camera every frame so it keeps looking at the target


        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        //transform.LookAt(target, Vector3.left);
        //if (GameObject.Find("PlayerPrefab")!=null)
        //{

        //}
    }
}
