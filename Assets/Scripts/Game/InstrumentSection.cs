using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentSection : MonoBehaviour
{
    private const int VOLUME_INCREMENT = 2;
    private const int MAX_VOLUME = 10;
    private const int MIN_VOLUME = 0;
    private const int TEMPO_INCREMENT = 10;
    private const int MAX_TEMPO = 120;
    private const int MIN_TEMPO = 80;
    private const float PITCH_INCREMENT = 0.5f;
    private const float MAX_PITCH = 1.5f;
    private const float MIN_PITCH = -1.5f;


    [SerializeField]
    private float pitch;

    [SerializeField]
    private int volume;

    [SerializeField]
    private int tempo;

    [SerializeField]
    private string instrument;

    [SerializeField]
    private Sprite image;

    public Sprite DisplayImage
    {
        get { return image; }
        set { image = value; }
    }

    public float Pitch
    {
        get { return pitch; }
        set { pitch = value; }
    }

    public int Volume
    {
        get { return volume; }
        set { volume = value; }
    }

    public int Tempo
    {
        get { return tempo; }
        set { tempo = value; }
    }

    public string Instrument
    {
        get { return instrument; }
        set { instrument = value; }
    }

    public void IncreaseVolume()
    {
        if (Volume + VOLUME_INCREMENT >= MAX_VOLUME)
        {
            Volume = MAX_VOLUME;
        }
        else 
        {
            Volume += VOLUME_INCREMENT;
        }
    }

    public void IncreaseTempo()
    {
        if (Tempo + TEMPO_INCREMENT >= MAX_TEMPO)
        {
            Tempo = MAX_TEMPO;
        }
        else
        {
            Tempo += TEMPO_INCREMENT;
        }
    }

    public void IncreasePitch()
    {
        if (Pitch + PITCH_INCREMENT >= MAX_PITCH)
        {
            Pitch = MAX_PITCH;
        }
        else
        {
            Pitch += PITCH_INCREMENT;
        }
    }

    public void DecreaseVolume()
    {
        if (Volume - VOLUME_INCREMENT <= MIN_VOLUME)
        {
            Volume = MIN_VOLUME;
        }
        else
        {
            Volume -= VOLUME_INCREMENT;
        }
    }

    public void DecreaseTempo()
    {
        if (Tempo - TEMPO_INCREMENT <= MIN_TEMPO)
        {
            Tempo = MIN_TEMPO;
        }
        else
        {
            Tempo -= TEMPO_INCREMENT;
        }
    }

    public void DecreasePitch()
    {
        if (Pitch - PITCH_INCREMENT <= MIN_PITCH)
        {
            Pitch = MIN_PITCH;
        }
        else
        {
            Pitch -= PITCH_INCREMENT;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
