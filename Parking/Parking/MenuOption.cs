using System;

namespace Parking
{
    internal class MenuOption
    {
        public string ItemText { get; }
        public Action ItemHandler { get; }
        public bool IsExitOption { get; }

        public MenuOption(string itemText, Action itemHandler, bool isExitOption = false)
        {
            ItemText = itemText;
            ItemHandler = itemHandler;
            IsExitOption = isExitOption;
        }
    }
}
