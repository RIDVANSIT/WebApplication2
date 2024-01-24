using System;
namespace WebApplication2.Core;
public abstract class Entity
{
}
public abstract class Entity<PKey>:Entity
    where PKey : struct
{
    public PKey Id { get; set; }
}

