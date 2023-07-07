using DystopianDiceRoller;
using Spectre.Console;

internal class Program
{
    public static void Main(string[] args)
    {
        int NumOfDice;

        Console.Write("Number of Dice: ");
        NumOfDice = int.TryParse(Console.ReadLine(), out int num) ? num : 0;
        Console.WriteLine();

        ConsoleLayout console = new(NumOfDice);
        AnsiConsole.Write(console.TbLayout());

        //console.writeline();
        //console.writeline($"total number of hits: {p.totalhits(0)}");
        //console.writeline();
        //console.writeline($"vs obscured: {p.totalhits(1)}");
    }
}