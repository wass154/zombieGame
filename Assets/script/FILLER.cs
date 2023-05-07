using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FILLER : MonoBehaviour
{



    [SerializeField] float maxSprint;
    [SerializeField] float currentSprint;
    [SerializeField] float sprintDecreaseRate;
    [SerializeField] float sprintIncreaseRate;
    [SerializeField] Image sprintBarFill;
    [SerializeField] Animator playerAnimator;

    private bool isSprinting;


    // Start is called before the first frame update
    void Start()
    {
        currentSprint = maxSprint;
        sprintBarFill.fillAmount = currentSprint / maxSprint;
        isSprinting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentSprint > 0)
        {
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || currentSprint <= 0)
        {
            isSprinting = false;
        }
        if (isSprinting)
        {
            currentSprint -= sprintDecreaseRate * Time.deltaTime;
            currentSprint = Mathf.Clamp(currentSprint, 0, maxSprint);
            sprintBarFill.fillAmount = currentSprint / maxSprint;
            playerAnimator.SetBool("isWalking", false);
        }
        else
        {
            currentSprint += sprintIncreaseRate * Time.deltaTime;
            currentSprint = Mathf.Clamp(currentSprint, 0, maxSprint);
            sprintBarFill.fillAmount = currentSprint / maxSprint;
            if (sprintBarFill.fillAmount == 0)
            {
                playerAnimator.SetBool("isWalking", true);
            }
        }
    }
}


