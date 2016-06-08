using UnityEngine;
using OxGUI;

public class ExampleOxGUI : MonoBehaviour
{
    OxPanel panel;
    OxTextbox textbox;
    OxMenu menu;
    OxButton button;
    OxScrollbar scrollbar;
    public bool textbox_top, textbox_bottom, textbox_left, textbox_right;
    public bool menu_top, menu_bottom, menu_left, menu_right;
    public bool button_top, button_bottom, button_left, button_right;
    public bool scrollbar_top, scrollbar_bottom, scrollbar_left, scrollbar_right;

    void Start()
    {
        InitializePanel();
        InitializeTextbox();
        InitializeMenu();
        InitializeButton();
        InitializeScrollbar();
    }

    void OnGUI ()
    {
        SetMenuAnchor();
        SetTextboxAnchor();
        SetButtonAnchor();
        SetScrollbarAnchor();
        panel.Draw();
    }

    private void InitializeTextbox()
    {
        int textboxWidth = 100, textboxHeight = 48, textboxX = (Screen.width / 2) - (textboxWidth / 2), textboxY = (Screen.height / 2) - (textboxHeight / 2) - 150;
        textbox = new OxTextbox(new Vector2(textboxX, textboxY), new Vector2(textboxWidth, textboxHeight));
        textbox.text = "Hello";
        OxBase.ApplyAppearanceFromResources(textbox, "Textures/Checkbox", true, true, false);
        panel.AddItem(textbox);
    }
    private void SetTextboxAnchor()
    {
        if (textbox_top) textbox.anchor |= OxGUIHelpers.Anchor.Top;
        else textbox.anchor &= ~OxGUIHelpers.Anchor.Top;
        if (textbox_bottom) textbox.anchor |= OxGUIHelpers.Anchor.Bottom;
        else textbox.anchor &= ~OxGUIHelpers.Anchor.Bottom;
        if (textbox_left) textbox.anchor |= OxGUIHelpers.Anchor.Left;
        else textbox.anchor &= ~OxGUIHelpers.Anchor.Left;
        if (textbox_right) textbox.anchor |= OxGUIHelpers.Anchor.Right;
        else textbox.anchor &= ~OxGUIHelpers.Anchor.Right;
    }

    private void InitializeMenu()
    {
        int menuWidth = 150, menuHeight = 250, menuX = (Screen.width / 2) - (menuWidth / 2), menuY = (Screen.height / 2) - (menuHeight / 2);
        menu = new OxMenu(new Vector2(menuX, menuY), new Vector2(menuWidth, menuHeight));
        OxBase.ApplyAppearanceFromResources(menu, "Textures/GreyPanel");
        for(int i = 0; i < 25; i++)
        {
            OxButton menuButton = new OxButton();
            OxBase.ApplyAppearanceFromResources(menuButton, "Textures/BlueButton");
            menuButton.text = (i + 1).ToString();
            menu.AddItem(menuButton);
        }
        panel.AddItem(menu);
    }
    private void SetMenuAnchor()
    {
        if (menu_top) menu.anchor |= OxGUIHelpers.Anchor.Top;
        else menu.anchor &= ~OxGUIHelpers.Anchor.Top;
        if (menu_bottom) menu.anchor |= OxGUIHelpers.Anchor.Bottom;
        else menu.anchor &= ~OxGUIHelpers.Anchor.Bottom;
        if (menu_left) menu.anchor |= OxGUIHelpers.Anchor.Left;
        else menu.anchor &= ~OxGUIHelpers.Anchor.Left;
        if (menu_right) menu.anchor |= OxGUIHelpers.Anchor.Right;
        else menu.anchor &= ~OxGUIHelpers.Anchor.Right;
    }

    private void InitializeButton()
    {
        int buttonWidth = 48, buttonHeight = 48, buttonX = (Screen.width / 2) - (buttonWidth / 2), buttonY = (Screen.height / 2) - (buttonHeight / 2) - 200;
        button = new OxButton(new Vector2(buttonX, buttonY), new Vector2(buttonWidth, buttonHeight));
        button.text = "Hello";
        OxBase.ApplyAppearanceFromResources(button, "Textures/BlueButton");
        panel.AddItem(button);
    }
    private void SetButtonAnchor()
    {
        if (button_top) button.anchor |= OxGUIHelpers.Anchor.Top;
        else button.anchor &= ~OxGUIHelpers.Anchor.Top;
        if (button_bottom) button.anchor |= OxGUIHelpers.Anchor.Bottom;
        else button.anchor &= ~OxGUIHelpers.Anchor.Bottom;
        if (button_left) button.anchor |= OxGUIHelpers.Anchor.Left;
        else button.anchor &= ~OxGUIHelpers.Anchor.Left;
        if (button_right) button.anchor |= OxGUIHelpers.Anchor.Right;
        else button.anchor &= ~OxGUIHelpers.Anchor.Right;
    }

    private void InitializeScrollbar()
    {
        int scrollbarWidth = 200, scrollbarHeight = 48, scrollbarX = (Screen.width / 2) - (scrollbarWidth / 2), scrollbarY = (Screen.height / 2) - (scrollbarHeight / 2) + 200;
        scrollbar = new OxScrollbar(new Vector2(scrollbarX, scrollbarY), new Vector2(scrollbarWidth, scrollbarHeight));
        panel.AddItem(scrollbar);
    }
    private void SetScrollbarAnchor()
    {
        if (scrollbar_top) scrollbar.anchor |= OxGUIHelpers.Anchor.Top;
        else scrollbar.anchor &= ~OxGUIHelpers.Anchor.Top;
        if (scrollbar_bottom) scrollbar.anchor |= OxGUIHelpers.Anchor.Bottom;
        else scrollbar.anchor &= ~OxGUIHelpers.Anchor.Bottom;
        if (scrollbar_left) scrollbar.anchor |= OxGUIHelpers.Anchor.Left;
        else scrollbar.anchor &= ~OxGUIHelpers.Anchor.Left;
        if (scrollbar_right) scrollbar.anchor |= OxGUIHelpers.Anchor.Right;
        else scrollbar.anchor &= ~OxGUIHelpers.Anchor.Right;
    }

    private void InitializePanel()
    {
        int panelWidth = 300, panelHeight = 500, panelX = (Screen.width / 2) - (panelWidth / 2), panelY = (Screen.height / 2) - (panelHeight / 2);
        panel = new OxPanel(new Vector2(panelX, panelY), new Vector2(panelWidth, panelHeight));
        OxBase.ApplyAppearanceFromResources(panel, "Textures/GreyPanel");
    }

    /*private void TextureBlue(OxBase element)
    {
        element.AddAppearance(OxGUIHelpers.ElementState.Normal, new Texture2D[] {
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

        element.AddAppearance(OxGUIHelpers.ElementState.Highlighted, new Texture2D[] {
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

        element.AddAppearance(OxGUIHelpers.ElementState.Down, new Texture2D[] {
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
    private void TextureGrey(OxBase elment)
    {
        elment.AddAppearance(OxGUIHelpers.ElementState.Normal, new Texture2D[] {
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
    }*/
}
