using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page 
{
    Part drumPart;
    Part pianoPart;
    Part celloPart;
    int lifespan;

    public const int MAX_LIFESPAN = 10;

    public Part DrumPart
    {
        get { return drumPart; }
        set { drumPart = value; }
    }

    public Page(int lifespan = MAX_LIFESPAN)
    {
        this.lifespan = lifespan;
        drumPart = new Part("Drum", Random.Range(-3, 3) * 0.5f, Random.Range(0, 20) * 5, Random.Range(40, 60) * 2);
        pianoPart = new Part("Piano", Random.Range(-3, 3) * 0.5f, Random.Range(0, 20) * 5, Random.Range(40, 60) * 2);
        celloPart = new Part("Cello", Random.Range(-3, 3) * 0.5f, Random.Range(0, 20) * 5, Random.Range(40, 60) * 2);
    }
}
