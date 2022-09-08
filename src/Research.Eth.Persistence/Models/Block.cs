namespace Research.Eth.Persistence.Models;

public class Block
{
    public int Number { get; set; }

    public string Hash { get; set; }
    public string PrevHash { get; set; }
    
}