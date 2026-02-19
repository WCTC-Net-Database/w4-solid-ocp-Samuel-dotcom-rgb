using W04.Models;

namespace W04.Interfaces;

public interface IFileHandler
{
    List<Character> ReadAll();

    void WriteAll(List<Character> characters);

    void AppendCharacter(Character character);

    Character? FindByName(List<Character> characters, string name);

    List<Character> FindByProfession(List<Character> characters, string profession);
}
