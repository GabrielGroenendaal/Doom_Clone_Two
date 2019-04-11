using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudioController : MonoBehaviour
{
    /* PLAYER AUDIO CLIPS */
    // 0: Pistol Shot
    // 1: Shotgun Shot
    // 2: Item Pickup
    // 3: Damage Taken
    // 4: Pistol Reload
    // 5: Shotgun Reload

    
    public AudioSource[] clips;
    
    void Start()
    {
        clips = new AudioSource[transform.childCount];
        
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i] = transform.GetChild(i).GetComponent<AudioSource>();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void playClip(int i)
    {
        endAllClips();
        clips[i].Play();
    }
    
    public void endAllClips()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i].Stop();
        }
    }
}