using CharacterGenerator.DesktopApp.Enums;

namespace CharacterGenerator.DesktopApp.Models
{
    public class Character
    {
        public string Name { get; internal set; }
        public Gender Gender { get; internal set; }

        public int Charisma { get; internal set; }
        public int Constitution { get; internal set; }
        public int Dexterity { get; internal set; }
        public int Intelligence { get; internal set; }
        public int Strength { get; internal set; }
        public int Wisdom { get; internal set; }
    }
}