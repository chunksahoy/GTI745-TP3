using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEye : MonoBehaviour
{
    private GameObject selected;
    private GameObject viewed;

    ControlPanel infoPanel;
    Vector3 CameraCenter;
    private void Awake()
    {
        infoPanel = GameObject.FindObjectOfType<ControlPanel>();
        CameraCenter = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, GetComponent<Camera>().nearClipPlane));
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        float thickness = .1f;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.SphereCast(origin, thickness, direction, out hit, 10))
        {
            if (hit.collider != null)
            {
                if (!hit.transform.gameObject.name.Equals("Floor"))
                {
                    // Offset position above object box (in world space)
                    float offsetPosY = hit.collider.bounds.center.y + 1.2f;

                    // Final position of marker above GO in world space
                    Vector3 offsetPos = new Vector3(hit.collider.bounds.center.x, offsetPosY, hit.collider.bounds.center.z);
                    infoPanel.transform.position = offsetPos;
                    viewed = hit.transform.gameObject;

                    infoPanel.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>().alpha = 1f;
                    infoPanel.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>().interactable = true;
                    infoPanel.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>().blocksRaycasts = true;

                    InstrumentSection section = viewed.GetComponent<InstrumentSection>();
                    infoPanel.Reset(section);
                }
                else
                {
                    infoPanel.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>().alpha = 0f;
                    infoPanel.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>().interactable = false;
                    infoPanel.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }
        }
    }
}
