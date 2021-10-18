using System.Threading.Tasks;
using Dev.Offboard.OffboardService.V1;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace DotnetGrpcClientIssueWeb.Controllers
{
    public class HomeController: Controller
    {
        private readonly OffboardServiceService.OffboardServiceServiceClient _offboardClient;
        private readonly Channel _channel;

        public HomeController(OffboardServiceService.OffboardServiceServiceClient offboardClient)
        {
            _offboardClient = offboardClient;
            _channel = new Channel("localhost", 8082, ChannelCredentials.Insecure);
        }

        [HttpGet("get-requests")]
        public async Task<IActionResult> GetRequests()
        {
            var requests = await _offboardClient.GetDismissalRequestsAsync(new DismissalRequestListRequest
            {
                Pager = new Pagination
                {
                    Limit = 100,
                    Offset = 0
                }
            });
        
            return Ok(requests);
        }
        
        [HttpGet("get-requests2")]
        public async Task<IActionResult> GetRequests2()
        {
            var client = new OffboardServiceService.OffboardServiceServiceClient(_channel);
            
            var requests = await client.GetDismissalRequestsAsync(new DismissalRequestListRequest
            {
                Pager = new Pagination
                {
                    Limit = 100,
                    Offset = 0
                }
            });

            return Ok(requests);
        }
    }
}