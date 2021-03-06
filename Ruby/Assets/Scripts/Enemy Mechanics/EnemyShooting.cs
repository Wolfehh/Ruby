using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float shotDelay = .2f; // Delay between shots
    public float offsetY = 0.0f; // offsets projectile origin Y
    public float offsetX = 0.0f; // offsets projectile origin X
    public GameObject projectilePrefab;

    private GameObject  player;
    private Animator anim;
    public MovingEnemy  movingEnemy;

    private AggroTimer aggroTimer;
    bool firecontrol = false; // used to stop unity from starting several shoot coroutines at once


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        movingEnemy = GetComponent<MovingEnemy>();
        player = GameObject.FindWithTag("Player");
        aggroTimer = GetComponent<AggroTimer>();
    }

    // coroutine that spawns projectile and moves it towards a given point
    void startShooting()
    {
        Debug.Log("startshooting"); // debug
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() // style convention dictates coroutines are generally capitalized
    {
        while (aggroTimer.isAggro && (GameObject.Find("Ruby") != null || GameObject.Find("Trevor") != null))
        {
            Debug.Log("shot"); // debug
            
            // Determine angle projectile needs to move in
            float angleX = (player.transform.position.x + .5f) - (transform.position.x + offsetX);
            float angleY = (player.transform.position.y + 1.0f) - (transform.position.y + offsetY);
            float shotAngle = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg - 90f;

            // Determine shot origin based on this object's position with given offsets applied
            Vector3 shotOrigin = transform.position;
            shotOrigin.x = shotOrigin.x + offsetX; // apply offsets
            shotOrigin.y = shotOrigin.y + offsetY;

            Instantiate(projectilePrefab, shotOrigin, Quaternion.Euler(new Vector3(offsetX, offsetY, shotAngle)));
            yield return new WaitForSeconds(shotDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Ruby") != null || GameObject.Find("Trevor") != null)
        {
            //seesPlayer = movingEnemy.seesPlayer;
            Vector3 playerDirection = transform.position - player.transform.position;

            // if enemy sees player and is within firerate
            if (aggroTimer.isAggro && !firecontrol)
            {
                anim.SetBool("Shooting", true); // Tell the animator if the enemy is shooting to set the appropriate sprites
                startShooting();
                firecontrol = true;
            }

            // if the enemy is no longer antagonized, turn off firecontrol
            if (!aggroTimer.isAggro)
            {
                anim.SetBool("Shooting", false); // Tell the animator if the enemy is shooting to set the appropriate sprites
                firecontrol = false;
                //StopCoroutine(Shoot());
            }
        }
    }
}
