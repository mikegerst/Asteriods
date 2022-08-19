using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float turnAngle = .1f;

    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotPoint;

    Vector3 _mousePos;
    Rigidbody2D _rigidbody;

    public static event Action PlayerDied;
    private void Start()
    {
        _mousePos = Input.mousePosition;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(transform.up * speed);

            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxSpeed);
            _rigidbody.angularVelocity = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.forward, turnAngle);
            //_rigidbody.SetRotation(turnAngle);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-transform.forward, turnAngle);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        var s = Instantiate(shot, shotPoint.position, transform.rotation);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Input.mousePosition, 2f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteriod"))
        {
            PlayerDied?.Invoke();
            Destroy(this.gameObject);
        }
            
    }
}
