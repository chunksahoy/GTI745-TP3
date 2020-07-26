using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    [System.Serializable]
    public struct InstrumentSpotlight
    {
        public string instrumentName;
        public Light spotlight;
    }

    public InstrumentSpotlight[] spotlights;

    float timer = 0f;
    float totalTime = 0f;

    Text timerDisplay;

    bool gameRunning = false;
    AudioSource audioSource;

    Text[] drumText;
    Text[] celloText;
    Text[] pianoText;

    Light lecternLight;

    InstrumentSection[] instruments;

    bool tuned = false;

    Canvas startGameUI;
    GameObject resultsPanel;

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
            return totalTime > CurrentSong.TotalDuration.TotalSeconds;
        }
    }

    public void StartGame()
    {
        lecternLight.enabled = false;
        startGameUI.enabled = false;

        foreach (InstrumentSpotlight spot in spotlights)
        {
            spot.spotlight.enabled = true;
        }

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

        foreach (Text text in celloText)
        {
            switch (text.name)
            {
                case "Volume":
                    text.text = currentPage.CelloPart.Volume.ToString();
                    break;
                case "Pitch":
                    text.text = currentPage.CelloPart.Pitch.ToString();
                    break;
                case "Tempo":
                    text.text = currentPage.CelloPart.Tempo.ToString();
                    break;
            }
        }

        foreach (Text text in pianoText)
        {
            switch (text.name)
            {
                case "Volume":
                    text.text = currentPage.PianoPart.Volume.ToString();
                    break;
                case "Pitch":
                    text.text = currentPage.PianoPart.Pitch.ToString();
                    break;
                case "Tempo":
                    text.text = currentPage.PianoPart.Tempo.ToString();
                    break;
            }
        }

    }

    private void Awake()
    {
        timerDisplay = GameObject.Find("Time").GetComponent<Text>();
        drumText = GameObject.Find("DrumSection").GetComponentsInChildren<Text>();
        celloText = GameObject.Find("CelloSection").GetComponentsInChildren<Text>();
        pianoText = GameObject.Find("PianoSection").GetComponentsInChildren<Text>();

        lecternLight = GameObject.Find("LecternLight").GetComponent<Light>();
        lecternLight.enabled = false;

        GameObject.Find("TimeLeft").GetComponent<Text>().text = currentSong.TotalDuration.ToString();
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

        instruments = GameObject.Find("Instruments").GetComponentsInChildren<InstrumentSection>();

        resultsPanel = GameObject.Find("Results");
        resultsPanel.SetActive(false);

        startGameUI = GameObject.Find("Lectern").GetComponentInChildren<Canvas>();
        startGameUI.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentSong.InitPages();

        currentPage = CurrentSong.Pages[currentPageIndex];
        UpdatePageInfo();
    }

    bool CheckInstruments()
    {
        bool onTune = true;
        foreach (InstrumentSection instrument in instruments)
        {
            if (onTune)
            {
                switch (instrument.name)
                {
                    case "Drum":
                        onTune = currentPage.DrumPart.IsOnTune(instrument);
                        break;
                    case "Piano":
                        onTune = currentPage.PianoPart.IsOnTune(instrument);
                        break;
                    case "Cello":
                        onTune = currentPage.CelloPart.IsOnTune(instrument);
                        break;
                }
            }
        }
        return onTune;
    }

    void DisplayResults()
    {
        float drumScoreVolume = 0f;
        float drumScorePitch = 0f;
        float drumScoreTempo = 0f;

        float celloScoreVolume = 0f;
        float celloScorePitch = 0f;
        float celloScoreTempo = 0f;

        float pianoScoreVolume = 0f;
        float pianoScorePitch = 0f;
        float pianoScoreTempo = 0f;

        foreach (Page page in CurrentSong.Pages)
        {
            if (page.DrumPart.IsVolumeValid)
            {
                drumScoreVolume++;
            }
            if (page.DrumPart.IsPitchValid)
            {
                drumScorePitch++;            
            }
            if (page.DrumPart.IsTempoValid)
            {
                drumScoreTempo++; 
            }

            if (page.CelloPart.IsVolumeValid)
            {
                celloScoreVolume++;
            }
            if (page.CelloPart.IsPitchValid)
            { 
                celloScorePitch++;
            }
            if (page.CelloPart.IsTempoValid)
            {
                celloScoreTempo++;
            }

            if (page.PianoPart.IsVolumeValid)
            {
                pianoScoreVolume++;
            }
            if (page.PianoPart.IsPitchValid)
            {
                pianoScorePitch++;
            }
            if (page.PianoPart.IsTempoValid)
            {
                pianoScoreTempo++;
            }
        }
        
        FindObjectOfType<ControlPanel>().gameObject.SetActive(false);
        resultsPanel.SetActive(true);
        
        var drumResults = GameObject.Find("DrumResults");
        drumResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Volume")).text = ((drumScoreVolume / CurrentSong.Pages.Count) * 100) + "%";
        drumResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Pitch")).text = ((drumScorePitch / CurrentSong.Pages.Count) * 100) + "%";
        drumResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Tempo")).text = ((drumScoreTempo / CurrentSong.Pages.Count) * 100) + "%";

        var celloResults = GameObject.Find("CelloResults");
        celloResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Volume")).text = ((celloScoreVolume / CurrentSong.Pages.Count) * 100) + "%";
        celloResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Pitch")).text = ((celloScorePitch / CurrentSong.Pages.Count) * 100) + "%";
        celloResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Tempo")).text = ((celloScoreTempo / CurrentSong.Pages.Count) * 100) + "%";

        var pianoResults = GameObject.Find("PianoResults");
        pianoResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Volume")).text = ((pianoScoreVolume / CurrentSong.Pages.Count) * 100) + "%";
        pianoResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Pitch")).text = ((pianoScorePitch / CurrentSong.Pages.Count) * 100) + "%";
        pianoResults.GetComponentsInChildren<Text>().First(x => x.name.Equals("Tempo")).text = ((pianoScoreTempo / CurrentSong.Pages.Count) * 100) + "%";
    }

    bool FirstTuning()
    {
        bool piaonoTuned = false;
        bool drumTuned = false;
        bool celloTuned = false;

        foreach (InstrumentSection instrument in instruments)
        {
            switch (instrument.name)
            {
                case "Drum":
                    drumTuned = currentPage.DrumPart.IsOnTune(instrument);
                    spotlights.First(x => x.instrumentName.Equals(instrument.name)).spotlight.enabled = !drumTuned;
                    break;
                case "Piano":
                    piaonoTuned = currentPage.PianoPart.IsOnTune(instrument);
                    spotlights.First(x => x.instrumentName.Equals(instrument.name)).spotlight.enabled = !piaonoTuned;
                    break;
                case "Cello":
                    celloTuned = currentPage.CelloPart.IsOnTune(instrument);
                    spotlights.First(x => x.instrumentName.Equals(instrument.name)).spotlight.enabled = !celloTuned;
                    break;
            }
        }
        return piaonoTuned && drumTuned && celloTuned;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning && !GameOver)
        {
            timer += Time.deltaTime;

            var span = new TimeSpan(0, 0, System.Convert.ToInt32(totalTime));
            timerDisplay.text = span.ToString();

            if (timer > 1f)
            {
                //seconds increment
                totalTime++;
                timer = 0f;

                GameObject.Find("PageTimer").GetComponent<Image>().fillAmount -= ((float)Page.MAX_LIFESPAN / 100) * .1f;

                //next page
                if ((int)totalTime > 0 && (int)totalTime % Page.MAX_LIFESPAN == 0)
                {
                    //check if instruments are on tune (scoring goes here)
                    CheckInstruments();

                    currentPageIndex++;
                    if (currentPageIndex < CurrentSong.Pages.Count)
                    {
                        currentPage = CurrentSong.Pages[currentPageIndex];
                        UpdatePageInfo();
                        GameObject.Find("PageTimer").GetComponent<Image>().fillAmount = 1;
                    }
                }
            }
        }
        else
        {
            //we either have yet to start the game (set-up phase, where the player adjust the initial values for the instruments), or we have finished the song
            //if game has yet to start -> display a way to start the game (button, action, location, etc)
            //else, GameOver, thus we display the results to the player
            if (!tuned && FirstTuning())
            {
                lecternLight.enabled = true;
                startGameUI.enabled = true;
                tuned = true;
            }
            
            if (GameOver)
            {
                DisplayResults();
            }
        }
    }
}
