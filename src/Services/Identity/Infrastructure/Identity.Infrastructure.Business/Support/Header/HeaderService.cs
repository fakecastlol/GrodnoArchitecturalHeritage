using Identity.Services.Interfaces.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Infrastructure.Business.Support.Header
{
    public class HeaderService : IHeaderService
    {
        private readonly IHttpContextAccessor _ctxAccessor;
        private readonly ILogger _logger;
        private KeyValuePair<string, StringValues>[] _requestHeaders;

        public HeaderService(IHttpContextAccessor ctxAccessor, ILogger<HeaderService> logger)
        {
            _ctxAccessor = ctxAccessor;
            SetHeaders();
            _logger = logger;
        }

        private void SetHeaders()
        {
            _requestHeaders = _ctxAccessor?.HttpContext?.Request?.Headers?.ToArray();
        }

        public string GetUserId()
        {
            foreach (var i in _requestHeaders)
            {
                _logger.LogWarning($"request headers for Email: {i.Key}---------------{i.Value}");
            }

            return _requestHeaders.FirstOrDefault((emp => emp.Key == "claims_UserId")).Value.ToString();
        }
    }
}
