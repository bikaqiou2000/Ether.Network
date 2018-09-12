using System;
using Ether.Network.Exceptions;
using Ether.Network.Tests.Contexts.NetConfig;
using Xunit;

namespace Ether.Network.Tests
{
    public class NetClientConfigurationTest : IDisposable
    {
        private readonly ConfigServer _server;

        public NetClientConfigurationTest()
        {
            this._server = new ConfigServer();
        }

        [Fact]
        public void StartClientWihtoutConfiguration()
        {
            this._server.SetupConfiguration();
            this._server.Start();

            using (var client = new ConfigClient())
            {
                Exception ex = Assert.Throws<EtherConfigurationException>(() => client.Connect());
                Assert.IsType<EtherConfigurationException>(ex);

                client.Disconnect();
            }
        }

        [Fact]
        public void StartClientWithConfiguration()
        {
            this._server.SetupConfiguration();
            this._server.Start();

            using (var client = new ConfigClient())
            {
                client.SetupConfiguration();
                client.Connect();
                client.Disconnect();
            }
        }

        [Fact]
        public void SetupClientConfigurationAfterConnected()
        {
            this._server.SetupConfiguration();
            this._server.Start();

            using (var client = new ConfigClient())
            {
                client.SetupConfiguration();
                client.Connect();

                Exception ex = Assert.Throws<EtherConfigurationException>(() => client.SetupConfiguration());
                Assert.IsType<EtherConfigurationException>(ex);

                client.Disconnect();
            }
        }

        public void Dispose()
        {
            this._server.Dispose();
        }
    }
}