using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Image Content;    

    [SerializeField]
    public Health Health;

    void Start()
    {
        Content = GetComponent<Image>();
    }    

    void Update()
    {
        if (Health.MaxAmount > 0)
        {
            var currentfillamount = Health.Amount / Health.MaxAmount;

            if (Content.fillAmount != currentfillamount)
            {
                if (currentfillamount <= 0)
                    Content.fillAmount = 0;
                else
                    Content.fillAmount = Mathf.Lerp(Content.fillAmount, currentfillamount, Time.deltaTime * 5f);
            }                
        }            
    }
}
