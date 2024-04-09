using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    private float cloudSpeed;

    private Rigidbody2D cloudrb;
    // Start is called before the first frame update
    void Start()
    {
        cloudrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Cloud will move soon!");
        cloudrb.velocity = new Vector2(cloudSpeed, cloudrb.velocity.y);
        
    }
}
