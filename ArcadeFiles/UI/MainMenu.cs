using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using GXPEngine;
class MainMenu : GameObject, IControllable
{
    List<UIButton> uIButtons = new List<UIButton>();
    int SelectedIndex = 0;


    int actionDelay = 499;
    int timer = 0;


    public MainMenu() {
        SetXY(Game.main.width / 2, Game.main.height / 2);
        AddChild(new Controller(1));
        timer = actionDelay;
    }


    public void AddButton(UIButton button) {
        uIButtons.Add(button);
        if (uIButtons.Count == 1) {
            uIButtons[SelectedIndex].Select();
        }
        AddChild(button);
    }

    void Update() {
        timer -= Time.deltaTime;
    }

    public void PressJumpButton()
    {
        Console.WriteLine(SelectedIndex);
        uIButtons[SelectedIndex % uIButtons.Count].ActivateButton();
        //Console.WriteLine("JUmpButton");
        //SelectButton();
    }

    public void PressPowerUpButton()
    {
        uIButtons[SelectedIndex % uIButtons.Count].ActivateButton();
        Console.WriteLine("PowerUp");
        //SelectButton();
        //throw new NotImplementedException();
    }

    public void RotateWheel(bool pRight)
    {
        if(timer< 0)
        {
            if (pRight)
            {
                MoveInMenuUp();
            }
            else
            {
                MoveInMenuDown();
            }
        }
       
    }

    void MoveInMenuDown()
    {
        uIButtons[SelectedIndex % uIButtons.Count].Deselect();
        SelectedIndex++;
        uIButtons[SelectedIndex % uIButtons.Count].Select();
        timer = actionDelay;
    }

    void MoveInMenuUp()
    {
        uIButtons[SelectedIndex % uIButtons.Count].Deselect();
        SelectedIndex--;

        if (SelectedIndex < 0) {
            SelectedIndex = uIButtons.Count - 1;
            Console.WriteLine(SelectedIndex + "/" + uIButtons.Count);
        }
        uIButtons[SelectedIndex % uIButtons.Count].Select();
        timer = actionDelay;
    }


    
}
