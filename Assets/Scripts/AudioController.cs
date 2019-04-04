using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    /* PLAYER AUDIO CLIPS */
    // 1: Damage Taken
    // 2: Pickup Item
    // 3: Shotgun Fire
    // 4: Pistol Fire
    // 5: Open/Close Menu
    // 6: Button Push
    
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
