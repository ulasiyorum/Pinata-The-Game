using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelAndPrice : MonoBehaviour
{
    public int price;
    public int x;
    public int i;
    public SkillTree skillTree;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    // Update is called once per frame
    void Update()
    {
        if (i != 5)
        {
            if (x == 1)
            {
                GetComponent<Text>().text = (skillTree.getLevels1()[i]) + "/5" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n";
                if (skillTree.getLevels1()[i] != 5) { GetComponent<Text>().text = (skillTree.getLevels1()[i]) + "/5" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + price * (skillTree.getLevels1()[i] + 1); }
            }
            else if (x == 2)
            {
                GetComponent<Text>().text = (skillTree.getLevels2()[i]) + "/5" + "\n" + "\n" + "\n" + "\n " + "\n" + "\n";
                if (skillTree.getLevels2()[i] != 5) { GetComponent<Text>().text = (skillTree.getLevels2()[i]) + "/5" + "\n" + "\n" + "\n" + "\n " + "\n" + "\n" + price * (skillTree.getLevels2()[i] + 1); }
            }
            else if (x == 3)
            {
                GetComponent<Text>().text = (skillTree.getLevels3()[i]) + "/5" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n";
                if (skillTree.getLevels3()[i] != 5) { GetComponent<Text>().text  = (skillTree.getLevels3()[i]) + "/5" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + price * (skillTree.getLevels3()[i] + 1); }
            }
        } else
        {
            if (x == 1)
            {
                if (skillTree.getLevels1()[i] != 5) { GetComponent<Text>().text = (skillTree.getLevels1()[i]) + "/5" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + price * (skillTree.getLevels1()[i] + 1); }
                else { GetComponent<Text>().text = "MAXED"; }
            }
            else if (x == 2)
            {
                
                if (skillTree.getLevels2()[i] != 5) { GetComponent<Text>().text = (skillTree.getLevels2()[i]) + "/5" + "\n" + "\n" + "\n" + "\n " + "\n" + "\n" + price * (skillTree.getLevels2()[i] + 1); }
                else { GetComponent<Text>().text = "MAXED"; }
            }
            else if (x == 3)
            {
                if (skillTree.getLevels3()[i] != 5) { GetComponent<Text>().text = (skillTree.getLevels3()[i]) + "/5" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + price * (skillTree.getLevels3()[i] + 1); }
                else { GetComponent<Text>().text = "MAXED"; }
            }
        }
    }

}
