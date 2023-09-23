using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuBox : MonoBehaviour
{
    public string title;
    public MenuBox parent;
    public int parentIndex;
    public float width;
    public float height;
    public int sortLayer;
    public int inactiveSortLayer;
    public bool active;
    public List<MenuItem> items;
    public GameObject textPrefab;
    public TextMeshPro titleText;
    public SpriteRenderer titleBack;
    private List<TextMeshPro> entries;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawMenu()
    {
        //set menu box size
        height = 0.5f + ((float)items.Count / 2);
        sprite.size = new Vector2 (width, height);
        if(active) sprite.sortingOrder = sortLayer + parentIndex;
        else sprite.sortingOrder = inactiveSortLayer + parentIndex;
        //clear entry list if applicable
        if(entries != null)
        {
            foreach(TextMeshPro text in entries)
            {
                GameObject.Destroy(text.gameObject);
            }
        }
        //create entries list
        entries = new List<TextMeshPro>();
        int index = 0;
        foreach(MenuItem item in items)
        {
            GameObject newText = GameObject.Instantiate(textPrefab, this.transform);
            TextMeshPro newTextAsset = newText.GetComponent<TextMeshPro>();
            newTextAsset.text = item.name;
            newTextAsset.rectTransform.sizeDelta = new Vector2(width - 0.5f, 0.5f);
            newText.transform.localPosition = new Vector2(0, (height/2f) - ((float)index/2) - 0.5f);
            entries.Add(newTextAsset);
            if(active) newTextAsset.sortingOrder = sortLayer + parentIndex + 1;
            else newTextAsset.sortingOrder = inactiveSortLayer + parentIndex + 1;
            index += 1;
        }
        //set title
        titleText.text = title;
        titleText.rectTransform.sizeDelta = new Vector2(width - 0.5f, 0.5f);
        titleText.transform.localPosition = new Vector2(-(width / 2) + 0.25f, (height / 2) + 0.375f);
        titleBack.size = new Vector2(width, 1);
        titleBack.transform.localPosition = new Vector2(0, (height / 2) + 0.375f);
        titleBack.sortingOrder = sprite.sortingOrder;
        if(title == string.Empty)
        {
            titleBack.size = Vector2.zero;
        }
        if(active) titleText.sortingOrder = sortLayer + parentIndex + 1;
        else titleText.sortingOrder = inactiveSortLayer + parentIndex + 1;
    }

    public Vector2 GetItemPos(int index)
    {
        index = Mathf.Clamp(index, 0, items.Count);
        return new Vector3(this.transform.position.x - (width / 2), entries[index].transform.position.y);
    }
}
