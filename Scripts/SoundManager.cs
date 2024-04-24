using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip shoot;
    public static AudioClip bgm;

    public static bool isBgPlaying = false; // ����׷�ٱ��������Ƿ����ڲ���

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        shoot = Resources.Load<AudioClip>("shoot");
        bgm = Resources.Load<AudioClip>("bgm");
        PlayBgm();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayShootClip()
    {
        audioSrc.PlayOneShot(shoot);
    }

    public static void PlayBgm()
    {
        if (!isBgPlaying)
        {
            audioSrc.clip = bgm;
            audioSrc.loop = true; // Ϊ����BGMѭ������
            audioSrc.Play();
            isBgPlaying = true;
        }
    }
}
