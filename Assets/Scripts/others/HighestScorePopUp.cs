using UnityEngine;

public class HighestScorePopUp : MonoBehaviour
{
    private void Start()
    {
        //Show "Highest Score!!!" indicator if the result is highter then previous highest score
        if(PlayerPrefs.GetInt("previous") < PlayerPrefs.GetInt("first"))
        {
            gameObject.SetActive(false);
        }
    }
}
