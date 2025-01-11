namespace AIoTServer.Util
{
    public class DataPacket(byte type, Object data)
    {

        public byte Type { get; private set; } = type;
        public Object Data { get; private set; } = data;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(DataPacket))
            {
                return false;
            }

            var data2 = (DataPacket)obj;
            return data2.Data.Equals(Data) && Type == data2.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Data, Type);
        }
    }
}