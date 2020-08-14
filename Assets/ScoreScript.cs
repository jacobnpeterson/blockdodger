using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    //public Transform textTransform;
    public TextMeshProUGUI score;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        score.SetText(((int)player.position.z).ToString());
    }

}
