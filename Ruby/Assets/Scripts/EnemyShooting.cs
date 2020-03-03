﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float fireDelay = .2f; // Delay between shots
    public float dispersion; // TODO will implement later
    public MovingEnemy movingEnemy;
    public GameObject projectilePrefab;
    public GameObject player;
    public LayerMask obstacles;

    private bool allowFire;


    // Start is called before the first frame update
    void Start()
    {
        movingEnemy = GetComponent<MovingEnemy>();
        obstacles = 1<<8; // This sets the layerMask to the "Obstacle" unity layer. It's a literal bit mask. Ask Kasey if you need clarification.
        player = GameObject.FindWithTag("Player");
    }

    // coroutine that spawns projectile and moves it towards a given point
    IEnumerator Shoot()
    {
        while (true)
        {
            //Debug.Log("Enemy shot"); // debug
            allowFire = false;

            // Determine angle projectile needs to move in
            float angleX = player.transform.position.x - transform.position.x;
            float angleY = player.transform.position.y - transform.position.y;
            float shotAngle = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg - 90f;

            // Instantiate given prefab
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, shotAngle)));

            //yield return WaitForSeconds(fireDelay); // Wait for fireDelay
            allowFire = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //seesPlayer = movingEnemy.seesPlayer;
        Vector3 playerDirection = transform.position - player.transform.position;

        // if enemy sees player, is within firerate and not obstructed by obstacles
        if (allowFire && movingEnemy.getSee() && !Physics2D.Raycast(transform.position, playerDirection, Mathf.Infinity, obstacles, -Mathf.Infinity, Mathf.Infinity))
        {
            Shoot();
        }
    }
}