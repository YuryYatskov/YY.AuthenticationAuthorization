﻿using Microsoft.AspNetCore.Authorization;

namespace ClientMVC.Infrastructure.Auth
{
    public class OlderThanRequirement : IAuthorizationRequirement
    {
        public OlderThanRequirement(int years)
        {
            Years = years;
        }

        public int Years { get; }
    }
}
