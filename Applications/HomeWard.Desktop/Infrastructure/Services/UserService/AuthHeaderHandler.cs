using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HomeWard.Desktop.Infrastructure.Services.UserService;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IUserService _user;

    public AuthHeaderHandler(IUserService user)
    {
        _user = user;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(_user.Token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _user.Token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
