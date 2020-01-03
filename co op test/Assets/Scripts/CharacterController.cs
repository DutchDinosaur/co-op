using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float RunMultip = 1.5f;
    [SerializeField] float WalkingThreshold;
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

        Camera.main.GetComponent<CameraController>().trackpos = transform;
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
            if (Input.GetKey(KeyCode.LeftShift)) {
                spd *= RunMultip;
                aninimator.SetBool("Running", true);
            }
            else {
                aninimator.SetBool("Running", false);
            }

            rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * spd * Time.deltaTime, 0, Input.GetAxis("Vertical") * spd * Time.deltaTime));
        }
    }
}