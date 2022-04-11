using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using FunWithSerialization.Protobuf.Models;
using ProtoBuf;

var data = JsonSerializer.Deserialize<User[]>(GetResourceContent("data.json"));

var result = ProtoSerialize<User[]>(data);

var deserialized = Serializer.Deserialize<User[]>(result);

Console.WriteLine();

static ReadOnlySpan<byte> ProtoSerialize<T>(T data)
{
    using var ms = new MemoryStream();
    Serializer.Serialize(ms, data);
    return ms.ToArray();
}

static string GetResourceContent(string name)
{
    var path = Assembly.GetExecutingAssembly()
        .GetManifestResourceNames()
        .FirstOrDefault(p => p.EndsWith(name));

    var resourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(path);

    using var reader = new StreamReader(resourceStream, Encoding.UTF8);

    return reader.ReadToEnd();
}