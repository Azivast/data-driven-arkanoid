using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeSceneTo(string nextScene) {
        SceneManager.LoadScene(nextScene);
    }
}
