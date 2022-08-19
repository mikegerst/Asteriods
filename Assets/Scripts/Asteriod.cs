using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteriod : MonoBehaviour
{
    [SerializeField] private float minSpeed = 10;
    [SerializeField] private float maxSpeed = 30;
    [SerializeField] private ParticleSystem asteriodDestructionParticleSystem;

    float speed;
    bool _secondGen = false;
    Rigidbody2D _rigidbody;
    Vector2 direction;

    public static event Action<GameObject> asteriodSplit;
    public static event Action<GameObject> asteriodDestoryed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Start()
    {
        direction = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        _rigidbody.AddForce(direction * speed * 1000);
        yield return new WaitForSeconds(.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject coll = collision.gameObject;

        if(!coll.CompareTag("Player") && !coll.CompareTag("Asteriod"))
        if (!_secondGen)
        {
            
            Instantiate(asteriodDestructionParticleSystem, transform.position, Quaternion.identity);
                for (int i = 0; i < 3; i++)
                {
                    var asteriod = Instantiate(this.gameObject, transform.position, Quaternion.identity);
                    asteriodSplit?.Invoke(asteriod);
                    asteriod.transform.localScale = asteriod.transform.localScale / 3;
                    asteriod.GetComponent<Asteriod>()._secondGen = true;
                    switch (i)
                    {
                        case 0:
                            asteriod.transform.position += transform.right * 20;
                            break;
                        case 1:
                            asteriod.transform.position -= transform.right * 20;
                            break;
                        case 2:
                            asteriod.transform.position += transform.up * 20;
                        break;
                    }
                }

            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }

    public bool IsSecondGen()
    {
        return _secondGen;
    }

    private void OnDestroy()
    {
        asteriodDestoryed?.Invoke(this.gameObject);
    }


}
