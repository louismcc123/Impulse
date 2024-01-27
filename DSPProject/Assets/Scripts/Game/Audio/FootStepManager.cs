using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FootstepManager : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public float minTimeBetweenFootsteps = 0.3f;
    public float maxTimeBetweenFootsteps = 0.6f;

    private AudioSource audioSource;
    private bool isWalking = false;
    private float timeSinceLastFootstep;

    private Transform xrRigTransform;
    private Vector3 lastPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Assign the XR Rig's transform (replace "YourXRRigGameObject" with the actual name)
        xrRigTransform = GameObject.Find("XR Origin (XR Rig)").transform;

        if (xrRigTransform == null)
        {
            Debug.LogError("XR Rig Transform not found. Make sure to set the correct GameObject name.");
        }

        lastPosition = xrRigTransform.position;
    }

    void Update()
    {
        // Check if the XR Rig is moving based on position changes
        isWalking = (xrRigTransform.position - lastPosition).magnitude > 0.1f;
        lastPosition = xrRigTransform.position;

        if (isWalking)
        {
            if (Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
            {
                PlayFootstepSound();
            }
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];

            if (footstepSound != null)
            {
                audioSource.PlayOneShot(footstepSound);
                timeSinceLastFootstep = Time.time;
            }
            else
            {
                Debug.LogWarning("Footstep sound is null. Make sure the AudioClip is assigned.");
            }
        }
        else
        {
            Debug.LogWarning("Footstep sounds array is empty. Add AudioClips to the array.");
        }
    }
}
