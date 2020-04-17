namespace NerdAllDebug.Sample.Host.Client.Options
{
    /// <summary>
    /// The options to be used by a *SampleServiceClient*.
    /// </summary>
    /// <example>
    /// Representation on appsettings.json file.
    /// <code>
    /// {
    ///   "Clients": {
    ///     "SampleServiceClient": {
    ///       "Name": "SampleServiceClient",
    ///       "AuthorizationToken": "c3d5f8de7c4744b39c532075df0d5dd8",
    ///       "BaseAddress": "https://domain-of-api.com/"
    ///     }
    ///   }
    /// }
    /// </code>
    /// </example>
    public class SampleServiceClientOptions
    {
        /// <summary>
        /// The logical name of the client to create.
        /// </summary>
        /// <value>SampleServiceClient</value>
        public string Name { get; set; }

        /// <summary>
        /// Key to authorize request on Microservices.
        /// </summary>
        /// <value>c3d5f8de7c4744b39c532075df0d5dd8</value>
        public string AuthorizationToken { get; set; }

        /// <summary>
        /// URL that identify host of the HTTP Rest API call.
        /// </summary>
        /// <value>https://domain-of-api.com/</value>
        public string BaseAddress { get; set; }
    }
}
