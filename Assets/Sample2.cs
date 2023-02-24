using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour
{
    void Start()
    {
        //2次元（多次元）配列
        //複数の次元からなる配列で、複数インデックスで要素を指定する
        //平面マップなどの管理では、縦と横の軸がらあるので
        //升目上に並べたデータの管理に２次元配列が適している

        //  ## 2次元配列の変数宣言
        //配列型宣言の[]内を次元ごとにカンマ　,　で区切る

        //***要素型[,]変数名***
        int[,] ary;

        //2次元配列のインスタンス化
        // ***new 要素型[要素数,要素数]***
        ary = new int[2,3];

        //2次元配列へのアクセス
        //先頭を0とする番号で、次元ごとにインデックスを指定する
        ary[0,0] = 10; ary[0, 1] = 20; ary[0, 2] = 30;
        ary[1,0] = 30; ary[1, 1] = 30; ary[1, 2] = 30;

        Debug.Log($"[0,0]={ary[0, 0]} [0,1]={ary[0, 1]} [0,2]={ary[0, 2]}");
        Debug.Log($"[1,0]={ary[1, 0]} [1,1]={ary[1, 1]} [0,2]={ary[1, 2]}");


        //多次元配列の初期化子
        //配列初期化子は次元毎に { } を入れ子にして多重化できる
        var iAry = new int[2, 3] { { 10, 20, 30 }, { 20, 30, 100 } };
        //初期化子を使う場合要素数は省略可能
        var iArya = new int[,] { { 10, 20, 30 }, { 20, 30, 100 } };


        //空配列
        //要素数が0の配列は有効。空の２次元配列も同様
        var nAry = new int[] { };

        for(var i = 0; i < nAry.Length; i++)
        {
            //実行されないが、エラーにはならない
            Debug.Log(nAry[i]);
        }

        //2次元配列の要素数の取得
        //Lengthプロパティは、配列全体の要素数
        Debug.Log($"iAry.Length= {iAry.Length}");

        //配列の次元数の取得
        Debug.Log($"iAry.Rank={iAry.Rank}");

        //次元毎の要素数を取得するにはGetLength()メソッドを使う
        Debug.Log($"iAry.GetLength(0)={ iAry.GetLength(0)}");


        //多重ループで一つずつ要素を取り出す
        for(int n = 0; n<iAry.GetLength(0); n++)
        {
            for(int k = 0; k< iAry.GetLength(1); k++)
            {
                Debug.Log($"[{n},{k}] = {iAry[n, k]}");
            }

        }

        //多次元配列でもforeachは使える
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
