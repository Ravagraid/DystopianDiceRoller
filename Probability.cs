namespace DystopianDiceRoller;

internal class Probability
{
    private Dictionary<int, float> results = new();

    public Probability() { }
    public Probability(int value)
    {
        //adds initial results to dictionary
        for (int i = 0; i < 6; i++)
        {
            float temp = (float)Convert.ToDouble(value);
            results.Add(i, (float)(temp / 6));
        }
    }

    public Dictionary<int, float> Results => results;
    // Combines relevant hits into a single dice total.
    public float TotalHits(int Scenario)
    {
        float output;

        switch (Scenario)
        {
            default: //In case something goes fucky
                return 0;
            case 0: //Normal Total Number
                output = HitCases(0, false);
                return output;

            case 1: //Normal vs Obscured
                output = HitCases(1, false);
                return output;

            case 2: //Sustained/Homing
                output = HitCases(0, true);
                return output;
            case 3: // Sustained/Homing vs Obscured
                output = HitCases(1, true);
                return output;
        }
    }

    internal float HitCases(int Obscured, bool singlereroll)
    {
        float output, Blanks, Counters, HeavyCounters, SingleHits, HeavyHits, ExplodingHits;

        Blanks = Results[0] + Rerolls(Obscured);
        SingleHits = results[3] + Rerolls(Obscured);
        HeavyHits = (results[4] * 2) + Rerolls(Obscured);
        ExplodingHits = (results[5] * 2) + Rerolls(Obscured);
        if (singlereroll)
        {
            SingleHits += Blanks / 6;
            HeavyHits += Blanks / 6;
            ExplodingHits += Blanks / 6;
        }
        output = SingleHits + HeavyHits + ExplodingHits;
        return output;
    }

    //Function for exploding hits and other dice rerolling mechanics
    internal float Rerolls(int i) => i switch
    {
        //default rerolls
        0 => ExpDice(2),
        //No Exploding
        1 => ExpDice(0),
        //Fusillade
        2 => ExpDice(2) * 2,
        _ => 0,
    };

    //Attempt at better exploding hit function
    internal float ExpDice(int LoopNum)
    {
        float initResult = results[0];
        float temp1, temp2;
        float output = 0;
        for (int i = 0; i < LoopNum; i++)
        {
            temp1 = initResult / 6;
            if (i > 0)
            {
                temp2 = temp1 / 6;
                temp1 = temp2;
            }
            output += temp1;
        }
        return output;
    }
}