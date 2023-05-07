using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blood : MonoBehaviour
{
  [SerializeField] Image image;
    [SerializeField] Color color0;
    [SerializeField] Color color1;
    [SerializeField] float lerpSpeed = 5f;
    [SerializeField] float alphaLerpDuration = 5f;
    private bool takingDamage;
    private float health = 1f;



    [Range(0f, 1f)]
    [SerializeField] float maxhelth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (takingDamage)
        {
            health -= Time.deltaTime;
            health = Mathf.Clamp01(health);
        }
        else
        {
            health += Time.deltaTime;
            health = Mathf.Clamp01(health);
        }
    
        
       
        image.color = Color.Lerp(color0, color1, health);
       

        takingDamage = false; 
    }
    public void TakeDamage(float damageAmount)
    {
        takingDamage = true;
        health -= damageAmount;
        health = Mathf.Clamp01(health);
    }
}
