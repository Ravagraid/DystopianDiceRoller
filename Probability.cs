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
     //Combines relevant hits into a single dice total.
    public float TotalHits(int Scenario)
    {
        float output;

        switch (Scenario)
        {
            default: //In case something goes fucky
                return 0;

            case 0: //Normal Total Number
                output = HitCases(false, 0, false, false);
                return output;

            case 1: //Normal vs Obscured
                output = HitCases(true, 0, false, false);
                return output;

            case 2: //Heavy Hits become single hits
                output = HitCases(false, 0, false, true);
                return output;

            case 3: //Sustained/Homing
                output = HitCases(false, 1, false, false);
                return output;

            case 4:  //Sustained/Homing vs Obscured
                output = HitCases(true, 1, false, false);
                return output;

            case 5: //Fusilade
                output = HitCases(false, 2, false, false);
                return output;

            case 6:  //Fusilade vs Obscured
                output = HitCases(true, 2, false, false);
                return output;

            case 7: // Devastating
                output = HitCases(false, 0, true, false);
                return output;

            case 8: // Devastating vs Obscured
                output = HitCases(true, 0, true, false);
                return output;
        }
    }

    internal float HitCases(bool Obscured, int NumOfRerolls, bool Devastating, bool HeavyToSingle)
    {
        float output, Blanks, SingleHits, HeavyHits, ExplodingHits;

        Blanks = Results[0] + ExpDice(Obscured);
        SingleHits = results[3] + ExpDice(Obscured);
        HeavyHits = results[4]+ ExpDice(Obscured);
        ExplodingHits = results[5] + ExpDice(Obscured);

        for (int i = 0; i < NumOfRerolls; i++)
        {
            SingleHits += Blanks / 6;
            HeavyHits += Blanks / 6;
            ExplodingHits += Blanks / 6;
        }

        if (!HeavyToSingle) { HeavyHits *= 2; }

        if (Devastating) { ExplodingHits *= 3; }
        else { ExplodingHits *= 2; }

        output = SingleHits + HeavyHits + ExplodingHits;
        return output;
    }

    //Attempt at better exploding hit function
    internal float ExpDice(bool Obscured)
    {
        float initResult = results[0];
        float temp1, temp2, output = 0;

        //if Obscured is true, loop 0 times. If false, loop 3 times.
        int LoopNum = Obscured ? 0 : 4;
        
        temp1 = initResult / 6;

        //loops through initial result to generate additional hits from exploding dice
        //simulates diminishing returns from Exploding Hits.
        for (int i = 0; i < LoopNum; i++)
        {
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