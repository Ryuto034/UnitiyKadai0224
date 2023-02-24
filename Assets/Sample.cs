using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    int index_ = 0;
    GameObject[] items_;
    [SerializeField]
    int elements_ = 0;
    // Start is called before the first frame update
    void Start()
    {
        items_ = new GameObject[elements_];
        for(var i = 0; i < items_.Length; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;

            var image = obj.AddComponent <Image>();
            if(i == 0) image.color = Color.red;
            else image.color = Color.white;
            items_[i] = obj;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index_--;
            if (index_ < 0) index_ = elements_ - 1;
            //for (var i = index_;  i < 0; i--)
            //{
            //    if (items_[i] != null)
            //    {
            //        index_ = i;
            //        break;
            //    }
            //}
            //if (index_ < 0) index_ = elements_-1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index_++;
            if (index_ >= elements_) index_ = 0;
            //for (var i = index_; i>= elements_; i++)
            //{
            //    if (items_[i] != null)
            //    {
            //        index_ = i;
            //        break;
            //    }
            //}

        }

        //if (index_ < 0 || index_ >= items_.Length) index_ %= items_.Length;

        for (var i = 0; i < elements_; i++)
        {
            if (items_[i] == null) continue;
            var image = items_[i].GetComponent<Image>();
            if(i == index_)
            {
                image.color = Color.red;
            }
            else
            {
                image.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && items_[index_] != null)
        {
            Destroy(items_[index_]);
            index_++;
            if (index_ >= elements_) index_ = 0;


        }

        Debug.Log($"index_ = {index_}");
    }

    //void UpIndex()
    //{
    //    index_++;
    //    if(index_ >= elements_) index_ = 0;
    //    return;
    //}

    //void DownIndex()
    //{
    //    if(index_ < 0) index_ = elements_-1;
    //    for (var i = index_; i < -1; i--)
    //    {
    //        if (i == -1) index_ = elements_ - 1;
    //        if (items_[i] == null) continue;
            
    //    }

    //    return;
    //}


}
