using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page 
{
    int lifespan;

    public const int MAX_LIFESPAN = 30;

    public Part DrumPart { get; set; }
    public Part PianoPart { get; set; }
    public Part CelloPart { get; set; }

    public Page(int lifespan = MAX_LIFESPAN)
    {
        this.lifespan = lifespan;
        DrumPart = new Part("Drum", Random.Range(-3, 3) * 0.5f, Random.Range(0, 5) * 2, Random.Range(8, 12) * 10);
        PianoPart = new Part("Piano", Random.Range(-3, 3) * 0.5f, Random.Range(0, 5) * 2, Random.Range(8, 12) * 10);
        CelloPart = new Part("Cello", Random.Range(-3, 3) * 0.5f, Random.Range(0, 5) * 2, Random.Range(8, 12) * 10);
    }
}
