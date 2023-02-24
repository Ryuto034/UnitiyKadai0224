using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour
{
    int index_x_ = 0;
    int index_y_ = 0;
    Image[,] ary;
    bool[,] aryArive;
    [SerializeField] int row = 0;
    [SerializeField] int col = 0;
    private void Start()
    {
        ary = new Image[row, col];
        aryArive = new bool[row, col];

        for (var r = 0; r < row; r++)
        {
            for (var c = 0; c < col; c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                if (r == 0 && c == 0) { image.color = Color.red; }
                else { image.color = Color.white; }

                ary[r, c] = image;
                aryArive[r, c] = true;

            }
        }

        this.GetComponent<GridLayoutGroup>().constraintCount = col;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // ���L�[��������
        {
            //Destroy����Ă��Ȃ��u���b�N�܂ŃX�L�b�v����
            int count = 0;
            do
            {
                index_x_--;
                count++;
                if (index_x_ < 0) index_x_ = col - 1;
                if (count > col)
                {
                    CheckAliveCell();   //�Ȃ������獶�ォ��Destroy����Ă��Ȃ��u���b�N��T���Ĉړ�����
                    break;
                }

            }
            while (!aryArive[index_y_, index_x_] && count <= col);
            {
                if (index_x_ < 0) index_x_ = col - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // �E�L�[��������
        {            
            int count = 0;
            
            do
            {
                index_x_++;
                count++;
                if (index_x_ >= col) index_x_ = 0;
                if (count > col)
                {
                    CheckAliveCell();
                    break;
                }
            }
            while (!aryArive[index_y_, index_x_] && count <= col);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // ��L�[��������
        {
            int count = 0;
            do
            {
                index_y_--;
                count++;
                if (index_y_ < 0) index_y_ = row - 1;
                if (count > row)
                {
                    CheckAliveCell();
                    break;
                }
            }
            while (!aryArive[index_y_, index_x_] && count <= row);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // ���L�[��������
        {
            int count = 0;
            do
            {
                index_y_++;
                count++;
                if (index_y_ >= row) index_y_ = 0;
                if (count > row)
                {
                    CheckAliveCell();
                    break;
                }
            }
            while (!aryArive[index_y_, index_x_] && count <= row);
        }

        //�Z���̐F��ύX
        for(int y = 0; y < row; y++)
        {
            for(int x = 0; x < col; x++)
            {
                if (!aryArive[y,x]) continue;
                var image = ary[y, x].GetComponent<Image>();
                if (y == index_y_ && x == index_x_)
                {
                    image.color = Color.red;
                }
                else image.color = Color.white;
            }
        }

        //�Z�����폜
        if (Input.GetKeyDown(KeyCode.Space) && aryArive[index_y_,index_x_])
        {
            aryArive[index_y_, index_x_] = false;
            Destroy(ary[index_y_, index_x_]);
        }

    }

    bool CheckAliveCell()
    {
        for(var c = 0; c < col; c++)
        {
            for(var r = 0; r < row; r++)
            {
                if (aryArive[r, c])
                {
                    index_y_ = r;
                    index_x_ = c;
                    return true;
                }
            }
        }

        //�S��Destroy����Ă����烊�X�^�[�g
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        return false;

    }

}