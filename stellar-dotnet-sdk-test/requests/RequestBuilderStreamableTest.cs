using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class RequestBuilderStreamableTest
    {
        private readonly Uri _uri = new Uri("http://test.com");

        [TestMethod]
        public async Task TestHelloStream()
        {
            // Check we skip the first message with "hello"data
            var fakeHandler = new FakeHttpMessageHandler();
            var stream = "event: open\ndata: hello\n\ndata: foobar\n\n";
            fakeHandler.QueueResponse(FakeResponse.StartsStream(StreamAction.Write(stream)));

            var eventSource = new SSEEventSource(_uri, builder => builder.MessageHandler(fakeHandler));

            string dataReceived = null;
            eventSource.Message += (sender, args) =>
            {
                dataReceived = args.Data;
                eventSource.Shutdown();
            };

            await eventSource.Connect();

            Assert.AreEqual("foobar", dataReceived);
        }
    }
}