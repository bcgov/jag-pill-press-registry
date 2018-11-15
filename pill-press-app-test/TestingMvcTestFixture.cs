using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gov.Jag.PillPressRegistry.Public.Test
{
    public class TestingMvcTestFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public TestingMvcTestFixture()
            : base() { }
    }
}
