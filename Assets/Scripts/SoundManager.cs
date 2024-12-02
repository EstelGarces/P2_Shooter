using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clip;
    private AudioSource controlAudio;

    void Start()
    {
        controlAudio = GetComponent<AudioSource>();
    }
    public void SeleccionAudio(int i, float volumen)
    {
        controlAudio.PlayOneShot(clip[i], volumen);
    }
}
