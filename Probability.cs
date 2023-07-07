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
                output = HitCases(true);
                return output;

            case 1: //Normal vs Obscured
                output = HitCases(false);
                return output;

            case 2: //Sustained/Homing
                output = HitCases(true);
                return output;

            case 3: // Sustained/Homing vs Obscured
                output = HitCases(false);
                return output;

            case 4: //Fusilade
                output = HitCases(true);
                return output;

            case 5: // Fusilade vs Obscured
                output = HitCases(false);
                return output;
        }
    }

    internal float HitCases(bool Obscured /*bool singlereroll*/)
    {
        float output, Blanks, Counters, HeavyCounters, SingleHits, HeavyHits, ExplodingHits;


        Blanks = Results[0] + ExpDice(Obscured);
        Counters = Results[1] + ExpDice(Obscured);
        HeavyCounters = Results[2] + ExpDice(Obscured);
        SingleHits = results[3] + ExpDice(Obscured);
        HeavyHits = (results[4] * 2) + ExpDice(Obscured);
        ExplodingHits = (results[5] * 2) + ExpDice(Obscured);

        //if (singlereroll)
        //{
        //    SingleHits += Blanks / 6;
        //    HeavyHits += Blanks / 6;
        //    ExplodingHits += Blanks / 6;
        //    output = SingleHits + HeavyHits + ExplodingHits;
        //}

        output = SingleHits + HeavyHits + ExplodingHits;
        return output;
    }

    //Attempt at better exploding hit function
    internal float ExpDice(bool Obscured)
    {
        float initResult = results[0];
        float temp1, temp2, output = 0;

        //if Obscured is true, loop 3 times. If false, loop once.
        int LoopNum = Obscured ? 2 : 0;

        //loops through initial result to generate additional hits from exploding dice
        //simulates diminishing returns from Exploding Hits.
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