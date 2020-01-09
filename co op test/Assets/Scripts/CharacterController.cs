using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float RunMultip = 1.5f;
    [SerializeField] float JumpForce = 50;

    Rigidbody rb;
    PlayerManager pm;
    Animator aninimator;
    SpriteRenderer spriteRenderer;

    float spd;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerManager>();
        aninimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (pm.IsMine) {
            Camera.main.GetComponent<CameraController>().trackpos = transform;
        }
    }

    private void Update() {
        if (pm.IsMine) {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                aninimator.SetBool("Walking", true);

                if (Input.GetAxis("Horizontal") < 0) {
                    spriteRenderer.flipX = true;
                }
                else {
                    spriteRenderer.flipX = false;
                }
            }
            else {
                aninimator.SetBool("Walking", false);
            }

            spd = speed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                spd *= RunMultip;
                aninimator.SetBool("Running", true);
            }
            else {
                aninimator.SetBool("Running", false);
            }

            Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            //rb.velocity = new Vector3(inputVector.x * spd, rb.velocity.y,inputVector.y * spd);
            rb.MovePosition(transform.position + new Vector3(inputVector.x * spd, 0, inputVector.y * spd));

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
                rb.velocity += Vector3.up * JumpForce;
            }

            //rb.AddForce(new Vector3(inputVector.x * spd * Time.deltaTime, 0, inputVector.y * spd * Time.deltaTime));
            //rb.AddForce(new Vector3(-rb.velocity.x * counterForce,0,-rb.velocity.z * counterForce));

            if (Input.GetKey(KeyCode.E) && isGrounded()) {
                aninimator.SetBool("Pot", true);
            }
            else {
                aninimator.SetBool("Pot", false);
            }
        }
    }

    private bool isGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, .75f + 0.1f);
    }
}