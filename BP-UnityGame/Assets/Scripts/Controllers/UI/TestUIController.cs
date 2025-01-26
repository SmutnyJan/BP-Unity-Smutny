using System;
using TMPro;
using UnityEngine;

public class TestUIController : MonoBehaviour
{
    public TextMeshProUGUI DateText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DateText.text = "Datum: " + DateTime.Now.ToString("dd. MM. yyyy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
