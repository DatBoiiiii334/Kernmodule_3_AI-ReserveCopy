using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public string[] mytags;
    public int healing;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (string tag in mytags) {
            if (collision.collider.tag == tag) {
                collision.collider.GetComponent<IGiveHp>().GiveHealth(healing);
                transform.gameObject.SetActive(false);
            }
        }

        if (collision.collider.tag == "wall") {
            transform.gameObject.SetActive(false);
        }
    }
}
