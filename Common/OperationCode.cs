namespace Common
{
    //区分请求和响应的请求
    public enum OperationCode : byte
    {
        Login,
        Register,
        Default,
        SyncPosition,
        SyncPlayer
    }
}