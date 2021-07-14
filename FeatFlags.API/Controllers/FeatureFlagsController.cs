using System.Threading.Tasks;
using FeatFlags.API.FeatConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatFlags.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureFlagsController : ControllerBase
    {
        private readonly ILogger<FeatureFlagsController> _logger;
        private readonly IFeatureManager _featureManager;

        public FeatureFlagsController(ILogger<FeatureFlagsController> logger,
                                      IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (await _featureManager.IsEnabledAsync(FeatureFlagName.FeatureA))
            {
                _logger.LogInformation("Feature Flag AAA");
            }

            if (await _featureManager.IsEnabledAsync(FeatureFlagName.FeatureB))
            {
                _logger.LogWarning("Feature Flag BBB");
            }

            if (await _featureManager.IsEnabledAsync(FeatureFlagName.FeatureC))
            {
                _logger.LogError("Feature Flag CCC");
            }

            return Ok();
        }

        [HttpGet("feat-a")]
        [FeatureGate(FeatureFlagName.FeatureA)]
        public async Task<IActionResult> GetFeatureA()
        {
            if (await _featureManager.IsEnabledAsync(FeatureFlagName.FeatureA))
            {
                _logger.LogInformation("Feature Flag AAA");
                return Ok("Feature Flag AAA");
            }

            return BadRequest();
        }

        [HttpGet("feat-c")]
        [FeatureGate(FeatureFlagName.FeatureC)]
        public async Task<IActionResult> GetFeatureC()
        {
            if (await _featureManager.IsEnabledAsync(FeatureFlagName.FeatureC))
            {
                _logger.LogInformation("Feature Flag CCC");
                return Ok("Feature Flag CCC");
            }

            return BadRequest();
        }

        [HttpGet("feat-d")]
        public async Task<IActionResult> GetFeatureD()
        {
            if (await _featureManager.IsEnabledAsync(FeatureFlagName.FeatureD))
            {
                _logger.LogInformation("Feature Flag DDD");
                return Ok("Feature Flag DDD");
            }

            return BadRequest();
        }

        [HttpGet("feat-custom")]
        public async Task<IActionResult> GetFeatureUser()
        {
            if (await _featureManager.IsEnabledAsync(FeatureFlagName.FeatureCustom))
            {
                _logger.LogInformation("Feature Flag CUSTOM");
                return Ok("Feature Flag CUSTOM");
            }

            return BadRequest();
        }
    }
}
