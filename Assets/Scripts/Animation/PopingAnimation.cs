using UnityEngine;
using System.Collections;

public class PopingAnimation : MonoBehaviour
{
    public float popTime;
    public AnimationCurve curve;
    public bool DestroyAfterPopOut = true;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;

        StartCoroutine(PopIn());  //Play the pop in animation
    }

    public void StopPopInAnimation()
    {
        enablePopIn = false;
        transform.localScale = originalScale;
    }

    private bool enablePopIn;

    //Play the pop in animation
    IEnumerator PopIn()
    {
        enablePopIn = true;
        transform.localScale = new Vector3(0f, 0f, transform.localScale.z);
        float timer = 0;
        while(timer <= popTime && enablePopIn)
        {
            //change the scale of the object base on the Animation Curve
            float x = curve.Evaluate(timer / popTime) * originalScale.x;
            float y = curve.Evaluate(timer / popTime) * originalScale.y;
            transform.localScale = new Vector3(x, y, 0f);

            timer += Time.deltaTime;

            yield return new WaitForSeconds(0);

        }

        if(enablePopIn)
        transform.localScale = originalScale;

    }

    public void PopOutAnimation()
    {        
        StartCoroutine(PopOut()); //Play the pop out animation
    }

    //Play the pop out animation
    IEnumerator PopOut()
    {
        float timer = popTime;
        while (timer >= 0)
        {
            //change the scale of the object base on the Animation Curve
            float x = curve.Evaluate(timer / popTime) * originalScale.x;
            float y = curve.Evaluate(timer / popTime) * originalScale.y;
            transform.localScale = new Vector3(x, y, 0f);

            timer -= Time.deltaTime;

            yield return new WaitForSeconds(0);

        }
        if (DestroyAfterPopOut)
        {
            Destroy(gameObject);
        }else
        {
            transform.localScale = Vector3.zero;
        }
    }

}
