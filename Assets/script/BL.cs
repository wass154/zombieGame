using UnityEngine;
using UnityEngine.UI;


public class BL: MonoBehaviour
{
    [SerializeField] Image bloodScreenImage;
    [SerializeField] Color color0;
    [SerializeField] Color color1;
    [SerializeField] float lerpSpeed = 10f;
    [SerializeField] float alphaLerpDuration = 5f;

    private float currentHealth;
    private float targetHealth;
    private bool takingDamage;

    private void Start()
    {
        currentHealth = 1f; // Start at full health
        targetHealth = currentHealth;
        takingDamage = false;
    }

    private void Update()
    {
        // Check for changes in health
        if (targetHealth != currentHealth)
        {
            // Calculate the new health value using Lerp
            currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * lerpSpeed);

            // Update the blood screen color
            float alpha = Mathf.Lerp(0f, 1f, (1f - currentHealth) / alphaLerpDuration);
            bloodScreenImage.color = new Color(color1.r, color1.g, color1.b, alpha);
        }
        else if (!takingDamage && currentHealth < 1f)
        {
            // If not taking damage and health is not full, regenerate health
            targetHealth = 1f;
        }

        takingDamage = false;
    }

    public void TakeDamage(float damageAmount)
    {
        // Decrease the target health value and set takingDamage flag
        targetHealth = Mathf.Clamp01(targetHealth - damageAmount);
        takingDamage = true;
    }
}