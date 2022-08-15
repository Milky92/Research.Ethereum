namespace Research.Eth.Commons;

public interface IResult
{
    bool Success { get; }
    
    string Message { get; }
    
    int StatusCode { get; }
}

public interface IResult<out TData> : IResult
{
    TData Data { get; }
}