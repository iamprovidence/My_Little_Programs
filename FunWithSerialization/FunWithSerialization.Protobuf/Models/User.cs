using ProtoBuf;

namespace FunWithSerialization.Protobuf.Models;

[ProtoContract]
class User
{
    [ProtoMember(tag: 1)]
    public string Name { get; set; }

    [ProtoMember(tag: 2)]
    public string Surname { get; set; }

    [ProtoMember(tag: 3)]
    public int Age { get; set; }
}

