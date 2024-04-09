using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D cloud)
    {
        if (cloud.tag == "cloud")
        {
            Destroy(cloud);
        }
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnInactive()
    {
        gameObject.SetActive(false);
    }

}
