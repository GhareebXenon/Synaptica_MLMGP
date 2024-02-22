using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private Transform piecePrefap;
    [SerializeField] private Transform backPiecePrefap;
    [SerializeField] private Transform passwordTextPrefap;
    [SerializeField] private Keypad keypad;
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private string mission;
    [SerializeField] private int size = 3;
    [SerializeField] private float gapBetweenPieces = 0.0f;
    [Tooltip("0 for a solved puzzle."), SerializeField, Range(0, 6)] private int difficulty = 4;

    private GameObject slidingPuzzleCamera;
    private List<Transform> pieces;
    private int emptyLocation;
    private int upperBackIndex;
    private int lastBackIndex;
    private bool shuffling = false;
    private bool completed = false;
    private bool revealed = false;

    private void Start()
    {
        slidingPuzzleCamera = transform.Find("SlidingPuzzleCamera").gameObject;
        pieces = new List<Transform>();
        upperBackIndex = (size * (size - 1)) - 1;
        lastBackIndex = (size * size) - 1;
        CreatePieces();
        Shuffle();
    }

    private void Update()
    {
        //Mouse interaction
        var ray = slidingPuzzleCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

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


        if (!revealed && !shuffling && completed)
        {
            Transform upperBack = transform.Find($"Back{upperBackIndex}");
            Transform lastBack = transform.Find($"Back{lastBackIndex}");
            lastBack.position = Vector3.Lerp(lastBack.position, upperBack.position, 1.5f * Time.deltaTime);
            if (lastBack.position == upperBack.position)
            {
                revealed = true;
            }
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
                piece.localScale = ((2 * width) - gapBetweenPieces) * Vector3.one;
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
                float gap = gapBetweenPieces / 2;
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
            CheckComplete();
            return true;
        }
        return false;
    }

    private void CheckComplete()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                completed = false;
                return;
            }
        }
        StartCoroutine(Countdown());
        completed = true;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1.6f);
        mainCamera.SetActive(true);
        slidingPuzzleCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerUI.SetActive(true);
        missionManager.UpdateMission(mission, 1);
    }
}
