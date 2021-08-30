using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera = null;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 10f;
    [SerializeField] ParticleSystem muzzleFlash = null;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] Ammo ammoSlot = null;
    [SerializeField] AmmoType ammoType = 0;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] Text ammoText = null;
    bool canShoot = true;

    //when this instance is enabled
    private void OnEnable()
    {
        canShoot = true; 
    }
    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) && canShoot) {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo() {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        if (ammoType == AmmoType.pistolBullets) ammoText.text = "Pistol: ";
        else if (ammoType == AmmoType.Shells) ammoText.text = "Shotgun: ";
        else if (ammoType == AmmoType.carbineBullets) ammoText.text = "Carbine: ";
        ammoText.text += currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        //hit something? yes or no
        // where hit from and direction and range and what collides
        //camera then forward direction
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
           
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);    
            Debug.Log("I hit this: " + hit.transform.name);
        }
        else
        {
            return;
        }
    }
    private void CreateHitImpact(RaycastHit hit) {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
}
