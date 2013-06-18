using System;
using System.Threading;
using Agent.Menu.Menu;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Agent.Menu
{
    public class Program
    {
        public static void Main()
        {

            var menu = new Menu.Menu();
            menu.Items.Add(new MenuItem() { Title = "Hello", CommandName = "Hello", CommandArg = "World" });
            menu.Items.Add(new MenuItem() { Title = "World", CommandName = "World", CommandArg = "World" });
            menu.Items.Add(new MenuItem() { Title = "This is not so long", CommandName = "NotLong", CommandArg = "World" });
            menu.Items.Add(new MenuItem() { Title = "This text is very very long", CommandName = "Long", CommandArg = "World" });
            menu.Items.Add(new MenuItem() { Title = "Nice and short", CommandName = "Short", CommandArg = "World" });
            menu.Items.Add(new MenuItem() { Title = "A", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "B", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "C", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "D", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "E", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "F", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "G", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "H", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "I", CommandName = "A", CommandArg = "A" });
            menu.OnMenuItemClicked += menu_OnMenuItemClicked;
            menu.Render();


            ButtonHelper helper = ButtonHelper.Current;
            helper.OnButtonPress += helper_OnButtonPress;
            System.Threading.Thread.Sleep(Timeout.Infinite);
        }

        static void helper_OnButtonPress(Buttons button, InterruptPort port, ButtonDirection direction, DateTime time)
        {
            Debug.Print("hiiiiii");
        }

        static void menu_OnMenuItemClicked(Menu.Menu menu, MenuItem menuItem, DateTime time)
        {
            Debug.Print(menuItem.Title + " was clicked at " + time.ToString());

        }

    }
}
