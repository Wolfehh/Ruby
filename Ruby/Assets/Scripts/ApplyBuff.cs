﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    public float rapidFireRate; // Rate at which rapid fire shoots
    
    public enum BuffType
    {
        RapidFire,
        HealthPickup
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision) // When object with this script collides with 'powerup' object
    {
       

        PowerUp pow = collision.GetComponent<PowerUp>();

        if (pow != null)//Checks if the collision object (The powerup) has PowerUp component
        {
            print("got my pp up");
            StartCoroutine(ApplyEffect(pow));
            Destroy(collision.gameObject); // Destroys powerup after use
        }

    }


    IEnumerator ApplyEffect(PowerUp power) {

        switch (power.type) {

            case BuffType.RapidFire:
                Shoot shoot = GetComponent<Shoot>();
                print("Locked and Loaded");
                shoot.setFireRate(rapidFireRate); // Set fire rate located in shoot script
                yield return new WaitForSeconds(power.powerupExtent); // Waits using powerupExtend (seconds)
                shoot.setFireRate(shoot.defaultFireRate); // Returns fire rate to default
                break;


            case BuffType.HealthPickup:
                Health health = GetComponent<Health>();
                print("Res me, bro");
                health.currentHealth = health.currentHealth + power.healAmount;//Adds value to current health
                break;


                //Can add more types of powerups here

        }
        
    }
    
}
