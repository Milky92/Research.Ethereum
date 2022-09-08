namespace Research.Eth.Persistence.Models;

public class Estimate
{
    public decimal GasPrice { get; set; }
    public decimal GasLimit { get; set; }
    public decimal Factor { get; set; }
    public decimal Base { get; set; }
}