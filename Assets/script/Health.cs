using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float MaxHealth;
    [SerializeField] float CurrentHealth;
    [SerializeField] float lerpDuration;
    [SerializeField] bool isAttack;
    public static float PassHealth;
    [SerializeField] Blood bloodScreen;
    [SerializeField] private Image fillImage1;
    [SerializeField] private Image fillImage2;

    private float targetFillAmount;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        targetFillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {
            CurrentHealth = CurrentHealth - 20 * Time.deltaTime;
            PassHealth = CurrentHealth;
            bloodScreen.TakeDamage(20 * Time.deltaTime);

            float fillAmount = CurrentHealth / MaxHealth;
            fillImage1.fillAmount = fillAmount;
            fillImage2.fillAmount = fillAmount;

            targetFillAmount = fillAmount;
        }
        else
        {
            CurrentHealth = Mathf.Lerp(CurrentHealth, MaxHealth, lerpDuration * Time.deltaTime);
            PassHealth = CurrentHealth;

            targetFillAmount = 1f;
        }

        // Lerp the fill amount of the images
        float newFillAmount = Mathf.Lerp(fillImage1.fillAmount, targetFillAmount, lerpDuration * Time.deltaTime);
        fillImage1.fillAmount = newFillAmount;
        fillImage2.fillAmount = newFillAmount;

        if (CurrentHealth < 0)
        {
            print("PlayerDied");
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand") && !isAttack)
        {
            isAttack = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand") && isAttack)
        {
            isAttack = false;
        }
    }
}