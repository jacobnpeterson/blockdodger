using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button tryAgainButton;

    // Start is called before the first frame update
    void Start()
    {
        tryAgainButton.onClick.AddListener(() => ButtonClicked());   
    }

    // Update is called once per frame
    void ButtonClicked()
    {
        FindObjectOfType<GameState>().restore(); 
    }
}
