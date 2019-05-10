using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject explosionLayerMask;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);

        Collider[] ennemiesHit = Physics.OverlapSphere(gameObject.transform.position, 10, 1 << 10); // 1 << 10 = only layer 10
        foreach (Collider ennemyhit in ennemiesHit)
        {
            if (ennemyhit.name == "Head Collider")
                ennemyhit.gameObject.GetComponentInParent<Ennemy>().CmdTakeDamage(100);

        }

        Destroy(gameObject);
    }
}
