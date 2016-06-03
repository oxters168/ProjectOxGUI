using UnityEngine;
using OxGUI;

public class ExampleOxGUI : MonoBehaviour
{
    OxPanel panel;
    OxButton button;
    public bool top, bottom, left, right;

    void Start()
    {
        InitializeButton();
        InitializePanel();
    }

    void OnGUI ()
    {
        SetButtonAnchor();
        panel.Draw();
    }

    private void InitializeButton()
    {
        int buttonWidth = 48, buttonHeight = 48, buttonX = (Screen.width / 2) - (buttonWidth / 2), buttonY = (Screen.height / 2) - (buttonHeight / 2);
        button = new OxButton(buttonX, buttonY, buttonWidth, buttonHeight);
        AddTexturesToButton();
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
    private void SetButtonAnchor()
    {
        if (top) button.anchor |= OxGUIHelpers.Anchor.Top;
        else button.anchor &= ~OxGUIHelpers.Anchor.Top;
        if (bottom) button.anchor |= OxGUIHelpers.Anchor.Bottom;
        else button.anchor &= ~OxGUIHelpers.Anchor.Bottom;
        if (left) button.anchor |= OxGUIHelpers.Anchor.Left;
        else button.anchor &= ~OxGUIHelpers.Anchor.Left;
        if (right) button.anchor |= OxGUIHelpers.Anchor.Right;
        else button.anchor &= ~OxGUIHelpers.Anchor.Right;
    }

    private void InitializePanel()
    {
        int panelWidth = 300, panelHeight = 500, panelX = (Screen.width / 2) - (panelWidth / 2), panelY = (Screen.height / 2) - (panelHeight / 2);
        panel = new OxPanel(panelX, panelY, panelWidth, panelHeight);
        panel.AddItem(button);
        AddTexturesToPanel();
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
}
