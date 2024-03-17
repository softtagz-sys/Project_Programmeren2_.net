using System.Data.Common;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MTGM.DAL.EF;

namespace Tests.IntegrationTests.Config;

/*
Online MS Documentatie:
https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0#customize-webapplicationfactory
*/
public class ExtendedWebApplicationFactoryWithMockAuth<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    public SqliteConnection SqliteInMemoryConnection { get; private set; }

    private MockClaimSeed mockClaimSeed = new MockClaimSeed(new Claim[] {});
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextOptionsDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<MtgmDbContext>));
            services.Remove(dbContextOptionsDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));
            services.Remove(dbConnectionDescriptor);

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                SqliteInMemoryConnection = new SqliteConnection("DataSource=:memory:");
                var connection = SqliteInMemoryConnection;
                connection.Open();

                return connection;
            });

            services.AddDbContext<MtgmDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
        });
        
        
        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<IAuthenticationSchemeProvider, MockSchemeProvider>();
            services.AddScoped<MockClaimSeed>(_ => mockClaimSeed);
        });
        

        builder.UseEnvironment("Development");
    }
    
    /* Source: https://www.youtube.com/watch?v=b1-KG_x-Y5Q */
    public ExtendedWebApplicationFactoryWithMockAuth<TProgram> SetAuthenticatedUser(params Claim[] claimSeed)
    {
        mockClaimSeed = new MockClaimSeed(claimSeed);
        
        return this;
    }

    public class MockSchemeProvider : AuthenticationSchemeProvider
    {
        public MockSchemeProvider(IOptions<AuthenticationOptions> options) : base(options)
        {
        }

        protected MockSchemeProvider(
            IOptions<AuthenticationOptions> options,
            IDictionary<string, AuthenticationScheme> schemes
        )
            : base(options, schemes)
        {
        }

        public override Task<AuthenticationScheme> GetSchemeAsync(string name)
        {
            AuthenticationScheme mockScheme = new(
                IdentityConstants.ApplicationScheme,
                IdentityConstants.ApplicationScheme,
                typeof(MockAuthenticationHandler)
            );
            return Task.FromResult(mockScheme);
        }
    }

    public class MockAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly MockClaimSeed _claimSeed;

        public MockAuthenticationHandler(
            MockClaimSeed claimSeed,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        )
            : base(options, logger, encoder, clock)
        {
            _claimSeed = claimSeed;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!_claimSeed.GetSeeds().Any())
                return Task.FromResult(AuthenticateResult.Fail("No authenticated user seeded for test!"));
            
            var claimsIdentity = new ClaimsIdentity(_claimSeed.GetSeeds(), IdentityConstants.ApplicationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(claimsPrincipal, IdentityConstants.ApplicationScheme);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
    
    public class MockClaimSeed
    {
        private readonly IEnumerable<Claim> _seed;

        public MockClaimSeed(IEnumerable<Claim> seed)
        {
            _seed = seed;
        }

        public IEnumerable<Claim> GetSeeds() => _seed;
    }
}
