using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{

    public Button mainButton;
    public GameObject imagePanel;
    public GameObject mainPanel;

    // Start is called before the first frame update
    void Start()
    {
        mainButton.onClick.AddListener(OnMainButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMainButtonClick()
    {
        mainPanel.SetActive(false);
        imagePanel.SetActive(true);
    }
}
