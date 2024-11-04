using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public List<BowlingPin> pins;
    public BowlingBall ball;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

