using W04.Interfaces;
using W04.Models;

namespace W04.Services;

/// <summary>
/// CSV implementation of IFileHandler.
///
/// This class combines your Week 3 CharacterReader and CharacterWriter
/// into a single class that implements IFileHandler. The code inside
/// is the same - we're just organizing it behind an interface so we
/// can swap it with other implementations (like JSON).
/// </summary>
public class CsvFileHandler : IFileHandler
{
    private readonly string _filePath;

    public CsvFileHandler(string filePath)
    {
        _filePath = filePath;
    }

    public List<Character> ReadAll()
    {
        // TODO: Copy your Week 3 CharacterReader.ReadAll() logic here
        // 1. Read all lines from the file
        // 2. Skip the header row
        // 3. Parse each line into a Character object
        // 4. Return the list
        throw new NotImplementedException("Copy your Week 3 CSV reading logic here");
    }

    public void WriteAll(List<Character> characters)
    {
        // TODO: Copy your Week 3 CharacterWriter.WriteAll() logic here
        // 1. Convert each character to a CSV line
        // 2. Write all lines to the file (with header)
        throw new NotImplementedException("Copy your Week 3 CSV writing logic here");
    }

    public void AppendCharacter(Character character)
    {
        // TODO: Copy your Week 3 CharacterWriter.AppendCharacter() logic here
        // Hint: Use File.AppendAllText with a newline
        throw new NotImplementedException("Copy your Week 3 append logic here");
    }

    public Character? FindByName(List<Character> characters, string name)
    {
        // TODO: Copy your Week 3 LINQ logic here
        // Hint: return characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        throw new NotImplementedException("Copy your Week 3 FindByName LINQ here");
    }

    public List<Character> FindByProfession(List<Character> characters, string profession)
    {
        // TODO: Copy your Week 3 LINQ logic here
        // Hint: return characters.Where(c => c.Profession.Equals(profession, StringComparison.OrdinalIgnoreCase)).ToList();
        throw new NotImplementedException("Copy your Week 3 FindByProfession LINQ here");
    }
}
