namespace API.BLL.Common;

public class ServiceResult<T>
{
    public bool Result { get; private set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }

    private ServiceResult(bool result, T data, string errorMessage)
    {
        Result = result;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public static ServiceResult<T> Success(T data)
    {
        return new ServiceResult<T>(true, data, null);
    }

    public static ServiceResult<T> Failure(string errorMessage)
    {
        return new ServiceResult<T>(false, default, errorMessage);
    }
}
