using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatplayers : MonoBehaviour
{
    public Transform target;
    //public GameObject go = null;
    //List<Vector3> vector3List;
    private GameManagerScript gameManager;
    private void Start()
    {
        

    }
    void Update()
    {
        if (gameManager)
        {
            var meanvector = gameManager.GetMeanVector();
            if (meanvector != Vector3.zero)
            {
                //var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.transform.position = meanvector;
                transform.LookAt(meanvector);
                //Destroy(cube);


            }
            else
            {
                transform.LookAt(target);
            }
        }
        else
        {
            var go = GameObject.FindGameObjectWithTag("GameManager");
            gameManager = go.GetComponent<GameManagerScript>();
            //gameManager = GameObject.FindObjectOfType<GameManagerScript>();
        }

    }
    

}
