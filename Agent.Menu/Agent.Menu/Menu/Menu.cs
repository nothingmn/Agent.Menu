using System;
using System.Collections;
using Agent.Menu;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.Menu.Menu
{
    public class Menu
    {
        public static int AgentSize = 128;

        public delegate void MenuItemClicked(Menu menu, MenuItem menuItem, DateTime time);

        public event MenuItemClicked OnMenuItemClicked;
        private Bitmap _screen = null;
        private ButtonHelper _buttonHelper = null;
        public int SelectedIndex { get; set; }
        public ArrayList Items { get; set; }
        private int _maxVisible = 0;

        public Menu(Bitmap screen = null, ButtonHelper buttonHelper = null)
        {
            _screen = screen;
            if (_screen == null) _screen = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);
            _buttonHelper = buttonHelper;
            if (_buttonHelper == null)
                _buttonHelper = new ButtonHelper((Cpu.Pin) Buttons.Top, (Cpu.Pin) Buttons.Middle,
                                                 (Cpu.Pin) Buttons.Bottom);
            SelectedIndex = 0;
            Items = new System.Collections.ArrayList();

            _buttonHelper.OnButtonPress += buttonHelper_OnButtonPress;

            _maxVisible = AgentSize/menuFont.Height;
        }

        void buttonHelper_OnButtonPress(Buttons button, InterruptPort port, ButtonDirection direction, DateTime time)
        {
            if (direction == ButtonDirection.Up)
            {
                if (button == Buttons.Top) SelectedIndex--;
                if (button == Buttons.Bottom) SelectedIndex++;
                if (SelectedIndex < 0) SelectedIndex = 0;
                if (SelectedIndex >= Items.Count) SelectedIndex = Items.Count-1;
                
                if (button == Buttons.Middle)
                {
                    MenuItem item = (Items[SelectedIndex] as MenuItem);
                    if (OnMenuItemClicked != null) OnMenuItemClicked(this, item, time);
                }
                Debug.Print(SelectedIndex.ToString());
                Render();
            }
        }

        public void Render()
        {
            _screen.Clear();

            //border
            _screen.DrawRectangle(Color.White, 1, 0, 0, AgentSize, AgentSize, 0, 0, Color.Black, 0, 0, Color.Black, 0, 0,
                                  255);

            int first = 0;
            int last = Items.Count-1;
            if ((last - first) > _maxVisible)
            {
                first = SelectedIndex - (_maxVisible/2);
                if (first < 0)
                {
                    first = 0;
                }

                last = first +  _maxVisible - 1;
                if (last > Items.Count - 1) last = Items.Count - 1;

            }

            int top = 0;
            int height = menuFont.Height;

            for (int index = first; index <= last; index++)
            {
                MenuItem item = (Items[index] as MenuItem);
                item.Selected = false;
                if (index == SelectedIndex) item.Selected = true;

                Color backColor = Color.Black;
                Color textColor = Color.White;
                if (item.Selected)
                {
                    backColor = Color.White;
                    textColor = Color.Black;
                }
                _screen.DrawRectangle(backColor, 1, 1, top, Menu.AgentSize - 2, height, 0, 0, backColor, 0, 0, backColor,
                                      0,
                                      0, 255);

                _screen.DrawText(item.Title, MenuFont, textColor, 2, top + 1);
                top += height + 1;
            }
            _screen.Flush();
            Debug.Print("Menu rendered");
        }

        private Font menuFont = Resources.GetFont(Resources.FontResources.small);

        public Font MenuFont
        {
            get { return menuFont; }
        }


    }
}