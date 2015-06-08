using System;

namespace XmlParser.Data.Core
{
    public class IBaseEntity<T>
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        T Id { get; set; }

        DateTime DateTimeModified { get; set; } 
    }
}