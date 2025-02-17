using System;

class BasicAddition
{
    private Random random = new Random();

    public int augend { get; set; }
    public int addend { get; set; }
    public string givenAnswer { get; set; }

    public void GenerateNewProblem()
    {
        augend = random.Next(1, 10);
        addend = random.Next(1, 10);
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
