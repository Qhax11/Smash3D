using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeing : MonoBehaviour
{
    public float shakeSpeed, shakeVerge;
    private bool left=false, right=true;
    private void Update()
    {
        if(transform.rotation.eulerAngles.z > 180 + shakeVerge)
        {
            right = false;
            left = true;
        }
        else if(transform.rotation.eulerAngles.z < 180 - shakeVerge)
        {
            left = false;
            right = true;
        }
    }
    private void FixedUpdate()
    {

        if (right)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + Time.deltaTime * shakeSpeed);
        }
        if (left)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - Time.deltaTime * shakeSpeed);
        }

    }

}