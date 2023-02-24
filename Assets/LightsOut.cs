using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LightsOut : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int Row = 5;
    [SerializeField] int Col = 5;

    private int _clickCount = 0;
    private float _clearTime = 0;
    private bool _cleared = false;

    GameObject[,] _cells; 

    private void Start()
    {
        var grid = GetComponent<GridLayoutGroup>();
        grid.constraintCount = Row;
        _cells = new GameObject[Row, Col];
        var _colors = new Color[Col-1];
        for (var r = 0; r < Row; r++)
        {
            for(var c = 0; c < Col; c++)
            {
                var cell = new GameObject($"Cell{r},{c}");
                cell.transform.parent = transform;
                var image = cell.AddComponent<Image>();
                //ランダム生成(未完成)
                int num = Random.Range(0,2);
                image.color = num == 0 ? Color.black : Color.white;
                _colors[_colors.Length-1] = image.color;
                _cells[r, c] = cell;
            }
        }
    }

    private void Update()
    {
        if (_cleared)
        {
            if (Input.GetKeyDown(KeyCode.Return)) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            return;
        }
        if (ClearCheck())
        {
            Debug.Log($"クリア　タイム:{$"{_clearTime:F2}"}秒、手数:{_clickCount}");
            _cleared = true;
        }
        else _clearTime += Time.deltaTime;


        //デバッグ用　エンターキーで全て黒になる
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (var r = 0; r < Row; r++)
            {
                for (var c = 0; c < Col; c++)
                {
                    _cells[r, c].GetComponent<Image>().color = Color.black;   
                }
            }

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cleared) { return; }
        Debug.Log("Click");
        var cell = eventData.pointerCurrentRaycast.gameObject;
        for (var r = 0; r < Row; r++)
        {
            for (var c = 0; c < Col; c++)
            {
                if (_cells[r,c] == cell)
                {
                    TryChangeColor(r, c);
                    TryChangeColor(r+1, c);
                    TryChangeColor(r-1, c);
                    TryChangeColor(r, c+1);
                    TryChangeColor(r, c-1);
                    _clickCount++;
                    break;
                }
            }
        }


    }

    private void TryChangeColor(int r, int c)
    {
        if (r < 0 || r >= Row || c < 0 || c >= Col) return;
        var image = _cells[r,c].GetComponent<Image>();
        if(image != null)
        {
            image.color = image.color == Color.black ? Color.white : Color.black;
        }
    }

    private bool ClearCheck()
    {
        var color = _cells[0,0].GetComponent<Image>().color;
        for(var r = 0; r < Row; r++)
        {
            for(var c = 0; c < Col; c++)
            {
                if (_cells[r, c].GetComponent<Image>().color != color) return false;
            }
        }
        return true;
    }
}
