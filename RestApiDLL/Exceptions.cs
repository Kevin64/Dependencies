using ConstantsDLL.Properties;
using System;

namespace RestApiDLL
{
    /// <summary>
    /// Exception class for unregistered assets
    /// </summary>
    [Serializable]
    public class UnregisteredAssetException : Exception
    {
        public UnregisteredAssetException() : base(LogStrings.LOG_ASSET_NOT_EXIST) { }
    }

    /// <summary>
    /// Exception class for unauthorized agents
    /// </summary>
    [Serializable]
    public class InvalidAgentException : Exception
    {
        public InvalidAgentException() : base(LogStrings.LOG_AGENT_INVALID_CREDENTIALS) { }
    }

    /// <summary>
    /// Exception class for unregistered models
    /// </summary>
    [Serializable]
    public class UnregisteredModelException : Exception
    {
        public UnregisteredModelException() : base(LogStrings.LOG_MODEL_NOT_EXIST) { }
    }

    /// <summary>
    /// Exception class for invalid parameters
    /// </summary>
    [Serializable]
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException() : base(UIStrings.PARAMETER_ERROR) { }
    }

    /// <summary>
    /// Exception class for unsuccessful Rest calls
    /// </summary>
    [Serializable]
    public class InvalidRestApiCallException : Exception
    {
        public InvalidRestApiCallException() : base(LogStrings.LOG_WEB_SERVICE_ERROR) { }
    }
}
