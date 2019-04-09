using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A broadly useful script for storing and accessing large numbers of AudioSources by organizing them into an Array
public class TestAudioController : MonoBehaviour
{
    /* AUDIO CLIPS */
    // These are the indexes used from the Audio Controller on the PlayerController script / gameObject
    // 0: Pistol Shot
    // 1: Shotgun Shot
    // 2: Item Pickup
    // 3: Damage Taken
    // 4: Pistol Reload
    // 5: Shotgun Reload

    
    public AudioSource[] clips; // Stores the AudioSources
    
    // Populates the array with AudioSources from the children of the GameObject
    void Start()
    {
        clips = new AudioSource[transform.childCount];
        
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i] = transform.GetChild(i).GetComponent<AudioSource>();
        }
       
    }
    
    // Plays the Clip pointed to by the Index
    public void playClip(int i)
    {
        endAllClips();
        clips[i].Play();
    }
    
    // Ends all clips to prevent awkward overlap. May not be necessary
    public void endAllClips()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i].Stop();
        }
    }
}