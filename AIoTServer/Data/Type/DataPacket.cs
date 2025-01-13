namespace AIoTServer.Data.Type;

public class DataPacket(int type, object data)
{
    public int Type { get; } = type;
    public object Data { get; } = data;

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(DataPacket)) return false;

        var dataPacket = (DataPacket)obj;
        return dataPacket.Data.Equals(Data) && Type == dataPacket.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Data, Type);
    }
}