using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Camera Camera;
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
        GetComponentsInChildren<Text>().First(x =>x.name.Equals("Instrument")).text = section.Instrument;
        GetComponentsInChildren<Text>().First(x => x.name.Equals("Volume")).text = section.Volume.ToString();
        GetComponentsInChildren<Text>().First(x => x.name.Equals("Pitch")).text = section.Pitch.ToString();
        GetComponentsInChildren<Text>().First(x => x.name.Equals("Tempo")).text = section.Tempo.ToString();
    }
}
