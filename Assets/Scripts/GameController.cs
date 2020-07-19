using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    float timer = 0f;
    float totalTime = 0f;

    Text timerDisplay;

    bool gameRunning = false;
    AudioSource audioSource;

    Text[] drumText;

    InstrumentSection[] instruments;

    [SerializeField]
    Song currentSong;
    
    Page currentPage;
    int currentPageIndex = 0;

    public Song CurrentSong
    {
        get { return currentSong; }
        set { currentSong = value; }
    }

    public bool GameOver
    {
        get
        {
            return false;
        }
    }

    void StartGame()
    {
        gameRunning = true;
        audioSource.clip = CurrentSong.Audio;
        audioSource.Play();
    }

    void UpdatePageInfo()
    {
        foreach (Text text in drumText)
        {
            switch (text.name)
            {
                case "Volume":
                    text.text = currentPage.DrumPart.Volume.ToString();
                    break;
                case "Pitch":
                    text.text = currentPage.DrumPart.Pitch.ToString();
                    break;
                case "Tempo":
                    text.text = currentPage.DrumPart.Tempo.ToString();
                    break;
            }
        }
    }

    private void Awake()
    {
        timerDisplay = GameObject.Find("Time").GetComponent<Text>();
        drumText = GameObject.Find("DrumSection").GetComponentsInChildren<Text>();

        GameObject.Find("TimeLeft").GetComponent<Text>().text = currentSong.TotalDuration.ToString();
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

        instruments = GameObject.Find("Instruments").GetComponentsInChildren<InstrumentSection>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentSong.InitPages();

        currentPage = CurrentSong.Pages[currentPageIndex];
        UpdatePageInfo();

        StartGame();
    }

    void CheckInstruments()
    {
        foreach (InstrumentSection instrument in instruments)
        {
            switch (instrument.name)
            {
                case "Drum":
                    break;
                case "Piano":
                    break;
                case "Cello":
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning && !GameOver)
        {
            timer += Time.deltaTime;

            var span = new System.TimeSpan(0, 0, System.Convert.ToInt32(totalTime));
            timerDisplay.text = span.ToString();

            if (timer > 1f)
            {
                //seconds increment
                totalTime++;
                timer = 0f;

                if ((int)totalTime > 0 && (int)totalTime % Page.MAX_LIFESPAN == 0)
                {
                    currentPageIndex++;
                    currentPage = CurrentSong.Pages[currentPageIndex];
                    UpdatePageInfo();
                }

                //check if instruments are on tune
            }
        }
        else
        {

        }
    }
}
