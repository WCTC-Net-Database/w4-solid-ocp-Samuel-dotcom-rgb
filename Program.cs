using W04.Interfaces;
using W04.Models;
using W04.Services;

namespace W04;

class Program
{
    // Notice: We use the INTERFACE, not the concrete class!
    // This is the key to OCP - we can swap implementations without changing this code.
    static IFileHandler fileHandler = null!;
    static List<Character> characters = new();
    static string currentFilePath = string.Empty;

    static void Main()
    {
        // Default to CSV - same data as Week 3, but now using the interface
        SetFileFormat("csv");
        characters = fileHandler.ReadAll();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Character Manager ===");
            Console.WriteLine("1. Display All Characters");
            Console.WriteLine("2. Find Character by Name");
            Console.WriteLine("3. Find Characters by Profession");
            Console.WriteLine("4. Add Character");
            Console.WriteLine("5. Level Up Character");
            Console.WriteLine("6. Switch File Format (CSV/JSON)");
            Console.WriteLine("0. Save and Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters();
                    break;
                case "2":
                    FindCharacterByName();
                    break;
                case "3":
                    FindCharactersByProfession();
                    break;
                case "4":
                    AddCharacter();
                    break;
                case "5":
                    LevelUpCharacter();
                    break;
                case "6":
                    SwitchFileFormat();
                    break;
                case "0":
                    fileHandler.WriteAll(characters);
                    Console.WriteLine($"Characters saved to {currentFilePath}");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Sets the file format by swapping the handler implementation.
    /// This demonstrates OCP - we can add new formats without modifying existing code!
    /// </summary>
    static void SetFileFormat(string format)
    {
        currentFilePath = Path.Combine("Files", format == "json" ? "input.json" : "input.csv");

        // Swap implementations based on format - THIS is OCP in action!
        if (format == "json")
        {
            fileHandler = new JsonFileHandler(currentFilePath);
        }
        else
        {
            fileHandler = new CsvFileHandler(currentFilePath);
        }

        Console.WriteLine($"Using {format.ToUpper()} format: {currentFilePath}");
    }

    static void DisplayAllCharacters()
    {
        Console.WriteLine("\n--- All Characters ---");
        if (characters.Count == 0)
        {
            Console.WriteLine("No characters found.");
            return;
        }

        foreach (var character in characters)
        {
            Console.WriteLine(character);
        }
    }

    static void FindCharacterByName()
    {
        Console.Write("Enter character name to find: ");
        string name = Console.ReadLine() ?? "";

        // Use the handler's FindByName method (LINQ inside!)
        var character = fileHandler.FindByName(characters, name);

        if (character != null)
        {
            Console.WriteLine($"Found: {character}");
        }
        else
        {
            Console.WriteLine($"No character found with name '{name}'");
        }
    }

    static void FindCharactersByProfession()
    {
        Console.Write("Enter character profession to filter (e.g., Fighter, Wizard): ");
        string profession = Console.ReadLine() ?? "";

        // Use the handler's FindByProfession method (LINQ inside!)
        var results = fileHandler.FindByProfession(characters, profession);

        Console.WriteLine($"\n--- {profession} Characters ({results.Count} found) ---");
        foreach (var character in results)
        {
            Console.WriteLine(character);
        }
    }

    static void AddCharacter()
    {
        Console.WriteLine("\n--- Add New Character ---");

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Profession (Fighter, Wizard, Rogue, etc.): ");
        string profession = Console.ReadLine() ?? "";

        Console.Write("Level: ");
        int.TryParse(Console.ReadLine(), out int level);

        Console.Write("HP: ");
        int.TryParse(Console.ReadLine(), out int hp);

        Console.Write("Equipment (comma-separated, e.g., sword,shield,potion): ");
        string equipmentInput = Console.ReadLine() ?? "";
        var equipment = equipmentInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(e => e.Trim())
                                      .ToList();

        var newCharacter = new Character(name, profession, level, hp, equipment);
        characters.Add(newCharacter);

        Console.WriteLine($"Added: {newCharacter}");
    }

    static void LevelUpCharacter()
    {
        Console.Write("Enter the name of the character to level up: ");
        string name = Console.ReadLine() ?? "";

        var character = fileHandler.FindByName(characters, name);

        if (character != null)
        {
            character.Level++;
            Console.WriteLine($"{character.Name} leveled up to level {character.Level}!");
        }
        else
        {
            Console.WriteLine("Character not found.");
        }
    }

    static void SwitchFileFormat()
    {
        Console.WriteLine("\n--- Switch File Format ---");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        Console.Write("Choose format: ");
        string choice = Console.ReadLine() ?? "";

        // Save current characters before switching
        fileHandler.WriteAll(characters);
        Console.WriteLine($"Saved to {currentFilePath}");

        // Switch to new format
        string newFormat = choice == "2" ? "json" : "csv";
        SetFileFormat(newFormat);

        // Load characters from new format (or keep current if file doesn't exist)
        try
        {
            characters = fileHandler.ReadAll();
            Console.WriteLine($"Loaded {characters.Count} characters from {currentFilePath}");
        }
        catch
        {
            Console.WriteLine($"No existing {newFormat.ToUpper()} file found. Keeping current characters.");
        }
    }
}
