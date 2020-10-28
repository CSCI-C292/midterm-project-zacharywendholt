using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvas : MonoBehaviour
{
    private Text _scoreText;
    void Start()
    {
        _scoreText = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + Score._score.ToString();
    }
}
