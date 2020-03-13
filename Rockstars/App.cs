using Android.App;
using Android.Runtime;
using Autofac;
using Rockstars.Implementation.Managers;
using Rockstars.Implementation.ViewModels;
using Rockstars.ViewModels;
using System;
using System.IO;

namespace Rockstars
{
    [Application]
    public class App : Application
    {
        public static IContainer Container { get; set; }

        /// <summary>
        ///     Maakt een instantie van <see cref="App"/>
        /// </summary>
        /// <param name="javaReference"></param>
        /// <param name="transfer"></param>
        public App(IntPtr javaReference, JniHandleOwnership transfer)
        { }

        /// <summary>
        ///     Wordt aangeroepen wanneer de applicatie voor de eerste keer wordt gestart
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();

            App.Initialize();
        }

        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new MusicManager()).As<IMusicManager>();
            builder.RegisterType<ArtistsViewModel>();
            builder.RegisterInstance(new PlaylistManager()).As<IPlaylistManager>();
            builder.RegisterType<PlaylistViewModel>();

            Container = builder.Build();
        }
    }
}
