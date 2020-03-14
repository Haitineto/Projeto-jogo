using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Keyboard : MonoBehaviour
{

    #region private properties
    public const int KeyboardRows = 7;

    public const int KeyboardColumns = 7;

    private System.Random _random = new System.Random();

    private bool _isPanning = false;

    private List<Runa> _selectedRunas = new List<Runa>();
    #endregion

    #region public properties
    public delegate void OnFinishSelectionEvent(List<Runa> selectedRunas);

    public delegate void AfterNewRunasAreCreatedEvent(List<Runa> newRunas);

    public OnFinishSelectionEvent OnFinishSelection;

    public AfterNewRunasAreCreatedEvent AfterNewRunasAreCreated;

    public GameObject RunaPrefab;

    public Runa[,] Runas = new Runa[KeyboardColumns, KeyboardRows];    
    #endregion

    #region private methods
    private Vector3 calculateRunaPosition(GameObject runa, int column, int row)
    {
        Bounds keyboardBounds = GetComponent<SpriteRenderer>().bounds;

        Bounds runaBounds = GetComponent<SpriteRenderer>().bounds;

        float widthOfEachRuna = keyboardBounds.size.x / KeyboardColumns;

        float heightOfEachRuna = keyboardBounds.size.y / KeyboardRows;

        float firstColumnPosition = keyboardBounds.min.x + (widthOfEachRuna / 2);

        float firstRowPosition = keyboardBounds.max.y - (heightOfEachRuna / 2);

        return new Vector3(firstColumnPosition + (column * widthOfEachRuna), firstRowPosition - (row * heightOfEachRuna), -1);
    }

    private Runa createRuna(int column, int row)
    {
        int quantityOfAvailibleElements = Enum.GetValues(typeof(EnumElement)).Length;

        EnumElement element = (EnumElement)_random.Next(0, quantityOfAvailibleElements);

        GameObject runaPrefab = Instantiate(RunaPrefab);

        Runa runa = runaPrefab.GetComponent<Runa>();

        runa.SetRunaElement(element);

        runa.SpotInKeyboard = new Vector2(column, row);

        setRunaPosition(runa, column, row, false);

        return runa;
    }

    private void onKeyDown()
    {
        _isPanning = true;
    }

    private void onKeyUp()
    {
        _isPanning = false;
        if (_selectedRunas.Count == 1)
            clearSelection();
        else if (_selectedRunas.Count > 0)
        {
            if (OnFinishSelection != null)
                OnFinishSelection(_selectedRunas);
            destroySelected();
            slideRunasToEmptySpaces();
            createSubstituteRunas();
        }
    }

    private void clearSelection()
    {
        for (int x = 0; x < KeyboardColumns; x++)
            for (int y = 0; y < KeyboardRows; y++)
                if (Runas[x, y] != null && Runas[x, y].Selected)
                    Runas[x, y].SetSelected(false);
        _selectedRunas.Clear();
    }

    private void destroySelected()
    {
        for (int x = 0; x < KeyboardColumns; x++)
            for (int y = 0; y < KeyboardRows; y++)
                if (Runas[x, y] != null && Runas[x, y].Selected)
                {
                    Destroy(Runas[x, y].gameObject);
                    Runas[x, y] = null;
                }
        _selectedRunas.Clear();
    }

    private void checkPanning()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isPanning)
            onKeyDown();
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            onKeyUp();
    }

    private Vector3 getMouseAdjustedPosition()
    {
        Vector3 mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseAdjustedPos = new Vector3(mouseCurrentPos.x, mouseCurrentPos.y, -1);
        return mouseAdjustedPos;
    }

    private void changeRunaSelection(Runa runa, bool selected)
    {
        if (selected)
        {
            _selectedRunas.Add(runa);
            runa.GetComponent<Runa>().SetSelected(true);
        }
        else
        {
            runa.SetSelected(false);
            _selectedRunas.Remove(runa);
        }
    }

    private void handleSelection()
    {
        if (!_isPanning) return;

        Vector3 mouseAdjustedPos = getMouseAdjustedPosition();

        for (int x = 0; x < KeyboardColumns; x++)
            for (int y = 0; y < KeyboardRows; y++)
                if (Runas[x, y] != null)
                {
                    Bounds runaBounds = Runas[x, y].GetComponent<MeshRenderer>().bounds;
                    Bounds mousePointerBounds = new Bounds(mouseAdjustedPos, new Vector3(0.01f, 0.01f, 0));
                    if (runaBounds.Intersects(mousePointerBounds) && canSelect(Runas[x, y]))
                        changeRunaSelection(Runas[x, y], true);
                    else if (runaBounds.Intersects(mousePointerBounds) && Runas[x, y].Selected && isLastButOne(Runas[x, y]))
                        changeRunaSelection(_selectedRunas[_selectedRunas.Count - 1], false);
                }
    }

    private bool isLastButOne(Runa runa)
    {
        if (_selectedRunas.Count >= 2 && _selectedRunas[_selectedRunas.Count - 2].GetInstanceID() == runa.GetInstanceID())
            return true;
        else
            return false;
    }

    private bool isAdjacentToLastSelected(Runa runa)
    {
        if (_selectedRunas.Count == 0) return false;

        Runa lastSelectedRuna = _selectedRunas[_selectedRunas.Count - 1];

        float distanceBetweenRows = Math.Abs(lastSelectedRuna.SpotInKeyboard.y) - Math.Abs(runa.SpotInKeyboard.y);

        float distanceBetweenColumns = Math.Abs(lastSelectedRuna.SpotInKeyboard.x) - Math.Abs(runa.SpotInKeyboard.x);

        if (Math.Abs(distanceBetweenRows) <= 1 && Math.Abs(distanceBetweenColumns) <= 1)
            return true;
        else
            return false;
    }

    private bool canSelect(Runa runa)
    {
        if (runa.Selected)
            return false;
        else if (_selectedRunas.Count > 0 && !_selectedRunas.Exists(x => x.Element == runa.Element))
            return false;
        else if (_selectedRunas.Count > 0 && !isAdjacentToLastSelected(runa))
            return false;
        else
            return true;
    }

    private Runa getRunaToExchange(int runaColumn, int runaRow)
    {
        for (int y = runaRow - 1; y >= 0; y--)
            if (Runas[runaColumn, y] != null)
                return Runas[runaColumn, y];

        return null;
    }

    private void slideRunasToEmptySpaces()
    {
        for (int x = 0; x < KeyboardColumns; x++)
            for (int y = KeyboardRows - 1; y >= 0; y--)
            {
                if (Runas[x, y] == null)
                {
                    Runa runaToExchange = getRunaToExchange(x, y);
                    if (runaToExchange != null)
                        exchangeRuna(x, y, (int)runaToExchange.SpotInKeyboard.x, (int)runaToExchange.SpotInKeyboard.y);
                }
            }
    }

    private void exchangeRuna(int columnRuna1, int rowRuna1, int columnRuna2, int rowRuna2)
    {
        Runa tempRuna = Runas[columnRuna1, rowRuna1];

        Runas[columnRuna1, rowRuna1] = Runas[columnRuna2, rowRuna2];

        Runas[columnRuna2, rowRuna2] = tempRuna;

        if (Runas[columnRuna1, rowRuna1] != null)
        {
            Runas[columnRuna1, rowRuna1].SpotInKeyboard = new Vector2(columnRuna1, rowRuna1);
            setRunaPosition(Runas[columnRuna1, rowRuna1], columnRuna1, rowRuna1, true);
        }

        if (Runas[columnRuna2, rowRuna2] != null)
        {
            Runas[columnRuna2, rowRuna2].SpotInKeyboard = new Vector2(columnRuna2, rowRuna2);
            setRunaPosition(Runas[columnRuna2, rowRuna2], columnRuna2, rowRuna2, true);
        }
    }

    private void setRunaPosition(Runa runa, int column, int row, bool animate)
    {
        if (animate)
            StartCoroutine(runa.MoveToPostion(calculateRunaPosition(runa.gameObject, column, row)));
        else
            runa.gameObject.transform.position = calculateRunaPosition(runa.gameObject, column, row);
    }

    private void createSubstituteRunas()
    {
        List<Runa> newRunas = new List<Runa>();
        for (int x = 0; x < KeyboardColumns; x++)
            for (int y = 0; y < KeyboardRows; y++)
                if (Runas[x, y] == null)
                {
                    Runas[x, y] = createRuna(x, y);
                    setRunaPosition(Runas[x, y], x, y - 2, false);
                    setRunaPosition(Runas[x, y], x, y, true);

                    newRunas.Add(Runas[x, y]);
                }
        if (AfterNewRunasAreCreated != null)
            AfterNewRunasAreCreated(newRunas);
    }
    #endregion

    #region public methods
    public void HandleUserGestures()
    {
        checkPanning();
        handleSelection();
    }

    public void BuildKeyboard()
    {
        for (int x = 0; x < KeyboardRows; x++)
            for (int y = 0; y < KeyboardColumns; y++)
            {
                if (Runas[x, y] != null)
                    Destroy(Runas[x, y].gameObject);
                Runas[x, y] = createRuna(x, y);
                setRunaPosition(Runas[x, y], x, y, false);
            }
    }

    public  EnumElement? CurrentSelectedElement()
    {
        if (!_selectedRunas.Any())
            return null;

        return _selectedRunas[0].Element;
    }
    #endregion
}
