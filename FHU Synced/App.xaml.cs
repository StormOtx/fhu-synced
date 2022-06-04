using Autofac;
using Autofac.Features.ResolveAnything;
using AutoMapper;
using FHU_Synced.DTOs;
using FHU_Synced.Extensions;
using FHU_Synced.Interfaces;
using FHU_Synced.Models;
using FHU_Synced.Repositories;
using FHU_Synced.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FHU_Synced
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterType<PresetRepository>().As<IPresetRepository>().SingleInstance();
            builder.RegisterType<MainWindow>().AsSelf();

            var container = builder.Build();

            DIResolver.Resolver = (type) => {
                return container.Resolve(type);
            };

            using (var scope = container.BeginLifetimeScope())
            {
                var window = scope.Resolve<MainWindow>();
                window.Show();
            }
        }
    }
}
