using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private GameObject audioPrefab;

    [Header("Mixer")]
    [SerializeField] private AudioMixer mainMixer;

    private Transform cameraTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (instance.cameraTransform == null)
        {
            if (Camera.main != null)
            {
                instance.cameraTransform = Camera.main.transform;
            }
            else
            {
                instance.cameraTransform = FindObjectOfType<Camera>().transform;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level">from 0.0001 to 1 (cant be 0)</param>
    public void SetAudioLevel(float level)
    {
        if (level <= 0.01) level = 0.01f;

        mainMixer.SetFloat("Volume", Mathf.Log10(level) * 40);
    }

    /// <summary>
    /// Play audio clip, audio source on main camera
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    public static GameObject Play(AudioClip audioClip, bool loop = false)
    {
        return instance.PlaySoundInternal(audioClip: audioClip, loop: loop);
    }

    public static GameObject PlayVoiceOver(AudioClip audioClip)
    {

        return instance.PlaySoundInternal(audioClip: audioClip, mixerGruop: "Voice Over");
    }


    /// <summary>
    /// Play random audio clip, audio source on main camera
    /// </summary>
    /// <param name="audioClips"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    public static GameObject Play(AudioClip[] audioClips, bool loop = false)
    {
        int random = Random.Range(0, audioClips.Length);
        return instance.PlaySoundInternal(audioClip: audioClips[random], loop: loop);
    }

    /// <summary>
    /// Play audio clip, audio source at specific position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="audioClip"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    public static GameObject Play(Vector3 position, AudioClip audioClip, bool loop = false)
    {
        return instance.PlaySoundInternal(position: position, audioClip: audioClip, loop: loop);
    }

    /// <summary>
    /// Play random audio clip, audio source at specific position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="audioClips"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    public static GameObject Play(Vector3 position, AudioClip[] audioClips, bool loop = false)
    {
        int random = Random.Range(0, audioClips.Length);
        return instance.PlaySoundInternal(position: position, audioClip: audioClips[random], loop: loop);
    }

    /// <summary>
    /// Play audio clip, audio source on specific transform
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="audioClip"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    public static GameObject Play(Transform transform, AudioClip audioClip, bool loop = false)
    {
        if (transform == null)
        {
            return null;
        }

        return instance.PlaySoundInternal(transform: transform, audioClip: audioClip, loop: loop);
    }

    /// <summary>
    /// Play random audio clip, audio source on specific transform
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="audioClips"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    public static GameObject Play(Transform transform, AudioClip[] audioClips, bool loop = false)
    {
        if (transform == null)
        {
            return null;
        }

        int random = Random.Range(0, audioClips.Length);
        return instance.PlaySoundInternal(transform: transform, audioClip: audioClips[random], loop: loop);
    }


    GameObject PlaySoundInternal(AudioClip audioClip, Transform transform = null, Vector3 position = default, bool loop = false, string mixerGruop = "Master")
    {
        if (audioClip == null)
        {
            return null;
        }

        if (instance.cameraTransform == null)
        {
            if (Camera.main != null)
            {
                instance.cameraTransform = Camera.main.transform;
            }
            else
            {
                instance.cameraTransform = FindObjectOfType<Camera>().transform;
            }
        }

        GameObject soundObject = Instantiate(instance.audioPrefab);

        if (transform != null)
        {
            soundObject.transform.SetParent(transform);
            soundObject.transform.localPosition = Vector3.zero;
        }
        else if (position != default)
        {
            soundObject.transform.position = position;
        }
        else
        {
            soundObject.transform.SetParent(cameraTransform);
            soundObject.transform.localPosition = Vector3.zero;
        }

        AudioSource source = soundObject.GetComponent<AudioSource>();
        source.outputAudioMixerGroup = instance.mainMixer.FindMatchingGroups(mixerGruop)[0];
        source.loop = loop;
        source.clip = audioClip;
        source.Play();

        if (!loop)
        {
            soundObject.GetComponent<SoundObject>().DestroyAfter(audioClip.length);//destroy if not looping
        }

        return soundObject;
    }
}
