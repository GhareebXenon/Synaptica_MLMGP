using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskRunes : MonoBehaviour
{
    [SerializeField] private Texture runeSprite;
    [SerializeField] private ObeliskPuzzle obeliskPuzzle;

    [SerializeField] private int runeNumber;

    private void Start()
    {
        GetComponent<MeshRenderer>().material.mainTexture = runeSprite;
        if (int.TryParse(runeSprite.name.Trim(), out runeNumber))
        {
            obeliskPuzzle.runesNumbers.Add(runeNumber);
            obeliskPuzzle.UpdateSelected();
        }
        else if (runeSprite.name == "Addition")
        {
            obeliskPuzzle.operation = 0;
        }
        else if (runeSprite.name == "Subtraction")
        {
            obeliskPuzzle.operation = 1;
        }
        else if (runeSprite.name == "Multiplication")
        {
            obeliskPuzzle.operation = 2;
        }
        else if (runeSprite.name == "Division")
        {
            obeliskPuzzle.operation = 3;
        }
    }
}
