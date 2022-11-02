using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropOffZone : MonoBehaviour
{
    public GameObject go; 
    public List<GameObject> list;
    // Start is called before the first frame update
    void Start()
    {
        go.GetComponent<TMPro.TMP_Text>().text = ("");
        list = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        go.GetComponent<TMPro.TMP_Text>().text = getStatus(list.Count,5);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cargo")
        {
            if(!list.Contains(other.gameObject))
            list.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (list.Contains(other.gameObject))
            list.Remove(other.gameObject);
    }
    private string getStatus(int current,int total)
    {
        return current.ToString() + " of " + total.ToString() + " cargo collected.";
    }
}
