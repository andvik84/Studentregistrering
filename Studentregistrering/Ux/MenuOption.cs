﻿namespace Studentregistrering
{
    public class MenuOption : IMenuOption
    {
        public string SearchableText
        {
            get { return MenuText; }
        }
        public string MenuText { get; set; }

        public MenuOption(string text)
        {
            MenuText = text;
        }

    }
}
