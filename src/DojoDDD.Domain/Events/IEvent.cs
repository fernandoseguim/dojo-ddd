using System;

namespace DojoDDD.Domain.Events
{
    public interface IEvent<out TData>
    {
        string EntityId { get; }

        string Name { get; }
        /// <summary>
        /// Date when the event was raised
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Additional data associated to the event
        /// </summary>
        /// <example>
        /// {
        ///     Name: John Doe,
        ///     Email: johndoe@email.com,
        ///     CreatedAt: 2020-10-10
        /// }
        /// </example>
        TData Data { get; }
    }
}