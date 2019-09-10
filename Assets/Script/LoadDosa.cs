using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDosa : MonoBehaviour
{

    public List<Dosa> dosas = new List<Dosa>();
    public TextAsset Dosa;
    // Start is called before the first frame update
    void Start()
    {
        Dosa = Resources.Load<TextAsset>("ListDosa");
        string[] data = Dosa.text.Split('\n');
        for (int i = 1; i < data.Length; i++) {
            if(data[i] != "") {
                string[] row = data[i].Split(';');
                Dosa d = new Dosa();
                d.NamaDosa = row[0];
                int.TryParse(row[1], out d.Rating);

                dosas.Add(d);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
