﻿namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual long Id { get; protected set; }
    }
}
