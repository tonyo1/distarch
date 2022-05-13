using System.Text;
using System.Text.Json;
using Confluent.Kafka;


namespace DtoLib;

public class HLAgentMessage : ISerializer<HLAgentMessage>
{
    
    byte[] ISerializer<HLAgentMessage>.Serialize(HLAgentMessage data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data, GetMessageType(context), options);
    }
    static Type GetMessageType(SerializationContext context)
          => Type.GetType(Encoding.UTF8.GetString(context.Headers[0].GetValueBytes()))!;
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        IgnoreNullValues = false,
    };

    public string Json { get; set; } = "";

    public HLAgentMessageType MessageType { get; set; } = HLAgentMessageType.None;
}

public enum HLAgentMessageType
{
    None=0,
    Pos = 1,
    StoreServer = 2,
    Associate = 3,
    Departament = 4,
    Catagory = 5,
    SubCatagory = 6,
}



public class Associate
{
    
    public String Name { get; set; } = "";
    public String Code { get; set; } = "";
}