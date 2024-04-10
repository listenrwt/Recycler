using UnityEngine;

public class TouchSystem : MonoBehaviour
{
    public static TouchSystem instance;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //Detect whether a collider is touched
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get the position of first touch
            
            //Create a raycast hitbox to interect with collider
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            
            //Find the interface (ITouchable) of the touchedObject and run the function
            if(hit.collider != null)
            {
                ITouchable hittedObj = hit.collider.GetComponent<ITouchable>();
                hittedObj?.TouchAction();
            }           

        }

    }
}
