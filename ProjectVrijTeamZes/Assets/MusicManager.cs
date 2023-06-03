using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic;

    private AudioSource backgroundSource;
    // Start is called before the first frame update
    void Start()
    {
        backgroundSource = GameObject.Find("Music").GetComponent<AudioSource>();
        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
