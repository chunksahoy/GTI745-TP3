using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    int pageIndex = 0;

    private System.TimeSpan totalDuration;

    List<Page> pages = new List<Page>();

    public List<Page> Pages
    {
        get { return pages; }
        set { pages = value; }
    }

    [SerializeField]
    private AudioClip songClip;
    public AudioClip Audio
    {
        get { return songClip; }
        set { songClip = value; }
    }

    public System.TimeSpan TotalDuration
    {
        get
        {
            if (totalDuration == System.TimeSpan.Zero)
            {
                totalDuration = new System.TimeSpan(0, 0, System.Convert.ToInt32(Audio.length));
            }
            return totalDuration;
        }
    }

    public Page NextPage
    {
        get
        {
            Page page = null;
            if (pageIndex < pages.Count)
            {
                page = pages[pageIndex];
                pageIndex++;
            }
            return page;
        }      
    }

    public void InitPages()
    {
        pageIndex = 0;

        int nbPages = (int)Audio.length / Page.MAX_LIFESPAN;

        for (int i = 0; i < nbPages; ++i)
        {
            pages.Add(new Page());
        }

        int extra = (int)Audio.length % Page.MAX_LIFESPAN;

        if (extra != 0)
        {
            pages.Add(new Page(extra));
        }
    }

    private void Awake()
    {

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
