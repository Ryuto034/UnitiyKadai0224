using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour
{
    void Start()
    {
        //2�����i�������j�z��
        //�����̎�������Ȃ�z��ŁA�����C���f�b�N�X�ŗv�f���w�肷��
        //���ʃ}�b�v�Ȃǂ̊Ǘ��ł́A�c�Ɖ��̎����炠��̂�
        //���ڏ�ɕ��ׂ��f�[�^�̊Ǘ��ɂQ�����z�񂪓K���Ă���

        //  ## 2�����z��̕ϐ��錾
        //�z��^�錾��[]�����������ƂɃJ���}�@,�@�ŋ�؂�

        //***�v�f�^[,]�ϐ���***
        int[,] ary;

        //2�����z��̃C���X�^���X��
        // ***new �v�f�^[�v�f��,�v�f��]***
        ary = new int[2,3];

        //2�����z��ւ̃A�N�Z�X
        //�擪��0�Ƃ���ԍ��ŁA�������ƂɃC���f�b�N�X���w�肷��
        ary[0,0] = 10; ary[0, 1] = 20; ary[0, 2] = 30;
        ary[1,0] = 30; ary[1, 1] = 30; ary[1, 2] = 30;

        Debug.Log($"[0,0]={ary[0, 0]} [0,1]={ary[0, 1]} [0,2]={ary[0, 2]}");
        Debug.Log($"[1,0]={ary[1, 0]} [1,1]={ary[1, 1]} [0,2]={ary[1, 2]}");


        //�������z��̏������q
        //�z�񏉊����q�͎������� { } �����q�ɂ��đ��d���ł���
        var iAry = new int[2, 3] { { 10, 20, 30 }, { 20, 30, 100 } };
        //�������q���g���ꍇ�v�f���͏ȗ��\
        var iArya = new int[,] { { 10, 20, 30 }, { 20, 30, 100 } };


        //��z��
        //�v�f����0�̔z��͗L���B��̂Q�����z������l
        var nAry = new int[] { };

        for(var i = 0; i < nAry.Length; i++)
        {
            //���s����Ȃ����A�G���[�ɂ͂Ȃ�Ȃ�
            Debug.Log(nAry[i]);
        }

        //2�����z��̗v�f���̎擾
        //Length�v���p�e�B�́A�z��S�̗̂v�f��
        Debug.Log($"iAry.Length= {iAry.Length}");

        //�z��̎������̎擾
        Debug.Log($"iAry.Rank={iAry.Rank}");

        //�������̗v�f�����擾����ɂ�GetLength()���\�b�h���g��
        Debug.Log($"iAry.GetLength(0)={ iAry.GetLength(0)}");


        //���d���[�v�ň���v�f�����o��
        for(int n = 0; n<iAry.GetLength(0); n++)
        {
            for(int k = 0; k< iAry.GetLength(1); k++)
            {
                Debug.Log($"[{n},{k}] = {iAry[n, k]}");
            }

        }

        //�������z��ł�foreach�͎g����
        foreach(int i in iAry)
        {
            Debug.Log(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
