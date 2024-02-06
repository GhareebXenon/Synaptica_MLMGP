using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera slidingPuzzleCamera;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private Transform piecePrefap;
    [SerializeField] private Transform backPiecePrefap;
    [SerializeField] private Transform passwordTextPrefap;
    [SerializeField] private Keypad keypad;
    [SerializeField] private int size = 3;
    [SerializeField] private float pieceGap = 0.1f;
    [Tooltip("0 for solved puzzle")][SerializeField] private int difficulty = 3;

    private List<Transform> pieces;
    private int emptyLocation;
    private bool shuffling = false;

    private void Start()
    {
        pieces = new List<Transform>();
        CreatePieces();
        Shuffle();
    }

    private void Update()
    {
        //Mouse interaction
        var ray = slidingPuzzleCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out piecePrefap))
                {
                    for (int i = 0; i < pieces.Count; i++)
                    {
                        if (pieces[i] == hit.transform)
                        {
                            if (SwapIfValid(i, -size, size)) { break; }
                            if (SwapIfValid(i, +size, size)) { break; }
                            if (SwapIfValid(i, -1, 0)) { break; }
                            if (SwapIfValid(i, +1, size - 1)) { break; }
                        }
                    }
                }
            }
        }


        if (!shuffling && CheckComplete())
        {
            Transform upperBack = transform.Find("Back5");
            Transform lastBack = transform.Find("Back8");
            lastBack.transform.position = Vector3.Lerp(lastBack.position, upperBack.position, 1.5f * Time.deltaTime);
            StartCoroutine(Countdown());
        }
    }

    private void CreatePieces()
    {
        float width = 1 / (float) size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefap, this.transform);
                Transform backPiece = Instantiate(backPiecePrefap, this.transform);
                pieces.Add(piece);
                //Set piece position from -1 to 1
                piece.localPosition = new Vector3 (-1 + (2 * width * col) + width, 1 - (2 * width * row) - width, 0);
                piece.localScale = ((2 * width) - pieceGap) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                backPiece.localPosition = new Vector3(-1 + (2 * width * col) + width, 1 - (2 * width * row) - width, 0.00002f);
                backPiece.localScale = 2 * width * Vector3.one;
                backPiece.name = $"Back{(row * size) + col}";
                //Set the empty piece location
                if (row ==  size - 1 && col == size - 1)
                {
                    emptyLocation = size * size - 1;
                    piece.gameObject.SetActive(false);

                    Transform passwordText = Instantiate(passwordTextPrefap, this.transform);
                    passwordText.localPosition = new Vector3(-1 + (2 * width * col) + width, 1 - (2 * width * row) - width, 0.00004f);
                    passwordText.localScale = 2 * width * Vector3.one;
                    passwordText.name = "Password";
                    passwordText.GetComponent<TextMeshPro>().text = keypad.keypadCombo.ToString();
                }
                //Set the UV coordinates
                float gap = pieceGap / 2;
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                Vector2[] uv = new Vector2[4];
                uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                mesh.uv = uv;
            }
        }
    }

    private void Shuffle()
    {
        shuffling = true;
        int count = 0;
        int last = 0;
        while (count < Mathf.Pow(size, difficulty))
        {
            if (difficulty == 0) break;

            int rnd = Random.Range(0, size * size);
            if (rnd == last) { continue; }
            last = emptyLocation;
            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }
        }
        shuffling = false;
    }

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            // Swap them in game state.
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            // Swap their transforms.
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            // Update empty location.
            emptyLocation = i;
            return true;
        }
        return false;
    }

    private bool CheckComplete()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}") return false;
        }
        return true;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1.5f);
        mainCamera.gameObject.SetActive(true);
        slidingPuzzleCamera.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerUI.SetActive(true);
    }
}
