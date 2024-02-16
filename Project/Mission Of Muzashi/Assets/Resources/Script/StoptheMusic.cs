using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoptheMusic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().EndMusic();
        }
    }
}
