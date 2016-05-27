using UnityEngine;
using OxGUI2;

public class ExampleOxGUI2 : MonoBehaviour {

    OxButton button = new OxButton(Screen.width / 2, Screen.height / 2, 300, 300);
    public float buttonWidth = 300, buttonHeight = 300;
    public float centerWidth = 0.5f, centerHeight = 0.5f;

    void Start()
    {
        AddTexturesToButton();

        button.resized += Button_resized;

        centerWidth = button.centerPercentWidth;
        centerHeight = button.centerPercentHeight;
    }

    private void Button_resized(object obj, Vector2 delta)
    {
        centerWidth = button.centerPercentWidth;
        centerHeight = button.centerPercentHeight;
    }

    void OnGUI ()
    {
        if(button.size.x != buttonWidth || button.size.y != buttonHeight)
            button.size = new Vector2(buttonWidth, buttonHeight);
        button.centerPercentWidth = centerWidth;
        button.centerPercentHeight = centerHeight;
        button.Draw();
    }

    private void AddTexturesToButton()
    {
        button.AddAppearance(OxGUIHelpers.ElementState.normal, new Texture2D[] {
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

        button.AddAppearance(OxGUIHelpers.ElementState.highlighted, new Texture2D[] {
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

        button.AddAppearance(OxGUIHelpers.ElementState.down, new Texture2D[] {
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
