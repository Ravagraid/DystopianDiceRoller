namespace DystopianDiceRoller
{
    internal class ConsoleLayout
    {
        private readonly Probability P = new();
        private readonly Dictionary<int, float> diceresult = new();
        public ConsoleLayout(int dicenum) {
            Probability p = new(dicenum);
            diceresult = p.Results;
            P = p;
        }

        //layout for tables
        public Layout TbLayout()
        {
            var layout = new Layout("Root")
                .SplitColumns(new Layout("Left"),
                              new Layout("Right"));

            layout["Left"].Update(
                new Panel(
                    Align.Center(
                        DefaultTbl()))
                .Expand());

            layout["Right"].Update(
                new Panel(
                    Align.Center(
                        ScenarioTbl()))
                .Expand());

            return layout;
        }

        //Table to show dice outcome spread
        private Table DefaultTbl()
        {
            Table table = new()
            {
                Border = TableBorder.Minimal
            };

            table.AddColumn(new TableColumn("Die Roll").Centered());
            table.AddColumn("Result");

            foreach (var item in diceresult)
            {
                table.AddRow(Convert.ToString(item.Key + 1), Convert.ToString(item.Value));
            }
            return table;
        }

        //table to show total hits in different scenarios
        private Table ScenarioTbl()
        {
            Table table = new()
            {
                Border = TableBorder.Minimal
            };

            table.AddColumn("Scenario");
            table.AddColumn("Total Hits");

            table.AddRow("Normal",Convert.ToString(P.TotalHits(0)));
            table.AddRow("Normal vs Obscured", Convert.ToString(P.TotalHits(1)));
            table.AddRow("Heavy Hits Count As Singles", Convert.ToString(P.TotalHits(2)));
            table.AddRow("Sustained/Homing", Convert.ToString(P.TotalHits(3)));
            table.AddRow("Sustained/Homing vs Obscured", Convert.ToString(P.TotalHits(4)));
            table.AddRow("Fusilade", Convert.ToString(P.TotalHits(5)));
            table.AddRow("Fusilade vs Obscured", Convert.ToString(P.TotalHits(6)));
            table.AddRow("Devastating", Convert.ToString(P.TotalHits(7)));
            table.AddRow("Devastating vs Obscured", Convert.ToString(P.TotalHits(8)));
            return table;
        }
    }
}