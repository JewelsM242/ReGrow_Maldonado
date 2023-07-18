using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public bool isPower;//judge whether the line is charged
    public List<GameObject> nextLines = new List<GameObject>();//reference of the other lines are connected
    public SpriteRenderer spriteRenderer;//the image of the line


    public void Rotate()//rotate the line
    {
        this.transform.Rotate(0, 0, -90);
    }

    public void AddNextLine(GameObject line)
    {
        nextLines.Add(line);
    }
    public void DeleteNextLine(GameObject line)
    {
        nextLines.Remove(line);
    }

    /// <summary>
    /// recursively power
    /// </summary>
    public void Power()
    {
        Debug.Log(this.transform.name + "powered");
        isPower = true;
        // power effect
        SetLuminance(true);
        // power the connected lines
        foreach (GameObject nextLine in nextLines)
        {
            var line = nextLine.GetComponent<Line>();
            if (!line.isPower)
            {
                line.Power();
            }
        }
    }

    /// <summary>
    /// reset the state
    /// </summary>
    public void ResetStatic()
    {
        isPower = false;
        SetLuminance(false);
    }

    /// <summary>
    /// change the color after powered
    /// </summary>
    /// <param name="p">true TurnRed/false changeBackToTheOriginalColor</param>
    public void SetLuminance(bool p)
    {
        if (p)
        {
            spriteRenderer.color = new Color32(240, 124, 130, 255);
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

    }

}
