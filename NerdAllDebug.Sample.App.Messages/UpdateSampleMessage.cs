using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NerdAllDebug.Sample.App.Messages
{
    /// <summary>
    /// Message to update sample values.
    /// </summary>
    public class UpdateSampleMessage
    {
        /// <summary>
        /// Represents name of sample.
        /// </summary>
        /// <value>String value.</value>
        [JsonPropertyName("name"), Required]
        public string Name { get; set; }
    }
}
