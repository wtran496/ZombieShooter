using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType,ammoAmount);
            Destroy(gameObject);
        }
    }
}
