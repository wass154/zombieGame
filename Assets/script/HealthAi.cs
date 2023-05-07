using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAi : MonoBehaviour
{
    [SerializeField] int HealthEnemy;
    
   private int BulletTakes;
    public ZombieSpawner Z;

    public delegate void EnemyDeath();
    public event EnemyDeath OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BulletTakes>=10 || HealthEnemy <= 0) {


            if (OnDeath != null)
            {
                OnDeath();
            }
            this.gameObject.SetActive(false);
            print("dead");
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletTakes++;
            HealthEnemy = HealthEnemy - 10;
       
        }
       
    }
}
