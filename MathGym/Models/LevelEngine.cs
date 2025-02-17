class LevelEngine
{
    public const int NUM_LEVELS = 10;
    public int curLevel = 0;

    public void BeginLevel()
    {
        //Start a timer
    }

    public void LevelStep()
    {
        curLevel++;
    }

    public bool CheckLevelEnd()
    {
        return curLevel < NUM_LEVELS;
    }

    public void EndLevel() 
    { 
        //Stop the timer
        //Log the time
    }
}
