using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NerdAllDebug.Sample.App.Messages
{
    /// <summary>
    /// Message with sample attributes.
    /// </summary>
     [Serializable]
    public class SampleMessage
    {
        /// <summary>
        /// Represents the identification of sample.
        /// </summary>
        /// <value>Int32 values.</value>
        [JsonPropertyName("idSample")]
        public int IdSample { get; set; }

        /// <summary>
        /// Represents name of sample.
        /// </summary>
        /// <value>String value.</value>
        [JsonPropertyName("name"), Required]
        public string Name { get; set; }

        /// <summary>
        /// Represents the date of last modified on sample.
        /// </summary>
        /// <value>Date and time value.</value>
        [JsonPropertyName("dateOfLastModified")]
        public DateTime DateOfLastModified { get; set; }
    }
}
