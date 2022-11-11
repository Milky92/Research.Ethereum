using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using Research.Eth.Persistence.Enums;

namespace Research.Eth.Persistence.Models;

public class Asset:EntityBase<Guid>
{
    [StringLength(50)]
    public string Name { get; set; }
    
    [StringLength(4)]
    public string Code { get; set; }
    
    public AssetType Type { get; set; }
    
    [NotMapped]
    public ICollection<Wallet> Wallets { get; set; }
}