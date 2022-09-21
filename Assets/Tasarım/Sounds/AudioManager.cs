using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip[] hit, woosh, moving;
    public static AudioClip bought, pageflip, summon;
    static AudioSource src;
    void Start()
    {
        src = GetComponent<AudioSource>();
        hit = new AudioClip[5];
        woosh = new AudioClip[2];
        moving = new AudioClip[5];
        for (int i = 0; i < hit.Length; i++) 
        {
            string s = "" + i;
            hit[i] = Resources.Load<AudioClip>("hit" + i);
            moving[i] = Resources.Load<AudioClip>("moving" + i);
        }
        for(int i=0; i<woosh.Length; i++)
        {
            string s = "" + i;
            woosh[i] = Resources.Load<AudioClip>("hitbehind" + i);
        }
        bought = Resources.Load<AudioClip>("bought");
        pageflip = Resources.Load<AudioClip>("pageflip");
        summon = Resources.Load<AudioClip>("summon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        int a = Random.Range(0,5);
        int b = Random.Range(0, 2);
        switch (clip)
        {
            case "hit":
                src.volume = 1;
                src.PlayOneShot(hit[a]);
                break;
            case "woosh":
                src.volume = 2;
                src.PlayOneShot(woosh[b]);
                break;
            case "bought":
                src.volume = 1;
                src.PlayOneShot(bought);
                break;
            case "moving":
                src.volume = 0.8f;
                src.PlayOneShot(moving[a]);
                break;
            case "pageflip":
                src.volume = 1;
                src.PlayOneShot(pageflip);
                break;
            case "summon":
                src.volume = 0.5f;
                src.PlayOneShot(summon);
                break;
            case "pop":
                src.volume = 0.5f;
                src.PlayOneShot(summon);
                break;
        }
    }

}
