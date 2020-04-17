using Microsoft.AspNetCore.Mvc;
using iBeach.Framework.Services;

namespace NerdAllDebug.Sample.App.Messages
{
    /// <summary>
    /// Request message for get one sample.
    /// </summary>
    public class GetSampleRequestMessage : RequestMessage
    {
        /// <summary>
        /// Represents the identification of sample.
        /// </summary>
        /// <value>Int32 values.</value>
        [BindProperty(Name = "idSample")]
        public int IdSample { get; set; }
    }
}
