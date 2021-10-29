using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace KiCoKalender.Configurations {
	public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions {
		public override OpenApiInfo Info { get; set; } = new OpenApiInfo() {
			Version = "3.0.0",
			Title = "BuyMyHouse for cloud databases",
			Description = "BuyMyHouse backend with .NET 5 Azure Functions with OpenAPI support.",
			TermsOfService = new Uri("https://github.com/Azure/azure-functions-openapi-extension"),
			Contact = new OpenApiContact() {
				Name = "Jelle Spreij",
				Email = "622120@student.inholland.nl",
			},
			License = new OpenApiLicense() {
				Name = "MIT",
				Url = new Uri("http://opensource.org/licenses/MIT"),
			}
		};

		public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
	}
}
