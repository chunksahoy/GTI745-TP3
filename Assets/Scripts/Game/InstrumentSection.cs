using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentSection : MonoBehaviour
{

    [SerializeField]
    private int pitch;

    [SerializeField]
    private int volume;

    [SerializeField]
    private int tempo;

    [SerializeField]
    private string instrument;

    public int Pitch
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
