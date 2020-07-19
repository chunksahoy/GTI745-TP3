using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Text t in GetComponentsInChildren<Text>())
        {
            t.rectTransform.position = new Vector3(t.rectTransform.position.x - 1, t.rectTransform.position.y, t.rectTransform.position.z);
            RectTransform rectTransform = GetComponent<RectTransform>();

            if (t.rectTransform.position.x < -(rectTransform.rect.width / 2))
            {
                t.rectTransform.position = new Vector3(GetComponent<RectTransform>().rect.width, t.rectTransform.position.y, t.rectTransform.position.z);
            }
        }
    }
}
