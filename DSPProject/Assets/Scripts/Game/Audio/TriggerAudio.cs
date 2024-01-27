using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public AudioObject triggerAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone");

            if (Vocals.instance != null)
            {
                Vocals.instance.Say(triggerAudio);
                Debug.Log("Playing trigger audio");
            }
            else
            {
                Debug.LogError("Vocals instance is null");
            }
        }
    }
}
