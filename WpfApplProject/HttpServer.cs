using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class HttpServer
{
    private readonly HttpListener _listener;
    private readonly string _url;
    private readonly Func<string, Task> _handleRequest;

    public HttpServer(string url, Func<string, Task> handleRequest)
    {
        _url = url;
        _listener = new HttpListener();
        _listener.Prefixes.Add(_url);
        _handleRequest = handleRequest;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _listener.Start();
        Console.WriteLine("HTTP Server started at " + _url);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var context = await _listener.GetContextAsync();
                var request = context.Request;
                var response = context.Response;

                if (request.HttpMethod == "POST")
                {
                    using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string json = await reader.ReadToEndAsync();
                        await _handleRequest(json);
                    }

                    response.StatusCode = (int)HttpStatusCode.OK;
                    byte[] buffer = Encoding.UTF8.GetBytes("OK");
                    response.ContentLength64 = buffer.Length;
                    await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                }

                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        _listener.Stop();
    }

    public void Stop()
    {
        _listener.Stop();
    }
}
