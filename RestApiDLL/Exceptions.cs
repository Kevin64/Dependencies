using ConstantsDLL.Properties;
using System;

namespace RestApiDLL
{
    [Serializable]
    public class UnregisteredAssetException : Exception
    {
        public UnregisteredAssetException() : base(LogStrings.LOG_ASSET_NOT_EXIST) { }
    }

    [Serializable]
    public class InvalidAgentException : Exception
    {
        public InvalidAgentException() : base(LogStrings.LOG_AGENT_INVALID_CREDENTIALS) { }
    }

    [Serializable]
    public class UnregisteredModelException : Exception
    {
        public UnregisteredModelException() : base(LogStrings.LOG_MODEL_NOT_EXIST) { }
    }

    [Serializable]
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException() : base(UIStrings.PARAMETER_ERROR) { }
    }

    [Serializable]
    public class InvalidRestApiCallException : Exception
    {
        public InvalidRestApiCallException() : base(LogStrings.LOG_WEB_SERVICE_ERROR) { }
    }
}
