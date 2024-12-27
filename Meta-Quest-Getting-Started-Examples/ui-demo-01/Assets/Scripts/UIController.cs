using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject mainPanel;
    public GameObject imagePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowImagePanel()
    {
        mainPanel.SetActive(false);
        imagePanel.SetActive(true);
    }
}
