﻿using EventBus.Abstructions;
using EventBus;
using RabbitMqCustomLib;
using Autofac;
using UniversityApi.IntegrationEvents.EventHandling;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using System.Data.Common;
using UniversityApi.IntegrationEvents;
using RabbitMQ.Client;

namespace UniversityApi.Extensions;

public static class CustomeExtentionMethods
{
    public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
    {
       

        services.AddTransient<IUniversityIntegrationEventService, UniversityIntegrationEventService>();

       
     
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["AppSettings:EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["AppSettings:EventBusUserName"]))
                {
                    factory.UserName = configuration["AppSettings:EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["AppSettings:EventBusPassword"]))
                {
                    factory.Password = configuration["AppSettings:EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["AppSettings:EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["AppSettings:EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });
        

        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
      
       
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = configuration["AppSettings:SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["AppSettings:EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["AppSettings:EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });
        

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        services.AddTransient<CreateProfileBasedOnDataFromUniversityServiceIntegrationEventHandler>();
    
        return services;
    }

}
