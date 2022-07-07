using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleMovement : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Translate(x, 0, 0);
        transform.Translate(0, y, 0);
    }
}
