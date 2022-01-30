using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaslightMovement : MonoBehaviour
{
    [SerializeField] GameObject cam = null;

    private Vector3 relativePosition;

    [SerializeField] private float _rotationSpeed = 20f;
    
    
    private Vector2 startingRotation = Vector2.zero;
    void Update()
    {
        transform.rotation =
            Quaternion.Slerp (transform.rotation, cam.transform.rotation, Time.deltaTime * _rotationSpeed);
        transform.position = cam.transform.TransformPoint(relativePosition);
        
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            var enemy = hit.transform.GetComponent<Enemy>();
            if (enemy)
                enemy.Hurt();
        }
    }
}
