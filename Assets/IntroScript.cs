using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public void StartPlaying()
    {
        SceneManager.LoadScene("Level1");
        Cursor.visible = false;
    }
}
