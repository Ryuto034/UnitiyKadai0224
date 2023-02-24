using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None = 0,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,

    Mine = -1,
}


public class Cell : MonoBehaviour
{
    [SerializeField]
    private Text _view = null;

    [SerializeField]
    private CellState _cellState;

    private MineSweeper _mine;
    public MineSweeper Mine
    {
        set => _mine = value;
    }

    private int _dic_r;
    private int _dic_c;

    private bool _isOpen = false;

    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    public bool IsOpen
    {
        get => _isOpen;
    }

    public int DicR
    {
        get => _dic_r;
        set => _dic_r = value;
    }
    public int DicC
    {
        get => _dic_c;
        set => _dic_c = value;
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    private void OnCellStateChanged()
    {
        if(_view == null) return;

        if (_cellState == CellState.None)
        {
            _view.text = "";
        }
        else if(_cellState == CellState.Mine)
        {
            _view.text = "X";
            _view.color = Color.black;
        }
        else
        {
            _view.text = ((int)_cellState).ToString();
            switch (_cellState)
            {
                case CellState.One:
                    _view.color = Color.cyan;
                    break;
                case CellState.Two:
                    _view.color = new Color(0,0.8f,0);
                    break;
                case CellState.Three:
                    _view.color = Color.red;
                    break;
                case CellState.Four:
                    _view.color = Color.blue;
                    break;
                default:
                    _view.color = Color.magenta;
                    break;

            }
            if(_cellState == CellState.One)
            {
                _view.color = Color.blue;
            }
        }
    }

    public void OpenCell()
    {
        if (_isOpen) return;
        _mine._openCount++;
        var hide = transform.Find("Hide");
        var flag = hide.transform.Find("Flag");
        if(flag.gameObject.activeSelf == true && _cellState != CellState.Mine)
        {
            //Debug.Log("flag‚ð—§‚Ä‚½ƒZƒ‹‚ªŠJ‚©‚ê‚½");
            ChangeFlag();
        }
        hide.gameObject.SetActive(false);
        _isOpen = true;

        if (_cellState == CellState.None)
        {
            var r = _dic_r;
            var c = _dic_c;

            _mine.TryOpenAroundCell(r - 1, c - 1);
            _mine.TryOpenAroundCell(r - 1, c);
            _mine.TryOpenAroundCell(r - 1, c + 1);
            _mine.TryOpenAroundCell(r, c - 1);
            _mine.TryOpenAroundCell(r, c + 1);
            _mine.TryOpenAroundCell(r + 1, c - 1);
            _mine.TryOpenAroundCell(r + 1, c);
            _mine.TryOpenAroundCell(r + 1, c + 1);

        }
    }

    public void ChangeFlag()
    {
        if (_isOpen) return;
        var flag = transform.Find("Hide").Find("Flag").gameObject;
        flag.SetActive(!flag.activeSelf);
        _mine._flagCount += flag.activeSelf ? 1 : -1;

    }
}
