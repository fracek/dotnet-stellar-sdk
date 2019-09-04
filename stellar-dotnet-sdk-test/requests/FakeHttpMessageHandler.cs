using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk_test.requests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly Queue<FakeResponse> _responses = new Queue<FakeResponse>();

        // Requests that were sent via the handler
        private readonly List<HttpRequestMessage> _requests = new List<HttpRequestMessage>();

        public event EventHandler<HttpRequestMessage> RequestReceived;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (_responses.Count == 0)
                throw new InvalidOperationException("No response configured");

            RequestReceived?.Invoke(this, request);

            _requests.Add(request);

            var response = _responses.Dequeue();
            return Task.FromResult(response.MakeResponse(cancellationToken));
        }

        public void QueueResponse(FakeResponse response) => _responses.Enqueue(response);
        public IEnumerable<HttpRequestMessage> GetRequests() => _requests;
    }

    public abstract class FakeResponse
    {
        public abstract HttpResponseMessage MakeResponse(CancellationToken cancellationToken);

        protected FakeResponse()
        {
        }

        public static FakeResponse WithHttpResponse(HttpResponseMessage message)
        {
            return new FakeResponseWithHttpResponse(message);
        }

        public static FakeResponse StartsStream(params StreamAction[] actions)
        {
            return new FakeResponseWithStream(actions);
        }
    }

    internal class FakeResponseWithHttpResponse : FakeResponse
    {
        private readonly HttpResponseMessage _message;

        public FakeResponseWithHttpResponse(HttpResponseMessage message)
        {
            _message = message;
        }

        public override HttpResponseMessage MakeResponse(CancellationToken cancellationToken)
        {
            return _message;
        }
    }

    internal class FakeResponseWithStream : FakeResponse
    {
        private readonly StreamAction[] _actions;

        public FakeResponseWithStream(StreamAction[] actions)
        {
            _actions = actions;
        }

        public override HttpResponseMessage MakeResponse(CancellationToken cancellationToken)
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var streamRead = new AnonymousPipeServerStream(PipeDirection.In);
            var streamWrite = new AnonymousPipeClientStream(PipeDirection.Out, streamRead.ClientSafePipeHandle);
            var content = new StreamContent(streamRead);
            content.Headers.ContentType = new MediaTypeHeaderValue("text/event-stream");
            httpResponse.Content = content;

            Task.Run(() => WriteStreamingResponse(streamWrite, cancellationToken));

            return httpResponse;
        }

        private async Task WriteStreamingResponse(Stream output, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var action in _actions)
                {
                    if (action.Delay != TimeSpan.Zero)
                    {
                        await Task.Delay(action.Delay, cancellationToken);
                    }

                    if (action.ShouldQuit())
                    {
                        return;
                    }

                    byte[] data = Encoding.UTF8.GetBytes(action.Content);
                    await output.WriteAsync(data, 0, data.Length, cancellationToken);
                }

                // if we've run out of actions, leave the stream open until it's cancelled
                while (true)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                }
            }
            catch (Exception)
            {
                // just exit
            }
        }
    }

    public class StreamAction
    {
        internal readonly TimeSpan Delay;
        internal readonly string Content;

        private StreamAction(TimeSpan delay, string content)
        {
            Delay = delay;
            Content = content;
        }

        public bool ShouldQuit()
        {
            return Content == null;
        }

        public static StreamAction Write(string content)
        {
            return new StreamAction(TimeSpan.Zero, content);
        }

        public static StreamAction CloseStream()
        {
            return new StreamAction(TimeSpan.Zero, null);
        }

        public StreamAction AfterDelay(TimeSpan delay)
        {
            return new StreamAction(delay, Content);
        }
    }
}