using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button myButton;

    private void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(Restart);

    }

    void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
