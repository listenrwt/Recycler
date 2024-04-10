using UnityEngine;

public class RefreshPrefs : MonoBehaviour
{
    private void Start()
    {
        //Refresh the Menu Scene at the beginning to load the PlayerPrefs
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene("MenuScene");
    }
}
