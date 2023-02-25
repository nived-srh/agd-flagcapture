using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IceArrows : MonoBehaviour
{

    public float damage;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().currentHealth -= damage;
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }

}

