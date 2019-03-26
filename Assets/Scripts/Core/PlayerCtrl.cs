using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speedmove = 1;
    public float speedrot = 1;

    private CharacterController cc;
    private float ver;
    private float hor;
    private Vector3 dir;
    private Quaternion qua;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        dir = new Vector3(hor, 0, ver).normalized;
        cc.SimpleMove(dir * speedmove);
        //transform.position = Vector3.Lerp(transform.position,transform.position+dir, Time.deltaTime * speedmove);

        if (hor != 0 || ver != 0)
        {
            qua = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, qua, Time.deltaTime * speedrot);
        }
    }
}
