using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ControlPanel : MonoBehaviour
{
    private UnityEngine.UI.Image[] scrollables;
    private InstrumentSection currentInstrument;

    int scrollIndex = 0;

    public Camera Camera;
    Text Name,
        Volume,
        Pitch,
        Tempo;

    UnityEngine.UI.Image Display;
    void Awake()
    {
        
        Display = GetComponentsInChildren<UnityEngine.UI.Image>().First(x => x.name.Equals("Display"));
        Name = GetComponentsInChildren<Text>().First(x => x.name.Equals("Instrument"));
        Volume = GetComponentsInChildren<Text>().First(x => x.name.Equals("Volume"));
        Pitch = GetComponentsInChildren<Text>().First(x => x.name.Equals("Pitch"));
        Tempo = GetComponentsInChildren<Text>().First(x => x.name.Equals("Tempo"));

        scrollables = GetComponentInChildren<Canvas>().GetComponentsInChildren<RectTransform>().First(x => x.name.Equals("Scrollable")).GetComponentsInChildren<UnityEngine.UI.Image>();
        Debug.Log(scrollables.Length);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = Camera.transform.position - transform.position;
        v.x = v.z = 0f;
        transform.LookAt(Camera.transform.position - v);
        transform.Rotate(0, 180, 0);
    }

    public void Reset(InstrumentSection section)
    {
        currentInstrument = section;
        UpdateInfos();
    }

    public void UpdateInfos()
    {
        Display.sprite = currentInstrument.DisplayImage;
        Name.text = currentInstrument.Instrument;
        Volume.text = currentInstrument.Volume.ToString();
        Pitch.text = currentInstrument.Pitch.ToString();
        Tempo.text = currentInstrument.Tempo.ToString();
    }

    public void NavigateControls(int scroll)
    {
        scrollables[scrollIndex].color = Color.clear;
        if (scrollIndex + scroll < 0)
        {
            scrollIndex = scrollables.Length - 1;
        }
        else
        {
            scrollIndex += scroll;
            scrollIndex %= scrollables.Length;
        }
        scrollables[scrollIndex].color = Color.white;
    }

    public void ExitNavigation()
    {
        scrollables[scrollIndex].color = Color.clear;
    }

    public string HighlightedAttribute()
    {
        return scrollables[scrollIndex].name.Replace("Section", "");
    }
}
