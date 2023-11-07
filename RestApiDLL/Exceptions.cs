using ConstantsDLL.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiDLL
{
    [Serializable]
    public class InvalidAssetException : Exception
    {
        public InvalidAssetException() : base(LogStrings.LOG_ASSET_NOT_EXIST) { }
    }

    [Serializable]
    public class InvalidAgentException : Exception
    {
        public InvalidAgentException() : base(LogStrings.LOG_AGENT_INVALID_CREDENTIALS) { }
    }

    [Serializable]
    public class InvalidModelException : Exception
    {
        public InvalidModelException() : base(LogStrings.LOG_MODEL_NOT_EXIST) { }
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
