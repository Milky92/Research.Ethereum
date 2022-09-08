namespace Research.Eth.Persistence.Mongo;

public class DatabaseSettings
{
    /// <summary>
    /// Get or set full connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Get or set server config options.
    /// </summary>
    public ServerConfigurationOptions Server { get; set; } = null!;

    /// <summary>
    /// Get or set data base options.
    /// </summary>
    public DatabaseConfigurationOptions DatabaseOptions { get; set; } = null!;
    
    public CredentialOptions Credentials { get; set; }
}

public class ServerConfigurationOptions
{
    /// <summary>
    /// Get or set tls channel.
    /// </summary>
    public bool UseTls { get; set; }

    /// <summary>
    ///Get or set certificate path, for authorized channel. Required for use tls channel.
    /// </summary>
    public string CertPath { get; set; }

    /// <summary>
    /// Get or set retry reads 
    /// </summary>
    public bool RetryReads { get; set; }

    /// <summary>
    /// Get or set full address to server.
    /// </summary>
    public DbServerAddress Address { get; set; }
}

public class DatabaseConfigurationOptions
{
    /// <summary>
    /// Get or set database name
    /// </summary>
    public string Name { get; set; }
}

public class DbServerAddress
{
    /// <summary>
    /// Get or set host name.
    /// </summary>
    public string Host { get; set; }
    /// <summary>
    /// Get or set port.
    /// </summary>
    public int? Port { get; set; }

    public override string ToString() => GetFullAddress(Host, Port);

    private static string GetFullAddress(string host, int? port = default)
    {
        if (port == default)
        {
            return host;
        }

        return $"{host}:{port}";
    }
}


public class CredentialOptions
{
    public string User { get; set; }
    public string Password { get; set; }
}