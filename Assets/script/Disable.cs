using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Disable : MonoBehaviour
{
    public UnityEvent OnDisabl;

    private void OnDisable()
    {
        if (OnDisabl != null)
        {
            OnDisabl.Invoke();
        }
    }
}
