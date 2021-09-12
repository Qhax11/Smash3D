using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorAnimation : MonoBehaviour
{
    public float rotationAxisX;
    public float rotationAxisZ;
    private void FixedUpdate()
    {
          transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+ Time.deltaTime * rotationAxisZ);

      //  transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + Time.deltaTime * rotationAxisX, 0, 0);
        transform.Rotate( Time.deltaTime * rotationAxisX, 0,0);
    }



}
