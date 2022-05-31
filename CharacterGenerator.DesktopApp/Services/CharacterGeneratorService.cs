using CharacterGenerator.DesktopApp.Enums;
using CharacterGenerator.DesktopApp.Models;
using CharacterGenerator.DesktopApp.Utils;

namespace CharacterGenerator.DesktopApp.Services
{
    public class CharacterGeneratorService : ICharacterGeneratorService
    {
        public Character GenerateCharacter(Gender gender)
        {
            return new Character
            {
                Charisma = GetStat(),
                Constitution = GetStat(),
                Dexterity = GetStat(),
                Gender = gender,
                Intelligence = GetStat(),
                Name = GetName(gender),
                Strength = GetStat(),
                Wisdom = GetStat(),
            };
        }

        private int GetStat()
        {
            return GetRandom.Int32(1, 18);
        }

        private string GetName(Gender gender)
        {
            return GetRandom.FirstName(gender == Gender.Male);
        }
    }
}
