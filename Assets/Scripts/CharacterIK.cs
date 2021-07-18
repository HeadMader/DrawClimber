using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    [SerializeField]
    private float velocity= 0.2f;
Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + Vector3.right *velocity* Time.fixedDeltaTime);
    }
}
