using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//to use animation triggers, from https://www.youtube.com/watch?v=7vMHTUwtyNs
public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnForehandPerformed;
    public UnityEvent OnBackhandPerformed;

    public void TriggerForehand()
    {
        OnForehandPerformed?.Invoke();
    }

    public void TriggerBackhand()
    {
        OnBackhandPerformed?.Invoke();
    }
}
