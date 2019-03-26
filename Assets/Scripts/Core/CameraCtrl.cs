using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target;
    public Vector3 relativePos = new Vector3(0, 15, -9);
    [Range(2,50)]
    public float dis = 20;
    public float fieldtime = 0.6f;

    private Camera cam;
    private float vel;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        transform.position = target.position + relativePos;
        transform.LookAt(target.position);

        float dist = (target.position - transform.position).magnitude;
        float requiredFOV = Mathf.Atan2(dis, dist) * Mathf.Rad2Deg;
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, requiredFOV, ref vel, fieldtime);
    }
}
