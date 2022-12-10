using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    //private static List<Vector3> vector3List = new List<Vector3>();

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPlayer(GameObject gameObject)
    {
        gameObjects.Add(gameObject);
    }
    public void removePlayer(GameObject gameObject)
    {
        if (gameObjects.Contains(gameObject))
        {
            gameObjects.Remove(gameObject);
        }
    }
    //public void PlayerJoined(GameObject player)
    //{
    //    vector3List.Add(player.transform.position);
    //}
    public Vector3 GetMeanVector()
    {
        
            //List<Vector3> positions = vector3List;
        var positions = GameObject.FindObjectsOfType<PlayerController>().Select(i=>i.transform.position).ToList();


            if (positions.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 meanVector = Vector3.zero;

            foreach (Vector3 pos in positions)
            {
                meanVector += pos;
            }

            return (meanVector / positions.Count);
        
        

    }
}
