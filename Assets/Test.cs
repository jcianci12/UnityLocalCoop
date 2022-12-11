using ProjectDawn.SplitScreen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        var s = gameObject.GetComponent<Camera>();

        s.gameObject.SetActive(false);
        s.gameObject.SetActive(true);
    }
}
