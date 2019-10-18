namespace Common
{
    //服务端自动向客户端发送事件类型
    public enum EventCode : byte
    {
        NewPlayer,
        ClosePlayer,
        SyncPosition
    }
}