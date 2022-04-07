using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System;
namespace Retail;
class Program
{
    static void Main(string[] args)
    {
        var conf = new AdminClientConfig
        {
            // GroupId = "test-consumer-group",
            BootstrapServers = "localhost:29092",
            //EnableAutoCommit = false,

        };


        using (var adminClient = new AdminClientBuilder(conf).Build())
        {
            ListGroups(adminClient);
            PrintMetadata(adminClient);
            DothisThing(adminClient);
        }
    }
    static void ListGroups(IAdminClient adminClient)
    {
        // Warning: The API for this functionality is subject to change.
        var groups = adminClient.ListGroups(TimeSpan.FromSeconds(10));
        Console.WriteLine($"Consumer Groups:");
        foreach (var g in groups)
        {
            Console.WriteLine($"  Group: {g.Group} {g.Error} {g.State}");
            Console.WriteLine($"  Broker: {g.Broker.BrokerId} {g.Broker.Host}:{g.Broker.Port}");
            Console.WriteLine($"  Protocol: {g.ProtocolType} {g.Protocol}");
            Console.WriteLine($"  Members:");
            foreach (var m in g.Members)
            {
                Console.WriteLine($"    {m.MemberId} {m.ClientId} {m.ClientHost}");
                Console.WriteLine($"    Metadata: {m.MemberMetadata.Length} bytes");
                Console.WriteLine($"    Assignment: {m.MemberAssignment.Length} bytes");
            }
        }

    }
    static void PrintMetadata(IAdminClient adminClient)
    {
        {
            // Warning: The API for this functionality is subject to change.
            var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
            Console.WriteLine($"{meta.OriginatingBrokerId} {meta.OriginatingBrokerName}");
            meta.Brokers.ForEach(broker =>
                Console.WriteLine($"Broker: {broker.BrokerId} {broker.Host}:{broker.Port}"));

            meta.Topics.ForEach(topic =>
            {
                Console.WriteLine($"Topic: {topic.Topic} {topic.Error}");
                topic.Partitions.ForEach(partition =>
                {
                    Console.WriteLine($"  Partition: {partition.PartitionId}");
                    Console.WriteLine($"    Replicas: {partition.Replicas.Length}");
                    Console.WriteLine($"    InSyncReplicas: {partition.InSyncReplicas.Length}");
                });
            });

        }
    }
    static async Task CreateTopicAsync(string bootstrapServers, string topicName)
    {
        using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build())
        {
            //  try
            //  {
            //await adminClient.CreateTopicsAsync(new TopicSpecification[] {
            //   new TopicSpecification { Name = topicName, ReplicationFactor = 1, NumPartitions = 1 } });
            // }
            //  catch (CreateTopicsException e)
            // {
            //     Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
            // }
        }
    }


    static void DeleteTopics(IAdminClient adminClient)
    {
 
           // adminClient.DeleteTopicsAsync( , null);
      
    }
    static void DothisThing(IAdminClient adminClient)
    {
        adminClient.ListGroups(TimeSpan.FromSeconds(20)).ForEach(e =>
        {
            Console.WriteLine($"{e.Group} {e.Error} {e.State}");
        });
    }
}
