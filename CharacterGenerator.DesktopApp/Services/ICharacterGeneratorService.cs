using CharacterGenerator.DesktopApp.Enums;
using CharacterGenerator.DesktopApp.Models;

namespace CharacterGenerator.DesktopApp.Services
{
    public interface ICharacterGeneratorService
    {
        public Character GenerateCharacter(Gender gender);
    }
}
