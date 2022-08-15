namespace Research.Eth.Commons.Models;

public sealed class Result<TData>:IResult<TData>
{
    public Result() { }
    
    public Result(TData data)
    {
        Data = data;
    }

    public bool Success { get; private set; }
    public string Message { get; private set; }
    public TData Data { get; private set; }
    
    public int StatusCode { get; private set; }
    
    public static IResult Continue() =>
        new Result<TData>()
            .SetSuccess(true)
            .SetStatusCode(100);
    
    public static IResult Continue(string message) =>
        new Result<TData>()
            .SetSuccess(true)
            .SetMessage(message)
            .SetStatusCode(100);
    
    public static IResult Processing() =>
        new Result<TData>()
            .SetSuccess(true)
            .SetStatusCode(102);
    
    public static IResult Processing(string message) =>
        new Result<TData>()
            .SetSuccess(true)
            .SetMessage(message)
            .SetStatusCode(102);
    
    public static Result<TData> Ok(TData data) => new Result<TData>().SetData(data).SetStatusCode(200);

    public static Result<TData> Created() =>
        new Result<TData>()
            .SetSuccess(true)
            .SetStatusCode(201);
    
    public static Result<TData> Created(string message) =>
        new Result<TData>()
            .SetSuccess(true)
            .SetMessage(message)
            .SetStatusCode(204);
    
    public static Result<TData> NoContent() =>
        new Result<TData>()
            .SetSuccess(true)
            .SetStatusCode(201);
    
    public static Result<TData> Bad() =>
        new Result<TData>()
            .SetStatusCode(400);
    
    public static Result<TData> Bad(string message) =>
        new Result<TData>()
            .SetMessage(message)
            .SetStatusCode(400);
    
    public static Result<TData> NotAuthorized() =>
        new Result<TData>()
            .SetStatusCode(401);
    
    public static Result<TData> NotAuthorized(string message) =>
        new Result<TData>()
            .SetMessage(message)
            .SetStatusCode(401);
    
    public static Result<TData> Forbidden() =>
        new Result<TData>()
            .SetStatusCode(403);
    
    public static Result<TData> Forbidden(string message) =>
        new Result<TData>()
            .SetMessage(message)
            .SetStatusCode(403);
    
    public static Result<TData> NotFound(string message) =>
        new Result<TData>()
            .SetMessage(message)
            .SetStatusCode(404);
    
    public static Result<TData> NotAllowed() =>
        new Result<TData>()
            .SetStatusCode(405);
    
    public static Result<TData> NotAllowed(string message) =>
        new Result<TData>()
            .SetSuccess(false)
            .SetMessage(message)
            .SetStatusCode(405);

    public static Result<TData> InternalError() => new Result<TData>()
        .SetStatusCode(500);

    public static Result<TData> InternalError(string message) => new Result<TData>()
        .SetStatusCode(500)
        .SetMessage(message);

    public static Result<TData> NotImplemented() => new Result<TData>()
        .SetStatusCode(501);
    
    public static Result<TData> NotImplemented(string message) => new Result<TData>()
        .SetStatusCode(501)
        .SetMessage(message);
        
    
    #region fluent setters

    private Result<TData> SetData(TData data)
    {
        Data = data;
        return this;
    }

    private Result<TData> SetMessage(string message)
    {
        Message = message;
        return this;
    }

    private Result<TData> SetSuccess(bool success)
    {
        Success = success;
        return this;
    }
    
    private Result<TData> SetStatusCode(int code)
    {
        StatusCode = code;
        return this;
    }

    #endregion
  
}