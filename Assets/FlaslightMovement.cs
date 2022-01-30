using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaslightMovement : MonoBehaviour
{
    [SerializeField] GameObject cam = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.rotation = cam.transform.rotation;
    }
}
