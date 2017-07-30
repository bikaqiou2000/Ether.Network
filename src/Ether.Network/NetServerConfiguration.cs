﻿using Ether.Network.Exceptions;
using Ether.Network.Utils;
using System.Linq;
using System.Net;

namespace Ether.Network
{
    /// <summary>
    /// Provide properties to configuration a <see cref="NetServer{T}"/>.
    /// </summary>
    public sealed class NetServerConfiguration
    {
        private readonly INetServer _server;
        private int _port;
        private int _backlog;
        private int _bufferSize;
        private int _maximumNumberOfConnections;
        private string _host;

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port
        {
            get { return this._port; }
            set { this.SetValue(ref this._port, value); }
        }
    
        /// <summary>
        /// Gets or sets the host address.
        /// </summary>
        public string Host
        {
            get { return this._host; }
            set { this.SetValue(ref this._host, value); }
        }

        /// <summary>
        /// Gets or sets the listening backlog.
        /// </summary>
        public int Backlog
        {
            get { return this._backlog; }
            set { this.SetValue(ref this._backlog, value); }
        }

        /// <summary>
        /// Gets or sets the maximum number of simultaneous connections on the server.
        /// </summary>
        public int MaximumNumberOfConnections
        {
            get { return this._maximumNumberOfConnections; }
            set { this.SetValue(ref this._maximumNumberOfConnections, value); }
        }

        /// <summary>
        /// Gets or sets the buffer size.
        /// </summary>
        public int BufferSize
        {
            get { return this._bufferSize; }
            set { this.SetValue(ref this._bufferSize, value); }
        }

        /// <summary>
        /// Gets the listening address.
        /// </summary>
        internal IPAddress Address => NetUtils.GetIpAddress(this._host);

        /// <summary>
        /// Creates a new <see cref="NetServerConfiguration"/>.
        /// </summary>
        public NetServerConfiguration()
            : this(null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="NetServerConfiguration"/>.
        /// </summary>
        /// <param name="server">Server reference</param>
        internal NetServerConfiguration(INetServer server)
        {
            this._server = server;
            this.Port = 0;
            this.Host = null;
            this.Backlog = 50;
            this.MaximumNumberOfConnections = 100;
            this.BufferSize = 4096;
        }

        /// <summary>
        /// Set the value of a property passed as reference.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="container"></param>
        /// <param name="value"></param>
        private void SetValue<T>(ref T container, T value)
        {
            if (this._server != null && this._server.IsRunning)
                throw new EtherConfigurationException("Cannot change configuration once the server is running.");

            if (!Equals(container, value))
                container = value;
        }
    }
}
