// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace FlatSharpEndToEndTests.Oracle.Flatc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LinkedListNode : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static LinkedListNode GetRootAsLinkedListNode(ByteBuffer _bb) { return GetRootAsLinkedListNode(_bb, new LinkedListNode()); }
  public static LinkedListNode GetRootAsLinkedListNode(ByteBuffer _bb, LinkedListNode obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LinkedListNode __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Value { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetValueBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetValueBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetValueArray() { return __p.__vector_as_array<byte>(4); }
  public FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode? Next { get { int o = __p.__offset(6); return o != 0 ? (FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode?)(new FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode> CreateLinkedListNode(FlatBufferBuilder builder,
      StringOffset ValueOffset = default(StringOffset),
      Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode> NextOffset = default(Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode>)) {
    builder.StartTable(2);
    LinkedListNode.AddNext(builder, NextOffset);
    LinkedListNode.AddValue(builder, ValueOffset);
    return LinkedListNode.EndLinkedListNode(builder);
  }

  public static void StartLinkedListNode(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddValue(FlatBufferBuilder builder, StringOffset ValueOffset) { builder.AddOffset(0, ValueOffset.Value, 0); }
  public static void AddNext(FlatBufferBuilder builder, Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode> NextOffset) { builder.AddOffset(1, NextOffset.Value, 0); }
  public static Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode> EndLinkedListNode(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode>(o);
  }
  public LinkedListNodeT UnPack() {
    var _o = new LinkedListNodeT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LinkedListNodeT _o) {
    _o.Value = this.Value;
    _o.Next = this.Next.HasValue ? this.Next.Value.UnPack() : null;
  }
  public static Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode> Pack(FlatBufferBuilder builder, LinkedListNodeT _o) {
    if (_o == null) return default(Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode>);
    var _Value = _o.Value == null ? default(StringOffset) : builder.CreateString(_o.Value);
    var _Next = _o.Next == null ? default(Offset<FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode>) : FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNode.Pack(builder, _o.Next);
    return CreateLinkedListNode(
      builder,
      _Value,
      _Next);
  }
}

public class LinkedListNodeT
{
  public string Value { get; set; }
  public FlatSharpEndToEndTests.Oracle.Flatc.LinkedListNodeT Next { get; set; }

  public LinkedListNodeT() {
    this.Value = null;
    this.Next = null;
  }
}


}
