using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Transform TrackingPosition;
    [SerializeField] private float maxRayDistance = 50;
    [SerializeField] private float shadowDistance = 0.01f;

    [SerializeField] private SpriteRenderer sprite;

    void LateUpdate() {
        RaycastHit hit;
        Ray ShadowRay = new Ray(TrackingPosition.position, Vector3.down);

        if (Physics.Raycast(ShadowRay, out hit, maxRayDistance)){
            sprite.enabled = true;

            transform.position = hit.point + (hit.normal * shadowDistance);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.rotation *= Quaternion.Euler(90,0,0);
        }
        else {
            sprite.enabled = false;
        }
    }
}