using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MineSweeper : MonoBehaviour, IPointerClickHandler
{
    //�ϐ��錾
    #region
    [SerializeField] private int _rows = 1;
    [SerializeField] private int _colums = 1;
    [SerializeField] private  int _mimeCount = 1;
    [SerializeField] private GridLayoutGroup _gridLayoutGroop = null;
    [SerializeField] private Cell _cellPrefab = null;
    [SerializeField] private GameObject _retryButton = null;
    [SerializeField] private GameObject _result = null;
    [SerializeField] private Text _mineCountText = null;
    [SerializeField] private Text _flagCountText = null;

    Cell[,] _cells;
    public int _openCount = 0;
    public int _clickCount = 0;
    public int _flagCount = 0;
    private float _time = 0;
    bool _isGameOver = false;
    #endregion

    private void Start()
    {
        _gridLayoutGroop.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroop.constraintCount = _colums;

        _mineCountText.text = string.Format($"X�̐�:{_mimeCount}");
        _flagCountText.text = string.Format($"���̐�:{_flagCount}");


        //�Z���𐶐�
        _cells = new Cell[_rows, _colums];
        var parent = _gridLayoutGroop.gameObject.transform;
        for(var r = 0; r < _rows; r++)
        {
            for(var c = 0; c < _colums; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                cell.DicR = r;
                cell.DicC = c;
                cell.Mine = this;
                _cells[r, c] = cell;
            }
        }

        //���e��ݒu
        for(var i = 0; i < _mimeCount; i++)
        {
            var r = Random.Range(0,_rows);
            var c = Random.Range(0,_colums);
            var cell = _cells[r, c];
            if(cell.CellState == CellState.Mine) { i--; }
            cell.CellState = CellState.Mine;
        }

        //����̔��e�𐔂���
        CountAroundMine();

    }

    private void Update()
    {
        _flagCountText.text = string.Format($"���̐�:{_flagCount}");
        if (_isGameOver) { return; }
        _time += Time.deltaTime;
    }

    bool TryCheckCell(int r, int c)
    {
        if(r < 0 || r >= _rows) { return false; }
        if(c < 0 || c >= _colums) { return false; }
        if (_cells[r, c].CellState == CellState.Mine) { return true; }
        return false;
    }

    public bool TryOpenAroundCell(int r, int c)
    {
        if (r < 0 || r >= _rows) { return false; }
        if (c < 0 || c >= _colums) { return false; }
        _cells[r, c].GetComponent<Cell>().OpenCell();
        return true;
    }

    //�N���b�N���ꂽ��
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_isGameOver) { return; }
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var left = PointerEventData.InputButton.Left;
        var right =PointerEventData.InputButton.Right;
        Debug.Log($"id={eventData.button}, left={left},right={right}");


        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                if (_cells[r, c].gameObject == cell)
                {
                    //Debug.Log("�Z�����N���b�N����");
                    var cellScript = cell.GetComponent<Cell>();

                    //���N���b�N�ŃI�[�v��
                    if(eventData.button == left && !cellScript.IsOpen)
                    {
                        _clickCount++;
                        if(cellScript.CellState == CellState.Mine) 
                        {
                            //1��ڂŔ��e���������甚�e�ł͂Ȃ��Z���Ɠ���ւ���
                            //���͂̔��e�𐔂��Ȃ���
                            if (_clickCount <= 1)
                            {
                                var swapCell = cellScript;
                                int row = 0;
                                int col = 0;
                                do
                                {
                                    row = Random.Range(0, _rows);
                                    col = Random.Range(0, _colums);
                                    swapCell = _cells[row, col];
                                }
                                while (swapCell.CellState == CellState.Mine);

                                _cells[row, col].CellState = CellState.Mine;
                                _cells[r, c].CellState = CellState.None;
                                CountAroundMine();
                                cellScript.OpenCell();
                                return;
                            }
                            //���e����������Q�[���I�[�o�[
                            GameOver();
                        }
                        cellScript.OpenCell();
                        Debug.Log($"open = {_openCount}");
                        if (_openCount == _rows * _colums - _mimeCount) { GameClear(); }
                    }
                    //�E�N���b�N�Ŋ���ݒuor����
                    else if(eventData.button == right)
                    {
                        cellScript.ChangeFlag();
                    }
                    break;
                }
            }
        }


    }

    //����̔��e�̐��𐔂���
    private void CountAroundMine()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                if (_cells[r, c].CellState != CellState.Mine)
                {
                    int mineNum = 0;
                    if (TryCheckCell(r + 1, c - 1)) { mineNum++; }
                    if (TryCheckCell(r + 1, c)) { mineNum++; }
                    if (TryCheckCell(r + 1, c + 1)) { mineNum++; }
                    if (TryCheckCell(r, c - 1)) { mineNum++; }
                    if (TryCheckCell(r, c + 1)) { mineNum++; }
                    if (TryCheckCell(r - 1, c - 1)) { mineNum++; }
                    if (TryCheckCell(r - 1, c)) { mineNum++; }
                    if (TryCheckCell(r - 1, c + 1)) { mineNum++; }
                    _cells[r, c].CellState = (CellState)mineNum;
                }

            }
        }

    }

    private void GameOver()
    {
        Debug.Log("�΂��͂�");
        _isGameOver = true;
        _retryButton.SetActive(true);
        for(var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                var cell = _cells[r, c];
                if (cell.CellState == CellState.Mine)
                {
                    cell.OpenCell();
                    cell.gameObject.GetComponent<Image>().color = Color.red;
                }
            }
        }
        _openCount = 0;
    }

    private void GameClear()
    {
        Debug.Log("�N���A�[");
        _isGameOver = true;
        _result.SetActive(true);
        _retryButton.SetActive(true);
        var time = _result.transform.Find("Time").GetComponent<Text>();
        var count = _result.transform.Find("Count").GetComponent<Text>();
        time.text = string.Format($"���ԁF{_time:f2}�b");
        count.text = string.Format($"�萔�F{_clickCount}��");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}