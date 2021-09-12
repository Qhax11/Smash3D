using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseAndRotate : MonoBehaviour
{
    bool up=true, down=false;
    private float playerY;
    public float riseValue;
    public float rotationValue;
    private void Start()
    {
        playerY = transform.position.y;
    }
    void Update()
    {
        if (transform.position.y > playerY + riseValue)
        {
            up = false;
            down = true;
        }
        else if (transform.position.y < playerY )
        {
            up = true;
            down = false;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Time.deltaTime * rotationValue, transform.rotation.eulerAngles.z);
       // transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + Time.deltaTime * rotationValue, transform.rotation.eulerAngles.y);
        if (up)
        {
          //  transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * riseValue, transform.position.z);
        }
        if (down)
        {
          //  transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * riseValue, transform.position.z);
        }
    }
}
