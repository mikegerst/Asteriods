using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        StartCoroutine("ShotMove");
    }

    IEnumerator ShotMove()
    {
        while (this)
        {
            transform.position += transform.up * speed * Time.deltaTime;
            yield return null;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
