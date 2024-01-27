using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vocals : MonoBehaviour
{
    private AudioSource source;
    private Dictionary<string, float> lastPlayTimes = new Dictionary<string, float>();
    public float defaultCooldownTime = 5f;

    public static Vocals instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void Say(AudioObject clip)
    {
        float cooldownTime = GetCooldownTime(clip);

        if (Time.time - GetLastPlayTime(clip) >= cooldownTime)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }

            source.PlayOneShot(clip.clip);

            Subtitle.instance.SetSubtitle(clip.subtitle, clip.clip.length);

            SetLastPlayTime(clip, Time.time);
        }
    }

    private float GetCooldownTime(AudioObject clip)
    {
        return defaultCooldownTime;
    }

    private float GetLastPlayTime(AudioObject clip)
    {
        string clipName = clip.clip.name;
        if (!lastPlayTimes.ContainsKey(clipName))
        {
            lastPlayTimes.Add(clipName, -GetCooldownTime(clip));
        }

        return lastPlayTimes[clipName];
    }

    private void SetLastPlayTime(AudioObject clip, float time)
    {
        string clipName = clip.clip.name;
        if (lastPlayTimes.ContainsKey(clipName))
        {
            lastPlayTimes[clipName] = time;
        }
        else
        {
            lastPlayTimes.Add(clipName, time);
        }
    }
}
