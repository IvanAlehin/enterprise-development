var builder = DistributedApplication.CreateBuilder(args);

var kafkaTopic = "applications-topic";
var fetchMinBytes = "4096";
var kafkaProduceDelayMs = "1000";

var kafkaTopicParam = builder.AddParameter("KafkaTopic", kafkaTopic);
var produceDelayParam = builder.AddParameter("KafkaProduceDelayMs", kafkaProduceDelayMs);
var fetchMinBytesParam = builder.AddParameter("KafkaFetchMinBytes", fetchMinBytes);

var mysql = builder.AddMySql("MySql")
    .AddDatabase("EstateAgencyDb");

var kafka = builder.AddKafka("Kafka")
    .WithEnvironment("KafkaTopic", kafkaTopic)
    .WithEnvironment("KafkaProduceDelay", kafkaProduceDelayMs)
    .WithKafkaUI();

builder.AddProject<Projects.EstateAgency_Kafka>("KafkaProducer")
    .WithReference(kafka, "KafkaConnection")
    .WaitFor(kafka)
    .WithEnvironment("KafkaTopic", kafkaTopic)
    .WithEnvironment("KafkaProduceDelay", kafkaProduceDelayMs);

builder.AddProject<Projects.EstateAgency_Api>("EstateAgencyApi")
    .WithReference(kafka, "KafkaConnection")
    .WithReference(mysql, "DefaultConnection")
    .WaitFor(mysql)
    .WaitFor(kafka)
    .WithEnvironment("KafkaTopic", kafkaTopic)
    .WithEnvironment("KafkaFetchMinBytes", fetchMinBytes);

builder.Build().Run();
