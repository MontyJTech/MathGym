using System;

class BasicAddition
{
    private Random random = new Random();

    public int augend { get; set; }
    public int addend { get; set; }
    public string givenAnswer { get; set; }

    public void GenerateNewProblem()
    {
        int newAugend = random.Next(1, 10);
        int newAddend = random.Next(1, 10);

        while(newAugend == augend && newAddend == addend)
        {
            newAugend = random.Next(1, 10);
            newAddend = random.Next(1, 10);
        }

        augend = newAugend;
        addend = newAddend;
    }

    public int GetAnswer()
    {
        return augend + addend;
    }

    public bool CheckGivenAnswer()
    {
        return int.TryParse(givenAnswer, out int answer) && answer == (augend + addend);
    }
}
