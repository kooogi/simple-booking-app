public static class ConsoleMenu
{
  public static int ShowMenu(string title, string[] menuOptions)
  {
    int selectedIndex = 0;

    while (true)
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine($"=== {title} ===");
      Console.ResetColor();
      Console.WriteLine();

      DisplayMenu(menuOptions, selectedIndex);

      ConsoleKeyInfo keyInfo = Console.ReadKey(true);

      switch (keyInfo.Key)
      {
        case ConsoleKey.UpArrow:
          selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
          break;
        case ConsoleKey.DownArrow:
          selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
          break;
        case ConsoleKey.Enter:
          Console.CursorVisible = true;
          return selectedIndex;
        case ConsoleKey.Escape:
          Console.CursorVisible = true;
          return menuOptions.Length - 1;
      }
    }
  }
  static void DisplayMenu(string[] menuOptions, int selectedIndex)
    {
      Console.WriteLine("Select form available modules");
      Console.WriteLine(" ");
      for (int i = 0; i < menuOptions.Length; i++)
      {
        if (i == selectedIndex)
        {
          Console.BackgroundColor = ConsoleColor.White;
          Console.ForegroundColor = ConsoleColor.DarkBlue;
          Console.WriteLine($">  {menuOptions[i]}");
          Console.ResetColor();
        }
        else
        {
          Console.WriteLine($"   {menuOptions[i]}");
        }
      }
    }
}

