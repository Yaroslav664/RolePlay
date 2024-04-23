using System;
using System.Collections.Generic;
using System.Linq;

namespace RolePlay
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public List<Item> Inventory { get; set; }

        public Character(string name, int level, int health, int strength, int agility, int intelligence)
        {
            Name = name;
            Level = level;
            Health = health;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Inventory = new List<Item>();
        }

        public Character() { }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public int Weight { get; set; }
    }

    public class Game
    {
        public List<Character> Characters { get; set; }

        public Game()
        {
            Characters = new List<Character>();
        }

        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }

        public void RemoveCharacter(string name)
        {
            Character characterToRemove = Characters.FirstOrDefault(c => c.Name == name);
            if (characterToRemove != null)
                Characters.Remove(characterToRemove);
            else
                Console.WriteLine("Персонаж не знайдений.");
        }

        public void AddItemToCharacter(string characterName, Item item)
        {
            Character character = Characters.FirstOrDefault(c => c.Name == characterName);
            if (character != null)
                character.Inventory.Add(item);
            else
                Console.WriteLine("Персонаж не знайдений.");
        }

        public void RemoveItemFromCharacter(string characterName, string itemName)
        {
            Character character = Characters.FirstOrDefault(c => c.Name == characterName);
            if (character != null)
            {
                Item itemToRemove = character.Inventory.FirstOrDefault(i => i.Name == itemName);
                if (itemToRemove != null)
                    character.Inventory.Remove(itemToRemove);
                else
                    Console.WriteLine("Предмет не знайдений в інвентарі.");
            }
            else
                Console.WriteLine("Персонаж не знайдений.");
        }

        public void DisplayCharacterInfo(string characterName)
        {
            Character character = Characters.FirstOrDefault(c => c.Name == characterName);
            if (character != null)
            {
                Console.WriteLine($"Ім'я: {character.Name}, Рівень: {character.Level}, Здоров'я: {character.Health}");
            }
            else
                Console.WriteLine("Персонаж не знайдений.");
        }

        public void DisplayCharacterInventory(string characterName)
        {
            Character character = Characters.FirstOrDefault(c => c.Name == characterName);
            if (character != null)
            {
                Console.WriteLine($"Інвентар {character.Name}:");
                foreach (Item item in character.Inventory)
                {
                    Console.WriteLine($"Назва: {item.Name}, Тип: {item.Type}, Значення: {item.Value}, Вага: {item.Weight}");
                }
            }
            else
                Console.WriteLine("Персонаж не знайдений.");
        }

        public Character SearchCharacter(string characterName)
        {
            return Characters.FirstOrDefault(c => c.Name == characterName);
        }
    }

    class Program
    {
        static Game game = new Game();

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Виберіть опцію:");
                Console.WriteLine("1. Створити персонажа");
                Console.WriteLine("2. Видалити персонажа");
                Console.WriteLine("3. Додати предмет до інвентаря персонажа");
                Console.WriteLine("4. Видалити предмет з інвентаря персонажа");
                Console.WriteLine("5. Показати інформацію про персонажа");
                Console.WriteLine("6. Показати всі предмети в інвентарі персонажа");
                Console.WriteLine("7. Знайти персонажа за ім'ям");
                Console.WriteLine("8. Вийти з програми");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Неправильний ввід. Будь ласка, введіть число від 1 до 8.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateCharacter();
                        break;
                    case 2:
                        DeleteCharacter();
                        break;
                    case 3:
                        AddItemToCharacter();
                        break;
                    case 4:
                        RemoveItemFromCharacter();
                        break;
                    case 5:
                        ShowCharacterInfo();
                        break;
                    case 6:
                        ShowCharacterInventory();
                        break;
                    case 7:
                        SearchCharacter();
                        break;
                    case 8:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неправильний ввід. Будь ласка, введіть число від 1 до 8.");
                        break;
                }
            }
        }

        static void CreateCharacter()
        {
            Console.WriteLine("Введіть ім'я персонажа:");
            string name = Console.ReadLine();

            Console.WriteLine("Введіть рівень персонажа:");
            int level = GetPositiveIntFromUser("рівень");

            Console.WriteLine("Введіть здоров'я персонажа:");
            int health = GetNonNegativeIntFromUser("здоров'я");

            Console.WriteLine("Введіть силу персонажа:");
            int strength = GetNonNegativeIntFromUser("силу");

            Console.WriteLine("Введіть впевненість персонажа:");
            int agility = GetNonNegativeIntFromUser("впевненість");

            Console.WriteLine("Введіть інтелект персонажа:");
            int intelligence = GetNonNegativeIntFromUser("інтелект");

            Character newCharacter = new Character(name, level, health, strength, agility, intelligence);
            game.AddCharacter(newCharacter);
            Console.WriteLine("Персонаж створений.");
        }

        static void DeleteCharacter()
        {
            Console.WriteLine("Введіть ім'я персонажа, якого потрібно видалити:");
            string name = Console.ReadLine();

            game.RemoveCharacter(name);
            Console.WriteLine("Персонаж видалений.");
        }

        static void AddItemToCharacter()
        {
            Console.WriteLine("Введіть ім'я персонажа:");
            string characterName = Console.ReadLine();

            Console.WriteLine("Введіть назву предмета:");
            string itemName = Console.ReadLine();

            Console.WriteLine("Введіть тип предмета:");
            string itemType = Console.ReadLine();

            Console.WriteLine("Введіть значення предмета:");
            int itemValue = GetNonNegativeIntFromUser("значення");

            Console.WriteLine("Введіть вагу предмета:");
            int itemWeight = GetNonNegativeIntFromUser("вагу");

            Item newItem = new Item { Name = itemName, Type = itemType, Value = itemValue, Weight = itemWeight };
            game.AddItemToCharacter(characterName, newItem);
            Console.WriteLine("Предмет доданий до інвентаря персонажа.");
        }

        static void RemoveItemFromCharacter()
        {
            Console.WriteLine("Введіть ім'я персонажа:");
            string characterName = Console.ReadLine();

            Console.WriteLine("Введіть назву предмета, який потрібно видалити:");
            string itemName = Console.ReadLine();

            game.RemoveItemFromCharacter(characterName, itemName);
            Console.WriteLine("Предмет видалений з інвентаря персонажа.");
        }

        static void ShowCharacterInfo()
        {
            Console.WriteLine("Введіть ім'я персонажа:");
            string name = Console.ReadLine();

            game.DisplayCharacterInfo(name);
        }

        static void ShowCharacterInventory()
        {
            Console.WriteLine("Введіть ім'я персонажа:");
            string name = Console.ReadLine();

            game.DisplayCharacterInventory(name);
        }

        static void SearchCharacter()
        {
            Console.WriteLine("Введіть ім'я персонажа, якого потрібно знайти:");
            string name = Console.ReadLine();

            Character character = game.SearchCharacter(name);
            if (character != null)
                Console.WriteLine($"Персонаж '{name}' знайдений.");
            else
                Console.WriteLine("Персонаж не знайдений.");
        }

        static int GetPositiveIntFromUser(string attributeName)
        {
            int value;
            while (true)
            {
                Console.WriteLine($"Введіть {attributeName}:");
                if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                    return value;
                Console.WriteLine($"Неправильний ввід. {attributeName} має бути цілим числом більше 0.");
            }
        }

        static int GetNonNegativeIntFromUser(string attributeName)
        {
            int value;
            while (true)
            {
                Console.WriteLine($"Введіть {attributeName}:");
                if (int.TryParse(Console.ReadLine(), out value) && value >= 0)
                    return value;
                Console.WriteLine($"Неправильний ввід. {attributeName} має бути цілим числом не менше 0.");
            }
        }
    }
}
