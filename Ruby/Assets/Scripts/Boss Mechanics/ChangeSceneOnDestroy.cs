using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnDestroy : MonoBehaviour
{
    Health health;
    private float CurrHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnDestroy()
    {
        CurrHealth = health.GetHealth();
        if (CurrHealth == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
