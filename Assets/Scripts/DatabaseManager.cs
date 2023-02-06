using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public PlayerMove playermove;

    static public DatabaseManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public string[] var_name;
    public string[] var;

    public string[] swtch_name;
    public bool[] swtch;

    public List<Item> item_list = new List<Item> ();
    // Start is called before the first frame update
    void Start()
    {
        //item_list.Add(new Item (playermove.haveShovel, Item.ItemType.Use));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
