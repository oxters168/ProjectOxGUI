using UnityEngine;
using OxGUI2;

public class ExampleOxGUI2 : MonoBehaviour {

    OxButton button = new OxButton(Screen.width / 2, Screen.height / 2, 300, 300);
    public float buttonWidth = 300, buttonHeight = 300;
    public float centerWidth = 0.5f, centerHeight = 0.5f;

    void Start()
    {
        /*Texture2D topLeft = new Texture2D(4, 4);
        topLeft.SetPixels(new Color[] { Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red });
        topLeft.Apply();
        Texture2D top = new Texture2D(4, 4);
        top.SetPixels(new Color[] { Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green });
        top.Apply();
        Texture2D topRight = new Texture2D(4, 4);
        topRight.SetPixels(new Color[] { Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue });
        topRight.Apply();
        Texture2D left = new Texture2D(4, 4);
        left.SetPixels(new Color[] { Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue });
        left.Apply();
        Texture2D center = new Texture2D(4, 4);
        center.SetPixels(new Color[] { Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red });
        center.Apply();
        Texture2D right = new Texture2D(4, 4);
        right.SetPixels(new Color[] { Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green });
        right.Apply();
        Texture2D bottomLeft = new Texture2D(4, 4);
        bottomLeft.SetPixels(new Color[] { Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green });
        bottomLeft.Apply();
        Texture2D bottom = new Texture2D(4, 4);
        bottom.SetPixels(new Color[] { Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue });
        bottom.Apply();
        Texture2D bottomRight = new Texture2D(4, 4);
        bottomRight.SetPixels(new Color[] { Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red });
        bottomRight.Apply();*/
        button.AddAppearance(OxGUIHelpers.ElementState.normal, new Texture2D[] {
            Resources.Load<Texture2D>("Textures/BlueButton/BlueTopLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/BlueTop"),
            Resources.Load<Texture2D>("Textures/BlueButton/BlueTopRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/BlueLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/BlueCenter"),
            Resources.Load<Texture2D>("Textures/BlueButton/BlueRight"),
            Resources.Load<Texture2D>("Textures/BlueButton/BlueBottomLeft"),
            Resources.Load<Texture2D>("Textures/BlueButton/BlueBottom"),
            Resources.Load<Texture2D>("Textures/BlueButton/BlueBottomRight")
        });

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
}
