using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Background/List", order = 1)]
public class MyScriptableObject : ScriptableObject
{
    public string objectName = "New MyScriptableObject";
    public Sprite[] backgroundSprites;
    public bool isShuffle = false;
    public int floorNum = 1;
    public List<Background> backgrounds;

    public void Shuffle(Sprite[] deck)
    {
        for (int i = 1; i < deck.Length; i++)
        {
            Sprite temp = deck[i];
            int randomIndex = Random.Range(1, deck.Length);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
        isShuffle = true;
    }
    public void ShuffleList(List<Background> list)
    {
        for (int i = 1; i < list.Capacity; i++)
        {
            Background temp = list[i];
            int randomIndex = Random.Range(1, list.Capacity);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        isShuffle = true;
    }
}
