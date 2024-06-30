using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPaper : MonoBehaviour
{

    [SerializeField] private GameObject Paper;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            Paper.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Paper.SetActive(false);
        }

    }
}
