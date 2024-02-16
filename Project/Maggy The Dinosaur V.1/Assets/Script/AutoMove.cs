using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] private int movingSpeed = 10;

    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * movingSpeed;
    }

}
