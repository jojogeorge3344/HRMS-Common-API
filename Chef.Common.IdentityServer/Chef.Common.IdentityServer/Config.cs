// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Chef.Common.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                        new IdentityResource("role", new List<string>(){"roles"})
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
                new ApiScope("webApi"),
                new ApiScope("AdminUIClient"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource
                {
                    Name = "jp_api",
                    DisplayName = "JP API",
                    Description = "OAuth2 Server Management Api",
                    ApiSecrets = { new Secret(":}sFUz}Pjc]K4yiW>vDjM,+:tq=U989dxw=Vy*ViKrP+bjNbWC3B3&kE23Z=%#Jr".Sha256()) },

                    UserClaims =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "is4-rights",
                        "username",
                        "roles"
                    },

                    Scopes =
                    {
                        "jp_api.is4",
                    }
                }
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "MvcClient",
                    ClientName = "MVC Client",
                    ClientSecrets = { new Secret("{5B13CAD5-446D-41AC-90DE-2B6FB2D1A18F}".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:9002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:9002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                // Web Api Client
               new Client
               {
                    ClientId = "WebApiClient",
                    ClientName = "WebApi Client",
                    ClientSecrets = { new Secret("{31240256-6D33-44F1-A0E4-D00FB6815709}".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:9003/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:9003/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "webApi"
                    }
                },

               // Angular Client
                new Client
                {
                    ClientId = "AngularClient",
                    ClientName = "Angular Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { "https://localhost:9003/auth-callback" },
                    PostLogoutRedirectUris = { "https://localhost:9003/" },
                    AllowedCorsOrigins =     { "https://localhost:9003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "webApi"
                    },
                },
                /*
                * JP Project ID4 Admin Client
                */
                new Client
                {

                    ClientId = "IS4-Admin",
                    ClientName = "IS4-Admin",
                    ClientUri = "http://localhost:4300",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = true,
                    RequirePkce = true,
                    AllowPlainTextPkce = false,
                    RequireClientSecret = false,
                    RedirectUris = new[] {
                        "http://localhost:4300/login-callback",
                        "http://localhost:4300/silent-refresh.html"
                    },
                    AllowedCorsOrigins = { "http://localhost:4300" },
                    LogoUri = "https://jpproject.azurewebsites.net/sso/images/brand/logo.png",
                    PostLogoutRedirectUris = {"http://localhost:4300",},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "jp_api.is4",
                        "role"
                    }
                },

            };
    }
}