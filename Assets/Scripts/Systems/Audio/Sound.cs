using UnityEngine;

[System.Serializable] //Save the info of the Audio using serializable class 
public class Sound 
{
    public string name;
    public AudioClip clip;
    public bool loop = false;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(-3f, 2f)]
    public float pitch = 1f;
    [HideInInspector]
    public AudioSource source;
}
