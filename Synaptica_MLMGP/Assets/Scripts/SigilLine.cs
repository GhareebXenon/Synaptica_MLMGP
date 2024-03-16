using UnityEngine;

[System.Serializable]
public class SigilLine
{
    public GameObject startPoint;
    public GameObject endPoint;

    public SigilLine(GameObject startPoint, GameObject endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }
}
