using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ResourceInfo("ClearUI")]
public class ClearUI : UI
{
    [SerializeField] private Image image;

    private bool fadeIn = true;
    public override void Open(UIParameter param)
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        while(true)
        {
            if(fadeIn)
            {
                if(image.color.a < 1)
                {
                    var tempColor = image.color;
                    tempColor.a += Time.deltaTime;
                    image.color = tempColor;
                    yield return null;
                    if (image.color.a >= 0.6)
                    {
                        fadeIn = false;
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

}
