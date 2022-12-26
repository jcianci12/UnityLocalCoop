using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropOffZone : MonoBehaviour
{
    public GameObject go; 
    public List<GameObject> currentList;
    public List<GameObject> totalList;

    // Start is called before the first frame update
    void Start()
    {
        go.GetComponent<TMPro.TMP_Text>().text = ("");
        currentList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        go.GetComponent<TMPro.TMP_Text>().text = getStatus(currentList.Count,totalList.Count);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cargo")
        {
            if(!currentList.Contains(other.gameObject))
            currentList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (currentList.Contains(other.gameObject))
            currentList.Remove(other.gameObject);
    }
    private string getStatus(int current,int total)
    {
        return current.ToString() + "/" + total.ToString();
    }
}
