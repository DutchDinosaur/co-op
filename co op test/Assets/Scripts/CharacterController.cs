using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0,Input.GetAxis("Vertical") * speed * Time.deltaTime));
    }
}