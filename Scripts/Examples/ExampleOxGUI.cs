using UnityEngine;
using OxGUI;

public class ExampleOxGUI : MonoBehaviour {

    OxPanel panel = new OxPanel(Screen.width / 2, Screen.height / 2, 300, 500);
    public float panelWidth = 300, panelHeight = 500;
    OxButton button = new OxButton(Screen.width / 2, Screen.height / 2, 48, 48);
    public float buttonWidth = 48, buttonHeight = 48;

    void Start()
    {
        AddTexturesToPanel();
        AddTexturesToButton();
    }

    void OnGUI ()
    {
        if (panel.width != panelWidth || panel.height != panelHeight)
            panel.size = new Vector2(panelWidth, panelHeight);
        panel.Draw();

        if(button.size.x != buttonWidth || button.size.y != buttonHeight)
            button.size = new Vector2(buttonWidth, buttonHeight);
        button.Draw();
    }

    private void AddTexturesToPanel()
    {
        panel.AddAppearance(OxGUIHelpers.ElementState.Normal, new Texture2D[] {
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayTopLeft"),
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayTop"),
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayTopRight"),
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayLeft"),
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayCenter"),
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayRight"),
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayBottomLeft"),
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayBottom"),
            Resources.Load<Texture2D>("Textures/GrayPanel/Normal/GrayBottomRight")
        });
    }
    private void AddTexturesToButton()
    {
        button.AddAppearance(OxGUIHelpers.ElementState.Normal, new Texture2D[] {
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueTopLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueTop"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueTopRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueCenter"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueBottomLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueBottom"),
            Resources.Load<Texture2D>("Textures/BlueButton/Normal/BlueBottomRight")
        });

        button.AddAppearance(OxGUIHelpers.ElementState.Highlighted, new Texture2D[] {
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueTopLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueTop"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueTopRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueCenter"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueBottomLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueBottom"),
            Resources.Load<Texture2D>("Textures/BlueButton/Over/BlueBottomRight")
        });

        button.AddAppearance(OxGUIHelpers.ElementState.Down, new Texture2D[] {
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueTopLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueTop"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueTopRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueCenter"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueBottomLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueBottom"),
            Resources.Load<Texture2D>("Textures/BlueButton/Down/BlueBottomRight")
        });
    }
}
