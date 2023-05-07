using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject attach;
    [SerializeField] int maxBullets;
    [SerializeField] float bulletLifetime = 2.0f;
    [SerializeField] float bulletSpeed = 2.0f;
    [SerializeField] float RayLength ;
    [SerializeField] Camera fpsCamera;
    private List<GameObject> bulletPool;
    public LayerMask hitLayers;
    // Start is called before the first frame update
    void Start()
    {
       
        bulletPool = new List<GameObject>();
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    public void Shoot()
    {
        // Retrieve a bullet instance from the pool and activate it
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {

                RaycastHit hit;
                if (Physics.Raycast(attach.transform.position, attach.transform.forward, out hit, Mathf.Infinity, hitLayers))
                {
                    Debug.DrawLine(attach.transform.position, attach.transform.forward , Color.red, RayLength);
                    bulletPool[i].transform.position = attach.transform.position;
                    bulletPool[i].transform.rotation = Quaternion.LookRotation(hit.point - attach.transform.position);
                }
                else
                {
                    bulletPool[i].transform.position = attach.transform.position;
                    bulletPool[i].transform.rotation = attach.transform.rotation;
                }
                    bulletPool[i].transform.position = attach.transform.position;
                bulletPool[i].transform.rotation = attach.transform.rotation;
                Rigidbody bulletRb = bulletPool[i].GetComponent<Rigidbody>();
                bulletRb.velocity = attach.transform.forward * bulletSpeed;
                bulletPool[i].SetActive(true);
                StartCoroutine(DeactivateBulletAfterLifetime(bulletPool[i]));
               // return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    IEnumerator DeactivateBulletAfterLifetime(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletLifetime);
        bullet.SetActive(false);
    }
}
