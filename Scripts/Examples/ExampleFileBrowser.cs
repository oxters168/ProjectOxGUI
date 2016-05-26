using UnityEngine;
using OxGUI;

public class ExampleFileBrowser : MonoBehaviour {

    OxChooser fileChooser = new OxChooser();

	// Use this for initialization
	void Start ()
    {
        fileChooser.FillBrowserList("C:/", true);
        //fileChooser.clicked += FileChooser_clicked;
	}

    private void FileChooser_clicked(OxGUI.OxGUI sender)
    {
        Debug.Log("Clicked");
    }

    // Update is called once per frame
    void Update ()
    {
        fileChooser.Resize(Screen.width * 0.25f, Screen.height * 0.5f);
        fileChooser.Reposition((Screen.width / 2f) - (fileChooser.Size().x / 2f), (Screen.height / 2f) - (fileChooser.Size().y / 2f));
    }

    void OnGUI()
    {
        fileChooser.Draw();
    }
}
