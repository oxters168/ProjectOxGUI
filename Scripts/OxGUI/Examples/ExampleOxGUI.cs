using UnityEngine;
using OxGUI;

public class ExampleOxGUI : MonoBehaviour
{
    OxWindow window;
    OxTabbedPanel tabbedPanel;
    OxTextbox textbox;
    OxMenu menu;
    OxListFileSelector fs;
    OxButton button;
    OxCheckbox checkbox;
    OxScrollbar scrollbar;
    public bool tp_top, tp_bottom, tp_left, tp_right;
    public bool textbox_top, textbox_bottom, textbox_left, textbox_right;
    public bool menu_top, menu_bottom, menu_left, menu_right;
    public bool fs_top, fs_bottom, fs_left, fs_right;
    public bool button_top, button_bottom, button_left, button_right;
    public bool checkbox_top, checkbox_bottom, checkbox_left, checkbox_right;
    public bool scrollbar_top, scrollbar_bottom, scrollbar_left, scrollbar_right;

    void Start()
    {
        InitializeWindow();
        InitializeTabbedPanel();
        InitializeMenu();
        InitializeTextbox();
        
        InitializeFS();
        InitializeButton();
        InitializeCheckbox();
        InitializeScrollbar();
    }

    void OnGUI ()
    {
        SetTabbedPanelAnchor();
        SetMenuAnchor();
        SetFSAnchor();
        SetTextboxAnchor();
        SetButtonAnchor();
        SetCheckboxAnchor();
        SetScrollbarAnchor();
        window.Draw();
        //tabbedPanel.Draw();
    }

    private void InitializeTabbedPanel()
    {
        AppearanceInfo dimensions = window.CurrentAppearanceInfo();
        float tabbedPanelWidth = dimensions.centerWidth - 10, tabbedPanelHeight = dimensions.centerHeight - 10, tabbedPanelX = (Screen.width / 2f) - (tabbedPanelWidth / 2), tabbedPanelY = (Screen.height / 2f) - (tabbedPanelHeight / 2);
        tabbedPanel = new OxTabbedPanel(new Vector2(tabbedPanelX, tabbedPanelY), new Vector2(tabbedPanelWidth, tabbedPanelHeight));
        window.AddItems(tabbedPanel);
        //tabbedPanel.position = new Vector2(tabbedPanelX, tabbedPanelY);
    }
    private void SetTabbedPanelAnchor()
    {
        if (tp_top) tabbedPanel.anchor |= OxHelpers.Anchor.Top;
        else tabbedPanel.anchor &= ~OxHelpers.Anchor.Top;
        if (tp_bottom) tabbedPanel.anchor |= OxHelpers.Anchor.Bottom;
        else tabbedPanel.anchor &= ~OxHelpers.Anchor.Bottom;
        if (tp_left) tabbedPanel.anchor |= OxHelpers.Anchor.Left;
        else tabbedPanel.anchor &= ~OxHelpers.Anchor.Left;
        if (tp_right) tabbedPanel.anchor |= OxHelpers.Anchor.Right;
        else tabbedPanel.anchor &= ~OxHelpers.Anchor.Right;
    }

    private void InitializeTextbox()
    {
        int textboxWidth = 100, textboxHeight = 48, textboxX = (Screen.width / 2) - (textboxWidth / 2), textboxY = (Screen.height / 2) - (textboxHeight / 2);
        textbox = new OxTextbox(new Vector2(textboxX, textboxY), new Vector2(textboxWidth, textboxHeight));
        textbox.text = "Hello";
        OxPanel quickPanel = tabbedPanel.AddTab("Textbox");
        quickPanel.AddItems(textbox);
    }
    private void SetTextboxAnchor()
    {
        if (textbox_top) textbox.anchor |= OxHelpers.Anchor.Top;
        else textbox.anchor &= ~OxHelpers.Anchor.Top;
        if (textbox_bottom) textbox.anchor |= OxHelpers.Anchor.Bottom;
        else textbox.anchor &= ~OxHelpers.Anchor.Bottom;
        if (textbox_left) textbox.anchor |= OxHelpers.Anchor.Left;
        else textbox.anchor &= ~OxHelpers.Anchor.Left;
        if (textbox_right) textbox.anchor |= OxHelpers.Anchor.Right;
        else textbox.anchor &= ~OxHelpers.Anchor.Right;
    }

    private void InitializeMenu()
    {
        int menuWidth = 75, menuHeight = 250, menuX = (Screen.width / 2) - (menuWidth / 2), menuY = (Screen.height / 2) - (menuHeight / 2);
        menu = new OxMenu(new Vector2(menuX, menuY), new Vector2(menuWidth, menuHeight));
        for(int i = 0; i < 25; i++)
        {
            OxButton menuButton = new OxButton();
            menuButton.text = (i + 1).ToString();
            menu.AddItems(menuButton);
        }
        OxPanel quickPanel = tabbedPanel.AddTab("Menu");
        quickPanel.AddItems(menu);
    }
    private void SetMenuAnchor()
    {
        if (menu_top) menu.anchor |= OxHelpers.Anchor.Top;
        else menu.anchor &= ~OxHelpers.Anchor.Top;
        if (menu_bottom) menu.anchor |= OxHelpers.Anchor.Bottom;
        else menu.anchor &= ~OxHelpers.Anchor.Bottom;
        if (menu_left) menu.anchor |= OxHelpers.Anchor.Left;
        else menu.anchor &= ~OxHelpers.Anchor.Left;
        if (menu_right) menu.anchor |= OxHelpers.Anchor.Right;
        else menu.anchor &= ~OxHelpers.Anchor.Right;
    }

    private void InitializeFS()
    {
        int fsWidth = 75, fsHeight = 250, fsX = (Screen.width / 2) - (fsWidth / 2), fsY = (Screen.height / 2) - (fsHeight / 2);
        fs = new OxListFileSelector(new Vector2(fsX, fsY), new Vector2(fsWidth, fsHeight));
        OxPanel quickPanel = tabbedPanel.AddTab("FileSelector");
        quickPanel.AddItems(fs);
    }
    private void SetFSAnchor()
    {
        if (fs_top) fs.anchor |= OxHelpers.Anchor.Top;
        else fs.anchor &= ~OxHelpers.Anchor.Top;
        if (fs_bottom) fs.anchor |= OxHelpers.Anchor.Bottom;
        else fs.anchor &= ~OxHelpers.Anchor.Bottom;
        if (fs_left) fs.anchor |= OxHelpers.Anchor.Left;
        else fs.anchor &= ~OxHelpers.Anchor.Left;
        if (fs_right) fs.anchor |= OxHelpers.Anchor.Right;
        else fs.anchor &= ~OxHelpers.Anchor.Right;
    }

    private void InitializeButton()
    {
        int buttonWidth = 48, buttonHeight = 48, buttonX = (Screen.width / 2) - (buttonWidth / 2), buttonY = (Screen.height / 2) - (buttonHeight / 2);
        button = new OxButton(new Vector2(buttonX, buttonY), new Vector2(buttonWidth, buttonHeight));
        button.text = "Hello";
        OxPanel quickPanel = tabbedPanel.AddTab("Button");
        quickPanel.AddItems(button);
    }
    private void SetButtonAnchor()
    {
        if (button_top) button.anchor |= OxHelpers.Anchor.Top;
        else button.anchor &= ~OxHelpers.Anchor.Top;
        if (button_bottom) button.anchor |= OxHelpers.Anchor.Bottom;
        else button.anchor &= ~OxHelpers.Anchor.Bottom;
        if (button_left) button.anchor |= OxHelpers.Anchor.Left;
        else button.anchor &= ~OxHelpers.Anchor.Left;
        if (button_right) button.anchor |= OxHelpers.Anchor.Right;
        else button.anchor &= ~OxHelpers.Anchor.Right;
    }

    private void InitializeCheckbox()
    {
        int checkboxWidth = 150, checkboxHeight = 48, checkboxX = (Screen.width / 2) - (checkboxWidth / 2), checkboxY = (Screen.height / 2) - (checkboxHeight / 2);
        checkbox = new OxCheckbox(new Vector2(checkboxX, checkboxY), new Vector2(checkboxWidth, checkboxHeight));
        checkbox.text = "Toggle";
        OxPanel quickPanel = tabbedPanel.AddTab("Checkbox");
        quickPanel.AddItems(checkbox);
    }
    private void SetCheckboxAnchor()
    {
        if (checkbox_top) checkbox.anchor |= OxHelpers.Anchor.Top;
        else checkbox.anchor &= ~OxHelpers.Anchor.Top;
        if (checkbox_bottom) checkbox.anchor |= OxHelpers.Anchor.Bottom;
        else checkbox.anchor &= ~OxHelpers.Anchor.Bottom;
        if (checkbox_left) checkbox.anchor |= OxHelpers.Anchor.Left;
        else checkbox.anchor &= ~OxHelpers.Anchor.Left;
        if (checkbox_right) checkbox.anchor |= OxHelpers.Anchor.Right;
        else checkbox.anchor &= ~OxHelpers.Anchor.Right;
    }

    private void InitializeScrollbar()
    {
        int scrollbarWidth = 200, scrollbarHeight = 48, scrollbarX = (Screen.width / 2) - (scrollbarWidth / 2), scrollbarY = (Screen.height / 2) - (scrollbarHeight / 2);
        scrollbar = new OxScrollbar(new Vector2(scrollbarX, scrollbarY), new Vector2(scrollbarWidth, scrollbarHeight));
        OxPanel quickPanel = tabbedPanel.AddTab("Scrollbar");
        quickPanel.AddItems(scrollbar);
    }
    private void SetScrollbarAnchor()
    {
        if (scrollbar_top) scrollbar.anchor |= OxHelpers.Anchor.Top;
        else scrollbar.anchor &= ~OxHelpers.Anchor.Top;
        if (scrollbar_bottom) scrollbar.anchor |= OxHelpers.Anchor.Bottom;
        else scrollbar.anchor &= ~OxHelpers.Anchor.Bottom;
        if (scrollbar_left) scrollbar.anchor |= OxHelpers.Anchor.Left;
        else scrollbar.anchor &= ~OxHelpers.Anchor.Left;
        if (scrollbar_right) scrollbar.anchor |= OxHelpers.Anchor.Right;
        else scrollbar.anchor &= ~OxHelpers.Anchor.Right;
    }

    private void InitializeWindow()
    {
        int windowWidth = Screen.width - 50, windowHeight = Screen.height - 50, windowX = (Screen.width / 2) - (windowWidth / 2), windowY = (Screen.height / 2) - (windowHeight / 2);
        window = new OxWindow(new Vector2(windowX, windowY), new Vector2(windowWidth, windowHeight));
    }
}
