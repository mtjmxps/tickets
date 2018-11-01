using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ticket.Startup))]
namespace Ticket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
