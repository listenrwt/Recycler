using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick); //Add the delegate into "onClick" event
    }

    private void TaskOnClick()
    {
        AudioManager.instance.PlaySound("Select");
    }
}
