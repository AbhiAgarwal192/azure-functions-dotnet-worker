using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace FunctionApp
{
    public class TelemetrySanitizer : ITelemetryInitializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetrySanitizer" /> class.
        /// </summary>
        public TelemetrySanitizer()
        {
        }
        /// <summary>
        /// Initializes the telemetry
        /// </summary>
        /// <param name="telemetry">Telemetry object</param>
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is ISupportProperties supportProperties)
            {
                foreach (string key in supportProperties.Properties.Keys)
                {
                    supportProperties.Properties[key] = this.Sanitize(supportProperties.Properties[key]);
                }
            }
            if (telemetry is TraceTelemetry traceTelemetry)
            {
                traceTelemetry.Message = this.Sanitize(traceTelemetry.Message);
            }
            if (telemetry is ExceptionTelemetry exceptionTelemetry)
            {
                exceptionTelemetry.Message = this.Sanitize(exceptionTelemetry.Message);
            }
        }
        private string Sanitize(string input) => input;
    }
}
