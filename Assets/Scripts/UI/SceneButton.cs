using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private string scene;

    public void TaskOnClick() {
        SceneManager.LoadScene(scene);
    } 
}
