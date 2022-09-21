using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMessage : MonoBehaviour
{
    [SerializeField] Sprite candy;
    static GameObject candySprite;
    static Sprite candySpriteStatic;
    private static GameObject popUpText;
    static GameObject text;
    public GameObject textNS;
    static float popUpTimer = 0;
    void Start()
    {
        text = textNS;

        candySpriteStatic = candy;
    }

    // Update is called once per frame
    void Update()
    {
        popUpTimer += Time.deltaTime;
        if(popUpText != null && popUpText.GetComponent<Text>().color.a != 0)
        {
            Color c = popUpText.GetComponent<Text>().color;
            Color c2 = candySprite.GetComponent<SpriteRenderer>().color;
            popUpText.GetComponent<Text>().color = new Color(c.r, c.g, c.b, c.a - 0.025f);
            popUpText.transform.position = new Vector2(popUpText.transform.position.x,popUpText.transform.position.y + .02f);
            candySprite.GetComponent<SpriteRenderer>().color = new Color(c2.r, c2.g, c2.b, c2.a - 0.025f);
        }
        if(popUpText != null && popUpTimer > 2f)
        {
            Destroy(popUpText);
        }
    }

    public static void StartPopUpMessageCandy(int message,Canvas parent)
    {
        if(message == 0) { return; }
        if(popUpText != null) { Destroy(popUpText); }
        popUpText = Instantiate(text, parent.transform);
        candySprite = Instantiate(new GameObject("candy"),popUpText.transform,true);
        candySprite.AddComponent<SpriteRenderer>().sprite = candySpriteStatic;
        candySprite.transform.position = new Vector2(popUpText.transform.position.x + .6f, popUpText.transform.position.y);
        candySprite.transform.localScale = new Vector2(candySprite.transform.localScale.x / 2.5f, candySprite.transform.localScale.y / 2.5f);
        popUpText.GetComponent<Text>().text = "" + message;
        popUpText.GetComponent<Text>().color = new Color(.5f, .2f, .1f, 1);
        popUpTimer = 0;
    }
}
