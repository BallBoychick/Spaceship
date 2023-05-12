// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using CoreWCF;
using CoreWCF.OpenApi.Attributes;
using CoreWCF.Web;

namespace CoreWCFServer
{

    [ServiceContract]
    [OpenApiBasePath("/api")]
    internal interface IWebApi
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/orders")]
        [OpenApiTag("Tag")]
        [OpenApiResponse(ContentTypes = new[] { "application/json" }, Description = "Success", StatusCode = HttpStatusCode.OK, Type = typeof(ExampleContract))]
        ExampleContract BodyEcho(
            [OpenApiParameter(ContentTypes = new[] { "application/json" }, Description = "param description.")] MessageContract param);
    }
}