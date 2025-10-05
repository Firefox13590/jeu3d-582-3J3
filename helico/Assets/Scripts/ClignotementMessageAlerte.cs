using TMPro;
using UnityEngine;

public class ClignotementMessageAlerte : MonoBehaviour
{
    TMP_Text tmpAlerte;
    float timerClignotement = .5f;
    bool isVisible = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmpAlerte = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // ecoulement cooldown
        timerClignotement -= Time.deltaTime;

        if(timerClignotement <= 0)
        {
            //Debug.Log("cligne");
            timerClignotement = .5f;

            // clignote entre rouge opaque et transparent
            if (isVisible)
            {
                tmpAlerte.color = new Color(1, 0, 0, 0);
            }
            else
            {
                tmpAlerte.color = new Color(1, 0, 0, 1);
            }
            isVisible = !isVisible;

            //Debug.Log(tmpAlerte.color);
            //Debug.Log(tmpAlerte.alpha);
            //Debug.Log(tmpAlerte.text);
        }
    }
}
