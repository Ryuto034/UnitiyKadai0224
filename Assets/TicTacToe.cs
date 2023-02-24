using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TicTacToe : MonoBehaviour
{
    private const int Size = 3;
    private GameObject[,] _cells;
    private Image[,] _images;

    [SerializeField] private Color _normalCell = Color.white;
    [SerializeField] private Color _selectedCell = Color.cyan;


    private int _selectedRow;
    private int _selectedColumn;

    [SerializeField] private Sprite _circle = null;
    [SerializeField] private Sprite _cross = null;

    bool _isCircleTurn = true; //ÅZÇÃéËî‘Ç»ÇÁtrue
    bool _gameEnd = false;  //åàíÖÇ™Ç¬Ç¢ÇƒÇ¢ÇΩÇÁtrue

    void Start()
    {
        _cells = new GameObject[Size, Size];
        _images = new Image[Size, Size];
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = new GameObject($"Cell({r}),{c}");
                cell.transform.parent = transform;
                var image = cell.AddComponent<Image>();
                _cells[r, c] = cell;
                _images[r, c] = image;
            }
        }
    }

    void Update()
    {
        if (_gameEnd)
        {
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) { _selectedColumn--; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { _selectedColumn++; }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { _selectedRow--; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { _selectedRow++; }

        if (_selectedColumn < 0) { _selectedColumn = 0; }
        if (_selectedColumn >= Size) { _selectedColumn = Size - 1; }
        if (_selectedRow < 0) { _selectedRow = 0; }
        if (_selectedRow >= Size) { _selectedRow = Size - 1; }

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var image = _images[r, c];
                image.color =
                    (r == _selectedRow && c == _selectedColumn)
                    ? _selectedCell : _normalCell;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var image = _images[_selectedRow, _selectedColumn];

            if(image.sprite != _circle && image.sprite != _cross)
            {
                if (_isCircleTurn) { image.sprite = _circle; }
                else { image.sprite = _cross; }

                _isCircleTurn = !_isCircleTurn;
            }
            if (Judge())
            {
                //Debug.Log("SPACEÇ≈çƒäJ");
                _gameEnd = true;
            } 
        }

    }
    bool Judge()
    {

            Image[] imagesH = new Image[3]; //â°
            Image[] imagesV = new Image[3]; //èc
            Image[] imagesD1 = new Image[3]; //éŒÇﬂÇP
            Image[] imagesD2 = new Image[3]; //éŒÇﬂÇQ

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for(var c = 0; c < _cells.GetLength(1); c++)
            {
                imagesH[c] = _images[r, c];
                imagesV[c] = _images[c, r];
            }
            if (winnerCheck(imagesH))return true;
            if (winnerCheck(imagesV))return true;

            imagesD1[r] = _images[r, r];
            imagesD2[r] = _images[r, _images.GetLength(1)-1 - r];
        }
        if(winnerCheck(imagesD1))return true;
        if(winnerCheck(imagesD2))return true;


        //ãÛîíÇÃÉuÉçÉbÉNÇ™écÇ¡ÇƒÇ¢ÇÈÇ©ÅH
        foreach (var image in _images)
        {
            var sprite = image.sprite;
            if (sprite != _circle && sprite != _cross) return false;
        }

        Debug.Log("à¯Ç´ï™ÇØ");
        return true;

    }

    bool winnerCheck(Image[] images)
    {

        if (images[0].sprite != _circle && images[0].sprite != _cross) return false;
        for(var i = 0; i < images.Length-1; i++)
        {
            if (images[i].sprite != images[i+1].sprite) return false;
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = _selectedCell;
        }

        if (images[0].sprite == _circle) Debug.Log("ÅZÇÃèüÇø");
        else Debug.Log("Å~ÇÃèüÇø");


        return true;
    }
}

