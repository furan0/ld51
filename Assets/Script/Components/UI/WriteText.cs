using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;
using UnityEngine.Events;

public class WriteText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float timeBetweenText = 1.5f;
    [SerializeField] float timeBetweenCharacters = 0.2f;
    private string currentText;
    private bool isWriting = false;
    private PsycheDatabase db;

    public UnityEvent characterWritten;

    void Awake() {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();
        Assert.IsNotNull(text);
    }
    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>()?.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWriting && isActiveAndEnabled) {
            writeRandomText();
        }
    }

    public void writeRandomText() {
        if (db == null) {
            Debug.LogWarning("No text DB found");
            return;
        }

        writeText(db.getPsychiatristBabble());
    }

    public void writeText(string text) {
        currentText = text;
        isWriting = true;
        StartCoroutine(textWriter());
    }

    public void writeText(int stringId) {
        if (db == null) {
            Debug.LogWarning("No text DB found");
            return;
        }

        writeText(db.getPsychiatristBabble(stringId));
    }

    public IEnumerator textWriter() {
        text.text = "";
        bool specialText = false;
        foreach (var character in currentText)
        {
            //Write all special char at once
            if (character.Equals('<')) {
                specialText = true;
            }
            if (specialText) {
                text.text += character;
                if (character.Equals('>')) {
                    specialText = false;
                }
                continue;
            }

            yield return new WaitForSeconds(timeBetweenCharacters);
            text.text += character;
            characterWritten?.Invoke();
        }
        
        yield return new WaitForSeconds(timeBetweenText);
        isWriting = false;
    }
}
