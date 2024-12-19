using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal toPortal;

    public static bool tpActive;

    void Start()
    {
        tpActive = true;
    }

    private void OnTriggerEnter2D(Colider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (tpActive && rb != null)
        {
            tpActive = false;
            float magnitude = rb.velocity.magnitude; // скорость выхода из портала
            rb.velocity = Vector3.zero;
            Vector3 direction = toPortal.transform.TransformDirection(Vector3.right) - transform.TransformDirection(Vector3.left);
            other.transform.position = toPortal.transform.position;
            rb.AddForce(direction * magnitude, ForceMode2D.Impulse);
        }
        else tpActive = true;
    }

}
