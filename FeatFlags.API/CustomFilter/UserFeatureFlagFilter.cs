using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace FeatFlags.API.CustomFilter
{
    [FilterAlias("UserFlag")]
    public class UserFeatureFlagFilter : IFeatureFilter
    {
        private readonly ILogger<UserFeatureFlagFilter> _logger;

        public UserFeatureFlagFilter(ILogger<UserFeatureFlagFilter> logger)
        {
            _logger = logger;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var settings = context.Parameters.GetSection("UserFlagParam");
            _logger.LogCritical(settings.Value);

            return Task.FromResult(bool.Parse(settings.Value));
        }
    }
}