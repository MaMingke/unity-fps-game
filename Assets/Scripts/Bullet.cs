using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public Rigidbody Rigid;

    private void OnEnable()
    {
        StartCoroutine(nameof(DelayDespawn)); //¿ªÊ¼ÑÓ³Ù´Ý»Ù
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Target") //Èç¹ûÅö×²µ½°Ð
        {
            collision.collider.GetComponent<Target>().OnHitPoint(collision.contacts[0].point);
        }
        Destroy(gameObject);
    }


    private IEnumerator DelayDespawn()
    {
        yield return new WaitForSeconds(5); //ÑÓ³Ù£¿Ãëºó´Ý»Ù
        Destroy(gameObject);
    }
}
