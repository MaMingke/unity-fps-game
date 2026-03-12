using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody rigid;

    private void Start()
    {
        rigid.velocity = Vector3.up;
    }
}
