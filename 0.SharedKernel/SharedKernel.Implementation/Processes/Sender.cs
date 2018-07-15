﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Infrastructure.Messages;
using NM.SharedKernel.Infrastructure.Processes;

namespace NM.SharedKernel.Implementation.Processes
{
    internal class Sender : ISender
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructor

        public Sender(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Methods

        public Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if(command == null) throw new ArgumentException("Command cannot be null.");
            var handlers = _serviceProvider.GetServices<IMessageHandler<TCommand>>().ToArray();

            if(handlers.Length > 1) throw new ApplicationException("Cannot have more than one command handler per command.");
            if(handlers.Length == default) throw new ApplicationException($"No command handler has been registered for {nameof(TCommand)}.");

            return handlers.Single().Handle(command);
        }

        #endregion
    }
}