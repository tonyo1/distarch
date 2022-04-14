using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace DtoLib;
public class Bannana
{
    public string Name { get; set; } = "";
    public int Price { get; set; } = 1;
    public DateTime TS { get; set; } = DateTime.Now;

}
public class CustomValueSerializer<Bannana> : Confluent.Kafka.ISerializer<Bannana>
{
    public byte[] Serialize(Bannana data, SerializationContext ctx)
    {
        return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
    }
}