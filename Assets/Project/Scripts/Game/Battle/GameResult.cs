

public class GameResult {
    public GameResult(bool won)
    {
        Won = won;
    }

    public bool Won { get; private set; }

    public static GameResult GetNewVictory()
    {
        return new GameResult(true);
    }
    public static GameResult GetNewLoss()
    {
        return new GameResult(false);
    }
}